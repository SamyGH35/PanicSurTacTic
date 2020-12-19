using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_pnjTest : MonoBehaviour
{
    public int zoneEffect = 10;
    public int nbPnjBleu = 20;
    public int nbPnjVert = 5;
    public GameObject pnjBleu;
    public GameObject pnjVert;

    private float spawnX;
    private float spawnZ;

    public double nombre_vague = 5;
    public int compteur_max = 2000;
    public double acceleration = 1.2f;
    int compteur = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < nbPnjBleu; i++)
        {
            spawnX = transform.position.x + Random.Range(-zoneEffect, zoneEffect);
            spawnZ = transform.position.z + Random.Range(-zoneEffect, zoneEffect);
            Instantiate(pnjBleu, new Vector3(spawnX, 0, spawnZ), Quaternion.identity /*Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0))*/ );
        }
        for (int i = 0; i < nbPnjVert; i++)
        {
            spawnX = transform.position.x + Random.Range(-zoneEffect, zoneEffect);
            spawnZ = transform.position.z + Random.Range(-zoneEffect, zoneEffect);
            Instantiate(pnjVert, new Vector3(spawnX, 0, spawnZ), Quaternion.identity /*Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0))*/ );
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (compteur >= compteur_max)
        {
            compteur = 0;

            nombre_vague = nombre_vague * acceleration;

            for (int i = 0; i < nombre_vague; i++)
            {
                spawnX = transform.position.x + Random.Range(-zoneEffect, zoneEffect);
                spawnZ = transform.position.z + Random.Range(-zoneEffect, zoneEffect);
                Instantiate(pnjBleu, new Vector3(spawnX, 0f, spawnZ), Quaternion.identity);
            }
        }
        else
        {
            compteur = compteur + 1;
        }
    }
}
