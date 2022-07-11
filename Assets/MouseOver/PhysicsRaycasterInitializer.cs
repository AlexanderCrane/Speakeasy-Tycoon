using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhysicsRaycasterInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        createPhysicsRaycaster();
    }

    public void createPhysicsRaycaster()
    {
        Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
    }
}
