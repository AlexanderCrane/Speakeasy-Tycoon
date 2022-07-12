using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    GameObject buildingCanvas;
    public float floatHoverHeight = 18.0f;
    Outline outline;

    private PurchaseController purchaseController;
    private BuildingUIContentController buildingUIContentController;

    void Start()
    {
        outline.enabled = false;
        buildingCanvas = GameObject.Find("BuildingCanvas");
    }

    void Update()
    {
        if (buildingUIContentController != null && purchaseController != null && purchaseController.buildingProperties == GetComponent<BuildingProperties>())
        {
            buildingUIContentController.fillContent(gatherContent(purchaseController.buildingProperties));
        }
    }

    void IPointerEnterHandler.OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        outline.enabled = true;
    }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Transform canvasTransform = buildingCanvas.transform;
        Vector3 thisPosition = this.transform.position;
        canvasTransform.gameObject.SetActive(true);
        canvasTransform.transform.position = new Vector3(thisPosition.x, thisPosition.y + floatHoverHeight, thisPosition.z);

        BuildingUIContentController buildingUIContentController = canvasTransform.GetComponent<BuildingUICanvasController>().contentController;
        BuildingProperties buildingProperties = GetComponent<BuildingProperties>();
        if (buildingProperties != null)
        {
            PurchaseController purchaseController = canvasTransform.GetComponent<BuildingUICanvasController>().purchaseController;
            GameObject purchaseControllerGameObject = purchaseController.gameObject;
            this.purchaseController = purchaseController;
            this.buildingUIContentController = buildingUIContentController;
            purchaseController.buildingProperties = buildingProperties;
            purchaseController.hidePanel.SetActive(buildingProperties.active);
            purchaseController.reopenPanel.SetActive(!buildingProperties.active && buildingProperties.buildingState != BuildingProperties.BuildingState.unpurchased);
            purchaseControllerGameObject.SetActive(!buildingProperties.active && buildingProperties.buildingState == BuildingProperties.BuildingState.unpurchased);
            purchaseController.buildingTypeDropdown.value = (int)buildingProperties.buildingState;
            buildingProperties.myDropDown = purchaseController.buildingTypeDropdown;
            buildingUIContentController.fillContent(gatherContent(buildingProperties));
        }
        else
        {
            Debug.LogError(this.gameObject.name + " has an outline script but not a BuildingProperties component");
            buildingUIContentController.fillContent(new List<string>{"Has an outline, but not building properties."});
        }
    }

    void IPointerExitHandler.OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        outline.enabled = false;
    }

    public void setOutline(Outline outline)
    {
        this.outline = outline;
    }

    public List<string> gatherContent(BuildingProperties buildingProperties)
    {
        switch (buildingProperties.buildingState)
        {
            case BuildingProperties.BuildingState.unpurchased:
                return gatherUnpurchasedContent(buildingProperties);
            case BuildingProperties.BuildingState.speakeasy:
                return gatherSpeakeasyContent(buildingProperties);
            case BuildingProperties.BuildingState.distillery:
                return gatherDistilleryContent(buildingProperties);
            default:
                Debug.Log("unexpected building state error");
                break;
        }

        return new List<string>{"error gathering building properties content"};
    }

    List<string> gatherUnpurchasedContent(BuildingProperties buildingProperties)
    {
        List<string> content = new List<string>();
        content.Add("Unpurchased");
        content.Add("Upfront Cost: " + buildingProperties.upfrontCost);
        content.Add("Rent Cost: " + buildingProperties.rentCost);
        return content;
    }

    List<string> gatherSpeakeasyContent(BuildingProperties buildingProperties)
    {
        List<string> content = new List<string>();
        content.Add("Speakeasy");
        content.Add("Drink Price: $" + buildingProperties.drinkPrice);
        content.Add("Alcohol Stores: " + buildingProperties.alcoholStores);
        content.Add("Total Profit: $" + buildingProperties.moneyMadeSoFar);
        return content;
    }

    List<string> gatherDistilleryContent(BuildingProperties buildingProperties)
    {
        List<string> content = new List<string>();
        content.Add("Distillery");
        content.Add("Alcohol Stores: " + buildingProperties.alcoholStores);
        content.Add("Number of cars: " + buildingProperties.numCars);
        content.Add("Cars in use: " + buildingProperties.numCarsBusy);
        content.Add("Alcohol Produced: " + buildingProperties.alcoholProducedSoFar);
        return content;
    }
}
