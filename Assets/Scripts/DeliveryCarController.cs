using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeliveryCarController : MonoBehaviour
{
    public bool returningToBase = false;
    public Transform targetDestination;
    public GameObject homeBase;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = targetDestination.position; 
    }

    public void ChangeDestination(Transform newDestination)
    {
        targetDestination = newDestination;
        agent.destination = targetDestination.position; 
    }
}
