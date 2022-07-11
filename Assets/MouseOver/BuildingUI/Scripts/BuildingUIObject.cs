using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingUIData", menuName = "ScriptableObjects/BuildingUIScriptableObject", order = 1)]
public class BuildingUIObject : ScriptableObject
{
    public Transform buildingUICanvasTransform;
}
