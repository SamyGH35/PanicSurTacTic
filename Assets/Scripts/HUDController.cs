using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    public GameObject Minimap;
    public GameObject Map;

    public GameObject gun;
    public GameObject grenade;
    public GameObject tourelle;
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
    public Slider contaminationMaxValue;
    public Text taux;

    public int secondsToWin = 60;
    public int maxContamination = 75;
    public GameObject perdu;
    public Text statPerdu;
    public GameObject gagné;
    public Text statGagné;
    public Text highScoreWin;
    public Text scoreWin;
    public Text highScoreLose;
    public Text scoreLose;
    private bool once = true;

    private bool use1 = true;
    private bool use2 = false;
    private bool use3 = false;
    private bool changes = false;

    private float time;

    public void Contamination()
    {
        taux.text = contamination.value.ToString() + "%";
    }

    void Start()
    {
        contaminationMaxValue.value = maxContamination;
        taux.text = contamination.value.ToString() + "%";
        PlayerPrefs.SetInt("Masques tirés", 0);
        PlayerPrefs.SetInt("Grenades lancées", 0);
        PlayerPrefs.SetInt("Tourelles déployées", 0);
        this.time = 0f;
    }

    void Update()
    {
        this.time += Time.deltaTime;
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
            gun.SetActive(true);
            grenade.SetActive(false);
            tourelle.SetActive(false);
            tourelle.GetComponent<TurretSpawner>().unUse();
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
            gun.SetActive(false);
            gun.GetComponent<GunBehaviour>().unUse();
            grenade.SetActive(true);
            tourelle.SetActive(false);
            tourelle.GetComponent<TurretSpawner>().unUse();
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
            gun.SetActive(false);
            gun.GetComponent<GunBehaviour>().unUse();
            grenade.SetActive(false);
            tourelle.SetActive(true);
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
        if (contamination.value >= maxContamination && Time.timeScale != 0 && once)
        {
            once = false;
            Time.timeScale = 0;
            string hs = PlayerPrefs.GetString("HighScore", "0:0");
            string s = (int)(this.time / 60) + ":" + ((int)(this.time % 60) < 10 ? "0" + (int)(this.time % 60) : "" + (int)(this.time % 60));
            if(int.Parse(s.Split(':')[0]) >= int.Parse(hs.Split(':')[0]))
            {
                if (int.Parse(s.Split(':')[1]) > int.Parse(hs.Split(':')[1]))
                {
                    highScoreWin.text = "NEW HIGH SCORE :";
                    highScoreWin.color = Color.red;
                    scoreWin.text = s;
                    scoreWin.color = Color.red;
                    highScoreLose.text = "NEW HIGH SCORE :";
                    highScoreLose.color = Color.red;
                    scoreLose.text = s;
                    scoreLose.color = Color.red;
                    PlayerPrefs.SetString("HighScore", s);
                }
                else
                {
                    highScoreWin.text = "HIGH SCORE :";
                    highScoreWin.color = Color.grey;
                    scoreWin.text = hs;
                    scoreWin.color = Color.grey;
                    highScoreLose.text = "HIGH SCORE :";
                    highScoreLose.color = Color.grey;
                    scoreLose.text = hs;
                    scoreLose.color = Color.grey;
                }
            }
            else
            {
                highScoreWin.text = "HIGH SCORE :";
                highScoreWin.color = Color.grey;
                scoreWin.text = hs;
                scoreWin.color = Color.grey;
                highScoreLose.text = "HIGH SCORE :";
                highScoreLose.color = Color.grey;
                scoreLose.text = hs;
                scoreLose.color = Color.grey;
            }
            PlayerPrefs.SetInt("Fragments de virus récoltés", (int)(this.time / 5) + 25 + (PlayerPrefs.GetInt("Grenades lancées", 15) < 15 ? 15 - PlayerPrefs.GetInt("Grenades lancées", 15) * 5 : 0) + (PlayerPrefs.GetInt("Tourelles déployées", 10) < 10 ? 10 - PlayerPrefs.GetInt("Tourelles déployées", 10) * 5 : 0));
            if (this.time >= secondsToWin)
            {
                this.statGagné.text = "Masques tirés : " + PlayerPrefs.GetInt("Masques tirés", 0) + System.Environment.NewLine +
                "Grenades lancées : " + PlayerPrefs.GetInt("Grenades lancées", 0) + System.Environment.NewLine +
                "Tourelles déployées : " + PlayerPrefs.GetInt("Tourelles déployées", 0) + System.Environment.NewLine +
                "Durée de la partie : " + s + System.Environment.NewLine +
                "Fragments de virus récoltés : " + PlayerPrefs.GetInt("Fragments de virus récoltés", 0);
                PlayerPrefs.SetInt("FragVirus", PlayerPrefs.GetInt("FragVirus", 0) + PlayerPrefs.GetInt("Fragments de virus récoltés", 0));
                gagné.SetActive(true);
                Cursor.visible = true;
            }
            else
            {
                this.statPerdu.text = "Masques tirés : " + PlayerPrefs.GetInt("Masques tirés", 0) + System.Environment.NewLine +
                "Grenades lancées : " + PlayerPrefs.GetInt("Grenades lancées", 0) + System.Environment.NewLine +
                "Tourelles déployées : " + PlayerPrefs.GetInt("Tourelles déployées", 0) + System.Environment.NewLine +
                "Durée de la partie : " + s + System.Environment.NewLine +
                "Fragments de virus récoltés : " + PlayerPrefs.GetInt("Fragments de virus récoltés", 0);
                PlayerPrefs.SetInt("FragVirus", PlayerPrefs.GetInt("FragVirus", 0) + PlayerPrefs.GetInt("Fragments de virus récoltés", 0));
                perdu.SetActive(true);
                Cursor.visible = true;
            }
        }
    }
}