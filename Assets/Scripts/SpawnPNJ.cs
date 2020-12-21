using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPNJ : MonoBehaviour
{
    public int zoneEffect = 10;
    public int nbPnjBleu = 4;
    public int nbPnjVert = 1;
    public GameObject pnjBleu;
    public GameObject pnjVert;

    private float spawnX;
    private float spawnZ;

    public int nbVagues = 5;
    public int tempsEntreVagues = 30;

    void Start()
    {
        Time.timeScale = 0;
        StartCoroutine(Vagues());
    }

    IEnumerator Vagues()
    {
        for(int i=0; i < nbVagues; i++)
        {
            while (Time.timeScale < 0.5f)
            {
                yield return new WaitForSeconds(0.01f);
            }
            for (int j = 0; j < nbPnjBleu; j++)
            {
                spawnX = transform.position.x + Random.Range(-zoneEffect, zoneEffect);
                spawnZ = transform.position.z + Random.Range(-zoneEffect, zoneEffect);
                Instantiate(pnjBleu, new Vector3(spawnX, 0, spawnZ), Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
            }
            for (int j = 0; j < nbPnjVert; j++)
            {
                spawnX = transform.position.x + Random.Range(-zoneEffect, zoneEffect);
                spawnZ = transform.position.z + Random.Range(-zoneEffect, zoneEffect);
                Instantiate(pnjVert, new Vector3(spawnX, 0, spawnZ), Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
            }
            yield return new WaitForSeconds(tempsEntreVagues);
        }
    }
}