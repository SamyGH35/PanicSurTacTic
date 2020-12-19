using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    float countdown;
    bool hasExploded = false;
    public GameObject explosionEffect;
    public float radius=5f;
    public float force= 700f;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
        
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f  && !hasExploded){
            Explode();
            hasExploded=true;
        }
    }
    void Explode(){

        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] colliders=Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders){
            if (nearbyObject.name.Contains("PNJ")){
                GameObject capsule = nearbyObject.transform.GetChild(0).gameObject;
                GameObject brasdroit = capsule.transform.GetChild(0).gameObject;
                GameObject brasgauche = capsule.transform.GetChild(1).gameObject;
                List<GameObject> gameObjects = new List<GameObject>{capsule, brasdroit, brasgauche};
                
                foreach (GameObject item in gameObjects)
                {
                    Renderer rend = item.GetComponent<Renderer>();
                    rend.material.SetColor("_Color",Color.red);
                }
             
            }
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null){
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        Destroy(gameObject);        

       

    }
}
