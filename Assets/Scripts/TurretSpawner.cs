using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    public GameObject turret;
    public Transform spawner;
    public int load = 3;

    public float timeBetween = 0.5f;
    private float timePassed = 0.0f;

    void Start()
    {
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        load = PlayerPrefs.GetInt("MunTourelle");

        if (Input.GetButton("Fire1") && timePassed > timeBetween && load > 0)
        {
            PutTurret();
            timePassed = 0.0f;
        }
    }

    private void PutTurret()
    {
        load--;
        PlayerPrefs.SetInt("MunTourelle", load);

        GameObject newTurret = Instantiate(turret);
        newTurret.transform.position = spawner.position;
    }
}
