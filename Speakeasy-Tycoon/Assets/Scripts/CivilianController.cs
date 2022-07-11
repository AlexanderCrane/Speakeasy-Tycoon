using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CivilianController : MonoBehaviour
{
    public Transform targetDestination;
    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = targetDestination.position; 
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.transform == targetDestination)
        {
            Destroy(this.gameObject);
        }    
    }
}
