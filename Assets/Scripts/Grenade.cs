using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public GameObject explosionEffect;
    public float radius = 5f;
    public float force = 700f;

    public AudioClip shotSound;
    public AudioSource source;

    void Start()
    {
        StartCoroutine(ExplodeAfter(delay));
    }

    IEnumerator ExplodeAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        while (transform.position.y > 1)
        {
            yield return new WaitForSeconds(0.1f);
        }
        Explode();
    }

    void Explode()
    {
        source.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
        source.PlayOneShot(shotSound);
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.name.Contains("PNJ"))
            {
                if (nearbyObject.GetComponent<Contagion>())
                {
                    nearbyObject.GetComponent<Contagion>().HitByGrenade();
                }
            }
        }
        Destroy(gameObject);
    }
}