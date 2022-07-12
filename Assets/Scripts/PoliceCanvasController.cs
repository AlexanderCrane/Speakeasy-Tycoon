using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.LookAt(Camera.main.transform);
        this.transform.rotation = Quaternion.Euler(45,45,0);
    }
}
