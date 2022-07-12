using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
 
public class PoliceCarSpawner : MonoBehaviour
{
    // public bool singleStreetDemo;
    public int interval = 10;
    public GameObject policeCarPrefab;
    int countCarsPatrolling = 0;
    int maxNumberCarsPatrolling = 1;
    [SerializeField]
    List<GameObject> spawnLocations;


    // Start is called before the first frame update
    void Start()
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("PoliceSpawnLocation").ToList();
        Debug.Log("Spawn locations: " + spawnLocations.Count);
        maxNumberCarsPatrolling = (spawnLocations.Count / 2) + 1;
        interval = Random.Range(5, 10);
        IEnumerator coroutine = spawnPoliceCarAfterTime(interval);
        StartCoroutine(coroutine);
    }

    public void policeCarReachedDestination()
    {
        countCarsPatrolling--;
    }

    private IEnumerator spawnPoliceCarAfterTime(int interval)
    {
        if(spawnLocations.Count == 1)
        {
            Debug.LogError("ERROR: Must have at least two spawn/destination locations for police car!");
            yield break;
        }

        yield return new WaitForSeconds(interval);
        
        //choose random time for next spawn
        interval = Random.Range(5, 20);

        //choose random spawner point to spawn from
        int spawnIndex = Random.Range(0, spawnLocations.Count);
        Debug.Log("Spawn index: " + spawnIndex);

        int destinationIndex = Random.Range(0, spawnLocations.Count);
        Debug.Log("Destination index: " + destinationIndex);

        if(destinationIndex == spawnIndex)
        {
            if(destinationIndex == 0)
            {
                destinationIndex++;
                Debug.Log("Incrementing destinationIndex");
            }
            else
            {
                destinationIndex--;
                Debug.Log("Decrementing destinationIndex");
            }
            
        }
        Debug.Log("Destination index (reworked): " + destinationIndex);

        if(countCarsPatrolling < maxNumberCarsPatrolling)
        {
            countCarsPatrolling++;
            GameObject policeCarToSpawn = GameObject.Instantiate(policeCarPrefab, spawnLocations[spawnIndex].gameObject.transform.position, this.transform.rotation);
            GameObject destination = spawnLocations[destinationIndex].gameObject;
            PoliceCarController carController = policeCarToSpawn.GetComponent<PoliceCarController>();
            carController.ChangeDestination(destination.transform);
            carController.targetCollider = destination.GetComponent<SphereCollider>();
        }

        IEnumerator coroutine = spawnPoliceCarAfterTime(interval);
        StartCoroutine(coroutine);

    }
}
