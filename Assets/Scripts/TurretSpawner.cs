using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    public GameObject turret;
    public Transform spawner;
    public GameObject tourelleEnMain;

    public float timeBetween = 0.5f;

    public AudioClip shotSound;
    public AudioSource source;

    private bool throwing = false;
    private bool once = false;

    void Update()
    {
        if (PlayerPrefs.GetInt("MunTourelle", 0) > 0 && !throwing)
        {
            tourelleEnMain.SetActive(true);
            if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
            {
                throwing = true;
                once = true;
            }
        }
        else if (!throwing)
        {
            tourelleEnMain.SetActive(false);
        }
        if (throwing && once)
        {
            once = false;
            PutTurret();
        }
    }

    private void PutTurret()
    {
        RaycastHit hit;
        if (Physics.Raycast(spawner.position, Vector3.down, out hit) && hit.collider.gameObject.name.Length >= 7 &&
            (hit.collider.gameObject.name.Substring(0, 4) == "Road" || hit.collider.gameObject.name.Substring(0, 5) == "Grass" || hit.collider.gameObject.name.Substring(0, 7) == "Terrain"))
        {
            source.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
            source.PlayOneShot(shotSound);
            PlayerPrefs.SetInt("MunTourelle", PlayerPrefs.GetInt("MunTourelle", 0) - 1);
            PlayerPrefs.SetInt("Tourelles déployées", PlayerPrefs.GetInt("Tourelles déployées", 0) + 1);
            Instantiate(turret, new Vector3(hit.point.x, hit.point.y, hit.point.z), spawner.rotation);
            tourelleEnMain.SetActive(false);
            StartCoroutine(Stop());
        }
        else
        {
            throwing = false;
        }
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(timeBetween);
        if (PlayerPrefs.GetInt("MunTourelle", 0) > 0)
        {
            tourelleEnMain.SetActive(true);
        }
        throwing = false;
    }
}
