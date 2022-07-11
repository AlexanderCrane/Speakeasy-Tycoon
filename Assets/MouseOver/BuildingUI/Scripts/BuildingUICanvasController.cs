using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUICanvasController : MonoBehaviour
{
    public BuildingUIContentController contentController;
    public BuildingUIObject buildingUIObject;
    public PurchaseController purchaseController;

    // Start is called before the first frame update
    void Start()
    {
        buildingUIObject.buildingUICanvasTransform = this.transform;
        GetComponent<Canvas>().worldCamera = Camera.main;
        faceCamera();
        this.gameObject.SetActive(false);
    }

    public void faceCamera()
    {
        // this.transform.rotation = Quaternion.LookRotation(this.transform.position - Camera.main.transform.position);
        this.transform.rotation = Quaternion.Euler(45, 45, 0); //orthographic rotation
    }
}
