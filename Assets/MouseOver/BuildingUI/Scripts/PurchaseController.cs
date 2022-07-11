using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PurchaseController : MonoBehaviour
{
    public enum PurchaseType {none, speakeasy, distillery}
    public BuildingUICanvasController buildingUICanvasController;
    public TMP_Dropdown buildingTypeDropdown;
    int purchaseType;

    public void changeType()
    {
        buildingUICanvasController.buildingUIObject.currentSelectedBuildingProperties.myDropDown = buildingTypeDropdown;
        buildingUICanvasController.buildingUIObject.currentSelectedBuildingProperties.ChangeType();
    }

    public void purchaseBuilding()
    {
        BuildingProperties buildingProperties = buildingUICanvasController.buildingUIObject.currentSelectedBuildingProperties;
        buildingProperties.PurchaseBuilding();
        this.gameObject.SetActive(!buildingProperties.active);
    }
}