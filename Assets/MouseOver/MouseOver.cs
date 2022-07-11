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
        if (buildingProperties != null)
        {
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
        content.Add("Unpurchased \n");
        content.Add("Upfront Cost: " + buildingProperties.upfrontCost + "\n");
        content.Add("Rent Cost: " + buildingProperties.rentCost + "\n");
        return content;
    }

    List<string> gatherSpeakeasyContent(BuildingProperties buildingProperties)
    {
        List<string> content = new List<string>();
        content.Add("Speakeasy \n");
        content.Add("Drink Price: " + buildingProperties.drinkPrice + "\n");
        return content;
    }

    List<string> gatherDistilleryContent(BuildingProperties buildingProperties)
    {
        List<string> content = new List<string>();
        content.Add("Distillery \n");
        content.Add("Alcohol Stores: " + buildingProperties.alcoholStores + "\n");
        return content;
    }
}
