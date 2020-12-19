using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDController2 : MonoBehaviour
{
    public GameObject Minimap;
    public GameObject Map;

    public GameObject borderGun;
    public Image backgroundGun;
    public Text munGun;
    public GameObject borderGrenade;
    public Image backgroundGrenade;
    public Text munGrenade;
    public GameObject borderTourelle;
    public Image backgroundTourelle;
    public Text munTourelle;

    public AudioClip mapSound;
    public AudioClip changeWeapon;
    public AudioSource source;

    public Slider contamination;
    public Text taux;

    public GameObject perdu;
    public GameObject gagné;

    private bool use1 = true;
    private bool use2 = false;
    private bool use3 = false;
    private bool changes = false;

    public void Contamination()
    {
        taux.text = contamination.value.ToString() + "%";
    }

    void Update()
    {
        if (use1)
        {
            if (Input.mouseScrollDelta.y < 0 && Time.timeScale != 0)
            {
                use1 = false;
                use2 = true;
                changes = true;
            }
            else if (Input.mouseScrollDelta.y > 0 && Time.timeScale != 0)
            {
                use1 = false;
                use3 = true;
                changes = true;
            }
        }
        else if (use2)
        {
            if (Input.mouseScrollDelta.y < 0 && Time.timeScale != 0)
            {
                use2 = false;
                use3 = true;
                changes = true;
            }
            else if (Input.mouseScrollDelta.y > 0 && Time.timeScale != 0)
            {
                use2 = false;
                use1 = true;
                changes = true;
            }
        }
        else if (use3)
        {
            if (Input.mouseScrollDelta.y < 0 && Time.timeScale != 0)
            {
                use3 = false;
                use1 = true;
                changes = true;
            }
            else if (Input.mouseScrollDelta.y > 0 && Time.timeScale != 0)
            {
                use3 = false;
                use2 = true;
                changes = true;
            }
        }
        if ((Input.GetKeyDown("1") || Input.GetKeyDown("[1]")) && Time.timeScale != 0)
        {
            use1 = true;
            use2 = false;
            use3 = false;
            changes = true;
        }
        else if ((Input.GetKeyDown("2") || Input.GetKeyDown("[2]")) && Time.timeScale != 0)
        {
            use1 = false;
            use2 = true;
            use3 = false;
            changes = true;
        }
        else if ((Input.GetKeyDown("3") || Input.GetKeyDown("[3]")) && Time.timeScale != 0)
        {
            use1 = false;
            use2 = false;
            use3 = true;
            changes = true;
        }
        if (use1 && changes)
        {
            use1 = true;
            borderGrenade.SetActive(false);
            backgroundGrenade.color = new Color(1,1,1,0.5f);
            borderTourelle.SetActive(false);
            backgroundTourelle.color = new Color(1, 1, 1, 0.5f);
            borderGun.SetActive(true);
            backgroundGun.color = new Color(1, 1, 1, 1);
            source.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
            source.PlayOneShot(changeWeapon);
            changes = false;
        }
        else if (use2 && changes)
        {
            use2 = true;
            borderGun.SetActive(false);
            backgroundGun.color = new Color(1, 1, 1, 0.5f);
            borderTourelle.SetActive(false);
            backgroundTourelle.color = new Color(1, 1, 1, 0.5f);
            borderGrenade.SetActive(true);
            backgroundGrenade.color = new Color(1, 1, 1, 1);
            source.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
            source.PlayOneShot(changeWeapon);
            changes = false;
        }
        else if (use3 && changes)
        {
            use3 = true;
            borderGun.SetActive(false);
            backgroundGun.color = new Color(1, 1, 1, 0.5f);
            borderGrenade.SetActive(false);
            backgroundGrenade.color = new Color(1, 1, 1, 0.5f);
            borderTourelle.SetActive(true);
            backgroundTourelle.color = new Color(1, 1, 1, 1);
            source.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
            source.PlayOneShot(changeWeapon);
            changes = false;
        }
        if ((Input.GetKeyDown("m") || Input.GetKeyDown("tab")) && Time.timeScale != 0)
        {
            source.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
            source.PlayOneShot(mapSound);
            Minimap.SetActive(false);
            Map.SetActive(true);
        }
        if ((Input.GetKeyUp("m") || Input.GetKeyUp("tab")) && Time.timeScale != 0)
        {
            Map.SetActive(false);
            Minimap.SetActive(true);
        }
        munGun.text = PlayerPrefs.GetInt("MunGun", 0).ToString();
        munGrenade.text = PlayerPrefs.GetInt("MunGrenade", 0).ToString();
        munTourelle.text = PlayerPrefs.GetInt("MunTourelle", 0).ToString();
        if (Input.GetKeyDown("+") || Input.GetKeyDown("[+]"))
        {
            Time.timeScale = 0;
            gagné.SetActive(true);
            Cursor.visible = true;
        }
        if (Input.GetKeyDown("-") || Input.GetKeyDown("[-]"))
        {
            Time.timeScale = 0;
            perdu.SetActive(true);
            Cursor.visible = true;
        }
    }
}
