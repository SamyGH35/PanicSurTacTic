using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearMask : MonoBehaviour
{
    public Material green;
    public Material greenMasked;
    public Material greenColor;

    public Material blue;
    public Material blueMasked;
    public Material blueColor;

    public ParticleSystem feedBack;

    public int time2live=5;

    private bool masked = false;
    private bool wearingMask = false;

    private bool hit = false;

    public bool IsMasked()
    {
        return wearingMask;
    }

    public void HitByGrenade()
    {
        if (transform.Find("Capsule").GetComponent<MeshRenderer>().material.name == "Green Material (Instance)")
        {
            transform.Find("Capsule").GetComponent<MeshRenderer>().material = blue;
        }
        else if (transform.Find("Capsule").GetComponent<MeshRenderer>().material.name == "Green masked Material (Instance)")
        {
            transform.Find("Capsule").GetComponent<MeshRenderer>().material = blueMasked;
        }
        transform.Find("Capsule").transform.Find("Bras gauche").GetComponent<MeshRenderer>().material = blueColor;
        transform.Find("Capsule").transform.Find("Bras droit").GetComponent<MeshRenderer>().material = blueColor;
        hit = true;
    }

    public bool GrenadeHit()
    {
        return hit;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Mask(Clone)")
        {
            if (transform.Find("Capsule").GetComponent<MeshRenderer>().material.name == "Blue Material (Instance)")
            {
                transform.Find("Capsule").GetComponent<MeshRenderer>().material = blueMasked;
                masked = true;
                wearingMask = true;
            }
            if (transform.Find("Capsule").GetComponent<MeshRenderer>().material.name == "Green Material (Instance)")
            {
                transform.Find("Capsule").GetComponent<MeshRenderer>().material = greenMasked;
                masked = true;
                wearingMask = true;
            }
            if (feedBack && masked)
            {
                ParticleSystem ps = Instantiate(feedBack);
                ps.transform.position = transform.position;
                ps.transform.parent = transform;
            }
        }
    }

    void Update()
    {
        if (masked)
        {
            masked = false;
            StartCoroutine(UnMask());
        }
    }

    IEnumerator UnMask()
    {
        yield return new WaitForSeconds(time2live);
        wearingMask = false;
        if (transform.Find("Capsule").GetComponent<MeshRenderer>().material.name == "Blue masked Material (Instance)")
        {
            transform.Find("Capsule").GetComponent<MeshRenderer>().material = blue;
        }
        if (transform.Find("Capsule").GetComponent<MeshRenderer>().material.name == "Green masked Material (Instance)")
        {
            transform.Find("Capsule").GetComponent<MeshRenderer>().material = green;
        }
    }
}
