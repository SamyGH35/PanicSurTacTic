using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_pnj : MonoBehaviour
{
    public float zoneEffect = 10f;
    public float zoneSpawnX, zoneSpawnY;
    public int nbr_pnj = 5;
    public GameObject pnj;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < nbr_pnj; i++)
        {
            zoneSpawnX = transform.position.x + Random.Range(-zoneEffect, zoneEffect);
            zoneSpawnY = transform.position.y + Random.Range(-zoneEffect, zoneEffect);
            Instantiate(pnj, new Vector3(zoneSpawnX, 0f, zoneSpawnY), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
