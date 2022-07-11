using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public BuildingUIObject buildingUIObject;

    public float floatHoverHeight = 20.0f;
    Outline outline;

    void Start()
    {
        outline.enabled = false;
    }

    void IPointerEnterHandler.OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        outline.enabled = true;
    }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        Transform canvasTransform = buildingUIObject.buildingUICanvasTransform;
        Vector3 thisPosition = this.transform.position;
        canvasTransform.gameObject.SetActive(true);
        canvasTransform.transform.position = new Vector3(thisPosition.x, thisPosition.y + floatHoverHeight, thisPosition.z);

        BuildingUIContentController buildingUIContentController = canvasTransform.GetComponent<BuildingUICanvasController>().contentController;
        BuildingProperties buildingProperties = GetComponent<BuildingProperties>();
        buildingUIObject.currentSelectedBuildingProperties = buildingProperties;
        if (buildingProperties != null)
        {
            PurchaseController purchaseController = canvasTransform.GetComponent<BuildingUICanvasController>().purchaseController;
            GameObject purchaseControllerGameObject = purchaseController.gameObject;
            purchaseControllerGameObject.SetActive(!buildingProperties.active);
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

    List<string> gatherContent(BuildingProperties buildingProperties)
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
        content.Add("Drink Price: " + buildingProperties.drinkPrice);
        return content;
    }

    List<string> gatherDistilleryContent(BuildingProperties buildingProperties)
    {
        List<string> content = new List<string>();
        content.Add("Distillery");
        content.Add("Alcohol Stores: " + buildingProperties.alcoholStores);
        return content;
    }
}
