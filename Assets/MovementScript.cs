using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    
    public Transform target;
    public float rotateSpeed ;

    
    void Update()
    {
        transform.RotateAround(target.position, target.up, Time.deltaTime * rotateSpeed);

        
    }
}
