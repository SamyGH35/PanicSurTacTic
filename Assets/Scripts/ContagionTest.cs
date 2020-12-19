using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContagionTest : MonoBehaviour
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
    public float timeBetweenCough = 2.0f;
    private float timePassed = 0.0f;

    private ParticleSystem coughParticles;

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
            timePassed += Time.deltaTime;
            if (timePassed > timeBetweenCough)
            {
                timePassed = 0;
                Cough();
            }
        }
    }

    public void Cough()
    {
        coughParticles.Play();

        Collider[] insideColliders = Physics.OverlapSphere(transform.position, radiusArea);
        foreach (Collider col in insideColliders)
        {
            ContagionTest script = col.transform.gameObject.GetComponent<ContagionTest>();
            if (script && script != this)
            {
                script.GetInfected();
            }
        }
    }

    public void GetInfected()
    {
        if (stade < 3)
            stade++;
    }
}