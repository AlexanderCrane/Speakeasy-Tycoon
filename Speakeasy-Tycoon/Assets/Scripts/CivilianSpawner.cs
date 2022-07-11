using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianSpawner : MonoBehaviour
{
    public Transform civilianTargetDestination;
    public GameObject civilian;
    public Collider triggerToDestroyCivilian;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitToSpawnCivilians());
        triggerToDestroyCivilian = this.GetComponent<SphereCollider>();
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    private IEnumerator waitToSpawnCivilians()
    {
        float timeToWait = Random.Range(2.0f, 5.0f);
        yield return new WaitForSeconds(timeToWait);
        
        GameObject civilianSpawned = GameObject.Instantiate(civilian, this.transform.position, this.transform.rotation);
        civilianSpawned.GetComponent<CivilianController>().targetDestination = civilianTargetDestination;
        StartCoroutine(waitToSpawnCivilians());
    }
}
