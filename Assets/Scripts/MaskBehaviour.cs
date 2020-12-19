using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Length>=5 && other.name.Substring(0, 4) != "Trig" && other.name.Substring(0, 5) != "Stock")
        {
            //Debug.Log("hit trigger " + other.name);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("hit collider " + other.gameObject.name);
        Destroy(gameObject);
    }
}