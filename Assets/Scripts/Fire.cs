using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public Rigidbody projectile;
    public float speed = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody instanciatedProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;

            instanciatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
        }
    }
}
