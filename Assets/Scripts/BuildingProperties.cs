using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class BuildingProperties : MonoBehaviour
{
    public enum BuildingState { unpurchased, speakeasy, distillery}
    public float upfrontCost;
    public float rentCost;
    public TMP_Dropdown myDropDown;
    public float alcoholProductionInterval;
    public ResourceManagement resourceManager;
    public  Collider entranceTrigger;
    public GameObject deliveryTruckPrefab;
    public GameObject truckSpawnLocation;
    public BuildingState buildingState = BuildingState.unpurchased;
    public bool active = false;
    public GameObject canvas;
    public AudioSource moneySound;
    private float customerChanceOfEntering;

    // These should be displayed in building UI
    public int alcoholStores = 0;
    public int numCars = 1; //only display if distillery
    public int numCarsBusy = 0; //only display if distillery
    public int alcoholProducedSoFar = 0; //only display if distillery
    public float moneyMadeSoFar = 0f; //only display if speakeasy
    public float drinkPrice = 10f; //only display if speakeasy

    // Start is called before the first frame update
    void Start()
    {
        moneySound = GameObject.FindGameObjectWithTag("MoneySound").GetComponent<AudioSource>();

        // entranceTrigger.gameObject.SetActive(false);
        customerChanceOfEntering = 100f - (drinkPrice-9f);
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManagement>();

        canvas.SetActive(false);
    }

    public void ChangeType()
    {
        int type = myDropDown.value;
        if(type == 0)
        {
            buildingState = BuildingState.unpurchased;
        }
        else if(type == 1)
        {
            buildingState = BuildingState.speakeasy;
        }
        else
        {
            buildingState = BuildingState.distillery;
        }
        Debug.Log("Type changed to " + buildingState.ToString());
    }

    public void PurchaseBuilding()
    {
        if(buildingState == BuildingState.unpurchased)
        {
            return;
        }
        else if(buildingState == BuildingState.speakeasy)
        {
            //disable purchase canvas
            //enable speakeasy canvas
            active = true;
            resourceManager.SpendMoney(upfrontCost);
            resourceManager.ChangeTotalCosts(rentCost * -1f);
            // entranceTrigger.gameObject.SetActive(true);

            resourceManager.speakeasyList.Add(this.gameObject);
        }
        else 
        {
            //disable purchase canvas
            //enable distillery canvas

            active = true;
            resourceManager.SpendMoney(upfrontCost);
            resourceManager.ChangeTotalCosts(rentCost * -1f);
            StartCoroutine(produceAlcohol());
            StartCoroutine(spawnVehicleAfterTime());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Civilian" && buildingState == BuildingState.speakeasy && active)
        {
            float chanceToEnter = Random.Range(0, 100);
            if(chanceToEnter <= customerChanceOfEntering)
            {
                if(alcoholStores > 0)
                {
                    moneySound.Play();
                    alcoholStores--;
                    moneyMadeSoFar += drinkPrice;
                    canvas.SetActive(true);
                    StartCoroutine(waitToDisableCanvas());
                    resourceManager.MakeMoney(drinkPrice);
                }
            }
        }
        else if(other.gameObject.tag == "DeliveryVehicle")
        {
            Debug.Log("Vehicle near building entrance!");
            DeliveryCarController carController = other.gameObject.GetComponent<DeliveryCarController>();
            if(carController.targetDestination == this.truckSpawnLocation.transform)
            {
                Debug.Log("Vehicle made it to destination!");

                //if car arrives at a speakeasy
                if(carController.returningToBase == false)
                {
                    // carController.gameObject.GetComponent<BoxCollider>().enabled = false;
                    IEnumerator coroutine = waitToReturnToBase(carController);
                    StartCoroutine(coroutine);
                }
                //if car arrives back at home base
                else
                {
                    numCarsBusy--;
                    Destroy(other.gameObject);
                }
            }
        }
    }

    public void ChangeDrinkPrice(float drinkPrice)
    {
        if(drinkPrice < 10f)
        {
            Debug.LogError("ERROR: Minimum price is $10");
            return;
        }

        if(buildingState == BuildingState.speakeasy)
        {
            this.drinkPrice = drinkPrice;
            customerChanceOfEntering = 100f - (drinkPrice-9f);
        }
    }

    private void SpawnVehicle()
    {
        if(numCarsBusy < numCars && alcoholStores > 0 && active && resourceManager.speakeasyList.Count > 0)
        {
            numCarsBusy++;
            alcoholStores--;
            //instantiate delivery truck
            GameObject deliveryTruck = GameObject.Instantiate(deliveryTruckPrefab, truckSpawnLocation.transform.position, truckSpawnLocation.transform.rotation);
            deliveryTruck.GetComponent<DeliveryCarController>().homeBase = truckSpawnLocation;
            //delivery truck will choose target within range and pathfind to it, then return
            StartCoroutine(brieflyDisableTriggerCollider());

            GameObject lastBuilding = resourceManager.speakeasyList[0];
            GameObject closestSpeakeasy = resourceManager.speakeasyList[0];
            foreach(GameObject speakeasy in resourceManager.speakeasyList)
            {
                if(Vector3.Distance(this.transform.position, speakeasy.transform.position) < Vector3.Distance(this.transform.position, lastBuilding.transform.position))
                {
                    closestSpeakeasy = speakeasy;
                }
            }
            deliveryTruck.GetComponent<DeliveryCarController>().targetDestination = closestSpeakeasy.GetComponent<BuildingProperties>().truckSpawnLocation.transform;
        }
    }

    private IEnumerator produceAlcohol()
    {
        yield return new WaitForSeconds(alcoholProductionInterval);
        if(active)
        {
            alcoholStores++;
            alcoholProducedSoFar++;
        }
        StartCoroutine(produceAlcohol());
    }

    private IEnumerator spawnVehicleAfterTime()
    {
        yield return new WaitForSeconds(5f);
        if(active && alcoholStores > 0)
        {
            SpawnVehicle();
        }
        StartCoroutine(spawnVehicleAfterTime());
    }

    private IEnumerator waitToReturnToBase(DeliveryCarController carController)
    {
        yield return new WaitForSeconds(5f);
        carController.ChangeDestination(carController.homeBase.transform);
        carController.returningToBase = true;
        alcoholStores++;            
    }

    private IEnumerator brieflyDisableTriggerCollider()
    {
        entranceTrigger.enabled = false;
        yield return new WaitForSeconds(2f);
        entranceTrigger.enabled = true;
    }

    private IEnumerator waitToDisableCanvas()
    {
        yield return new WaitForSeconds(3f);
            canvas.SetActive(false);
    }
}
