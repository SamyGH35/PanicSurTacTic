using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public AudioClip backButtonSound;
    public AudioSource source;

    public void ChargerScene(string scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1;
    }

    public void AjouterScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
    }

    public void FermerScene(string scene)
    {
        StartCoroutine(WaitForReturn(scene));
    }

    public IEnumerator WaitForReturn(string scene)
    {
        float time = Time.timeScale;
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.075f);
        SceneManager.UnloadSceneAsync(scene);
        Time.timeScale = time;
    }
    
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (SceneManager.GetSceneByName("Options").isLoaded)
            {
                SoundAndReturn("Options");
            }
        }
    }

    void SoundAndReturn(string scene)
    {
        float time = Time.timeScale;
        Time.timeScale = 0.1f;
        source.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
        source.PlayOneShot(backButtonSound);
        SceneManager.UnloadSceneAsync(scene);
        Time.timeScale = time;
    }
}
