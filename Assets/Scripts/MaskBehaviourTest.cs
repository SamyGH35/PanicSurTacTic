using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskBehaviourTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit " + other.name);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
