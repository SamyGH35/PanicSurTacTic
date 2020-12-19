using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public InputField nom;
    public Slider sensibilite;
    public Toggle inversion;
    public Slider musiques;
    public Slider sons;

    void Start()
    {
        nom.text = PlayerPrefs.GetString("Nom", "");
        sensibilite.value = PlayerPrefs.GetInt("Sensibilite", 10);
        inversion.isOn = PlayerPrefs.GetInt("Inversion", -1) == 1;
        musiques.value = PlayerPrefs.GetInt("VolumeMusiques", 100);
        sons.value = PlayerPrefs.GetInt("VolumeSons", 100);
    }

    public void Nom()
    {
        if (nom.text != "")
        {
            PlayerPrefs.SetString("Nom", nom.text);
        }
        else
        {
            nom.text = PlayerPrefs.GetString("Nom", nom.text);
        }
    }

    public void Sensibilite()
    {
        PlayerPrefs.SetInt("Sensibilite", (int)sensibilite.value);
    }

    public void Inversion()
    {
        PlayerPrefs.SetInt("Inversion", (inversion.isOn ? 1 : -1));
    }

    public void Musiques()
    {
        PlayerPrefs.SetInt("VolumeMusiques", (int)musiques.value);
    }

    public void Sons()
    {
        PlayerPrefs.SetInt("VolumeSons", (int)sons.value);
    }

    public void Retour()
    {
        StartCoroutine(WaitForReturn());
    }

    IEnumerator WaitForReturn()
    {
        float time = Time.timeScale;
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.075f);
        SceneManager.UnloadSceneAsync("Options");
        Time.timeScale = time;
    }
}