using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Contagion : MonoBehaviour
{
    public static int nbInstance = 0;
    public static int contamination = 0;
    private Slider sliderContamination;
    public bool inTuto = false;

    // Stade de la maladie :
    //      0 -> non contaminé
    //      1 -> porteur sans symptôme
    //      2 -> éternuement à intervalle régulier
    public int stade = 0;

    // Taille de la zone de contamination en sphere autour du tactic
    public float radiusArea = 5.0f;

    // 1 chance sur 4
    public int chanceToBeInfectedMasked = 4;
    // 1 chance sur 2
    public int chanceToBeInfected = 2;
    // 1 chance sur 4
    public int chanceToInfectMasked = 4;
    // 1 chance sur 2
    public int chanceToInfect = 2;

    // Temps entre 2 éternuements
    public int minTimeBetweenCough = 5;
    public int maxTimeBetweenCough = 20;

    public ParticleSystem feedBack;
    private ParticleSystem coughParticles;

    private bool cough;

    public Material green;
    public Material greenMasked;
    public Material greenColor;

    public Material blue;
    public Material blueMasked;
    public Material blueColor;

    public int time2live = 30;

    private int maskCount = 0;
    private bool masked = false;
    private bool wearingMask = false;

    private bool hit = false;

    public bool IsMasked()
    {
        return wearingMask;
    }

    public void HitByGrenade()
    {
        if (!wearingMask)
        {
            transform.Find("Capsule").GetComponent<MeshRenderer>().material = blue;
        }
        else
        {
            transform.Find("Capsule").GetComponent<MeshRenderer>().material = blueMasked;
        }
        transform.Find("Capsule").transform.Find("Bras gauche").GetComponent<MeshRenderer>().material = blueColor;
        transform.Find("Capsule").transform.Find("Bras droit").GetComponent<MeshRenderer>().material = blueColor;
        hit = true;
        if (stade > 0)
        {
            MinusContamination();
            stade = 0;
        }
    }

    public bool GrenadeHit()
    {
        return hit;
    }

    void Start()
    {
        nbInstance += 1;
        coughParticles = GetComponent<ParticleSystem>();

        var shape = coughParticles.shape;
        shape.radius = radiusArea;
        var emission = coughParticles.emission;
        emission.enabled = true;
        emission.SetBurst(0, new ParticleSystem.Burst(0.0f, radiusArea * 10));

        if (!inTuto)
        {
            sliderContamination = GameObject.Find("/Canvas/Contamination/Slider").GetComponent<Slider>();
        }

        if (stade > 0)
        {
            PlusContamination();
        }
    }

    void Update()
    {
        if (stade > 1)
        {
            if (!cough)
            {
                cough = true;
                StartCoroutine(WaitBeforeCought());
            }
        }
        if (masked)
        {
            masked = false;
            StartCoroutine(UnMask());
        }
    }

    public int getStade()
    {
        return this.stade;
    }

    IEnumerator WaitBeforeCought()
    {
        yield return new WaitForSeconds(Random.Range(minTimeBetweenCough, maxTimeBetweenCough));
        if (stade > 1)
        {
            Cough();
        }
    }

    void Cough()
    {
        if (masked)
        {
            coughParticles.emission.SetBurst(0, new ParticleSystem.Burst(0.0f, radiusArea * 1));
        }
        else
        {
            coughParticles.emission.SetBurst(0, new ParticleSystem.Burst(0.0f, radiusArea * 10));
        }
        coughParticles.Play();

        Collider[] insideColliders = Physics.OverlapSphere(transform.position, radiusArea);
        foreach (Collider col in insideColliders)
        {
            Contagion script = col.transform.gameObject.GetComponent<Contagion>();
            if (script && script != this)
            {
                if (masked)
                {
                    // Masqué, on n'infecte 1 fois sur 4 en moyenne
                    if (Random.Range(0, chanceToInfectMasked) == 0)
                        script.GetInfected();
                }
                else
                {
                    // Non-masqué, on n'infecte 1 fois sur 2 en moyenne
                    if (Random.Range(0, chanceToInfect) == 0)
                        script.GetInfected();
                }
            }
        }
        cough = false;
    }

    void GetInfected()
    {
        int random = 0;
        if (masked)
            random = Random.Range(0, chanceToBeInfectedMasked); // Si masqué, 1 chance sur 4 d'être infecté
        else
            random = Random.Range(0, chanceToBeInfected); // Sinon, 1 chance sur 2

        if (random == 0)
        {
            if (stade < 2)
            {
                stade++;
                if (stade < 2)
                {
                    PlusContamination();
                }
            }
            if (stade > 0)
            {
                if (!wearingMask)
                {
                    transform.Find("Capsule").GetComponent<MeshRenderer>().material = green;
                }
                else
                {
                    transform.Find("Capsule").GetComponent<MeshRenderer>().material = greenMasked;
                }
                transform.Find("Capsule").transform.Find("Bras gauche").GetComponent<MeshRenderer>().material = greenColor;
                transform.Find("Capsule").transform.Find("Bras droit").GetComponent<MeshRenderer>().material = greenColor;
            }
        }
    }

    private void PlusContamination()
    {
        contamination++;

        if (sliderContamination)
        {
            sliderContamination.value = contamination * 100 / nbInstance;
        }
    }

    private void MinusContamination()
    {
        contamination--;

        if (sliderContamination)
        {
            sliderContamination.value = contamination * 100 / nbInstance;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Mask(Clone)")
        {
            if (stade<1)
            {
                transform.Find("Capsule").GetComponent<MeshRenderer>().material = blueMasked;
            }
            else
            {
                transform.Find("Capsule").GetComponent<MeshRenderer>().material = greenMasked;
            }
            masked = true;
            wearingMask = true;
            maskCount++;
            if (feedBack && masked)
            {
                ParticleSystem ps = Instantiate(feedBack);
                ps.transform.position = transform.position;
                ps.transform.parent = transform;
            }
        }
    }

    IEnumerator UnMask()
    {
        yield return new WaitForSeconds(time2live);
        if (maskCount<=1)
        {
            wearingMask = false;
            if (stade < 1)
            {
                transform.Find("Capsule").GetComponent<MeshRenderer>().material = blue;
            }
            else
            {
                transform.Find("Capsule").GetComponent<MeshRenderer>().material = green;
            }
        }
        maskCount--;
    }
}