using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walk : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 250;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 250, NavMesh.AllAreas);
        Vector3 finalPosition = hit.position;
        GetComponent<NavMeshAgent>().destination = finalPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<NavMeshAgent>().destination == transform.position)
        {
            Vector3 randomDirection = Random.insideUnitSphere * 250;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 250, NavMesh.AllAreas);
            Vector3 finalPosition = hit.position;
            GetComponent<NavMeshAgent>().destination = finalPosition;
        }
    }
}
