using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contagion : MonoBehaviour
{
    // Stade de la maladie :
    //      0 -> non contaminé
    //      1 -> porteur sans symptôme
    //      2 -> éternuement à intervalle régulier
    //      3 -> enlève les masques des autres + fuit devant le joueur
    public int stade = 0;

    // Taille de la zone de contamination en sphere autour du tactic
    public float radiusArea = 5.0f;

    // Temps entre 2 éternuements
    public int minTimeBetweenCough = 5;
    public int maxTimeBetweenCough = 20;

    private ParticleSystem coughParticles;

    private bool cough;

    public Material green;
    public Material greenMasked;
    public Material greenColor;

    public Material blue;
    public Material blueMasked;
    public Material blueColor;

    public int time2live = 5;

    private bool masked = false;

    // Start is called before the first frame update
    void Start()
    {
        coughParticles = GetComponent<ParticleSystem>();

        var shape = coughParticles.shape;
        shape.radius = radiusArea;
        var emission = coughParticles.emission;
        emission.enabled = true;
        emission.SetBurst(0, new ParticleSystem.Burst(0.0f, radiusArea * 10));
    }

    // Update is called once per frame
    void Update()
    {
        if (stade > 1)
        {
            if (!cough)
            {
                StartCoroutine(WaitBeforeCought());
            }
        }
        if (masked)
        {
            StartCoroutine(UnMask());
        }
    }

    IEnumerator WaitBeforeCought()
    {
        cough = true;
        yield return new WaitForSeconds(Random.Range(minTimeBetweenCough, maxTimeBetweenCough));
        Cough();
    }

    void Cough()
    {
        coughParticles.Play();

        Collider[] insideColliders = Physics.OverlapSphere(transform.position, radiusArea);
        foreach (Collider col in insideColliders)
        {
            //ajouter Random.Range() pour ne pas être contaminé forcement à chaque fois
            Contagion script = col.transform.gameObject.GetComponent<Contagion>();
            if (script && script != this)
            {
                script.GetInfected();
            }
        }
        cough = false;
    }

    void GetInfected()
    {
        if (stade > 1)
        {
            transform.Find("Capsule").GetComponent<MeshRenderer>().material = green;
            transform.Find("Capsule").transform.Find("Bras gauche").GetComponent<MeshRenderer>().material = greenColor;
            transform.Find("Capsule").transform.Find("Bras droit").GetComponent<MeshRenderer>().material = greenColor;
        }
        if (stade < 3)
        {
            stade++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Mask(Clone)")
        {
            if (transform.Find("Capsule").GetComponent<MeshRenderer>().material.name == "Blue Material (Instance)")
            {
                transform.Find("Capsule").GetComponent<MeshRenderer>().material = blueMasked;
                masked = true;
            }
            if (transform.Find("Capsule").GetComponent<MeshRenderer>().material.name == "Green Material (Instance)")
            {
                transform.Find("Capsule").GetComponent<MeshRenderer>().material = greenMasked;
                masked = true;
            }
        }
    }

    IEnumerator UnMask()
    {
        masked = false;
        yield return new WaitForSeconds(time2live);
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