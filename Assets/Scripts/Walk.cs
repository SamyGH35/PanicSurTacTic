using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walk : MonoBehaviour
{
    public float marge = 2.5f;

    void Start()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 250;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 250, NavMesh.AllAreas);
        Vector3 finalPosition = hit.position;
        GetComponent<NavMeshAgent>().destination = finalPosition;
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, GetComponent<NavMeshAgent>().destination) <= marge)
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