using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public AudioClip backButtonSound;
    public AudioClip loadPauseSound;
    public AudioSource source;

    public GameObject pause;
    public GameObject positionnement;

    public void Retour()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!positionnement.activeInHierarchy)
        {
            if (Input.GetKeyDown("escape") && !pause.activeInHierarchy && Time.timeScale != 0)
            {
                source.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
                source.PlayOneShot(loadPauseSound);
                Time.timeScale = 0;
                pause.SetActive(true);
                Cursor.visible = true;
            }
            else if (Input.GetKeyDown("escape") && !SceneManager.GetSceneByName("Options").isLoaded)
            {
                source.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
                source.PlayOneShot(backButtonSound);
                pause.SetActive(false);
                Time.timeScale = 1;
                Cursor.visible = false;
            }
        }
    }
}
