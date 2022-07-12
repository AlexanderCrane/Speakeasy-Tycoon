using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PurchaseController : MonoBehaviour
{
    public enum PurchaseType {none, speakeasy, distillery}
    public BuildingUICanvasController buildingUICanvasController;
    public BuildingUIContentController buildingUIContentController;
    public TMP_Dropdown buildingTypeDropdown;

    public GameObject hidePanel;
    public GameObject reopenPanel;

    public BuildingProperties buildingProperties;
    int purchaseType;

    public void changeType()
    {
        buildingProperties.myDropDown = buildingTypeDropdown;
        buildingProperties.ChangeType();
    }

    public void purchaseBuilding()
    {
        buildingProperties.PurchaseBuilding();
        buildingUIContentController.fillContent(buildingProperties.GetComponent<MouseOver>().gatherContent(buildingProperties));
        hidePanel.SetActive(buildingProperties.active);
        this.gameObject.SetActive(!buildingProperties.active);
    }

    public void setInactive()
    {
        buildingProperties.active = false;
    }

    public void setActive()
    {
        buildingProperties.active = true;
    }
}
