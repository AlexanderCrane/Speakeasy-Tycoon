using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceCarController : MonoBehaviour
{
    public Transform targetDestination;
    public Collider targetCollider;
    private NavMeshAgent agent;
    private PoliceCarSpawner policeCarSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = targetDestination.position; 
        policeCarSpawner = GameObject.FindGameObjectWithTag("PoliceSpawner").GetComponent<PoliceCarSpawner>();
        Debug.Log("Agent destination: " + agent.destination.ToString());
    }

    public void ChangeDestination(Transform newDestination)
    {
        if(agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        targetDestination = newDestination;
        agent.destination = targetDestination.position; 
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Building")
        {
            BuildingProperties buildingProperties = other.gameObject.GetComponent<BuildingProperties>();
            if(buildingProperties.buildingState == BuildingProperties.BuildingState.distillery || buildingProperties.buildingState == BuildingProperties.BuildingState.speakeasy)
            {
                if(buildingProperties.active)
                {
                    Debug.Log("Alcohol confiscated!");
                    buildingProperties.alcoholStores = 0;
                }
            }
        }
        else if(other == targetCollider)
        {
            policeCarSpawner.policeCarReachedDestination();
            Destroy(this.gameObject);
        }
    }

}
