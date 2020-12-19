using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Accueil : MonoBehaviour
{
    public Text bonjour;
    public GameObject demanderNom;
    public InputField nom;
    public GameObject continuer;

    public AudioSource sourceMusiques;
    public AudioSource sourceSons;
    public AudioClip buttonSound;

    public GameObject player;
    public GameObject epaule;
    public GameObject blue;
    public GameObject green;
    public GameObject blueMasked;
    public GameObject greenMasked;

    private bool wait = false;

    void Start()
    {
        sourceMusiques.volume = (float)PlayerPrefs.GetInt("VolumeMusiques") / 100;
        if (PlayerPrefs.GetString("Nom", "") == "")
        {
            continuer.SetActive(false);
            demanderNom.SetActive(true);
            wait = true;
        }
        else
        {
            bonjour.text = "Bonjour " + PlayerPrefs.GetString("Nom", "");
        }
    }

    public void Nom()
    {
        if(nom.text != "")
        {
            PlayerPrefs.SetString("Nom", nom.text);
            bonjour.text = "Bonjour " + PlayerPrefs.GetString("Nom", nom.text);
            demanderNom.SetActive(false);
            continuer.SetActive(true);
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.25f);
        wait = false;
    }

    private bool coucou = true;
    private bool salut = false;
    private bool ok = true;
    private bool go = true;
    private int masked = 2;
    
    void Update()
    {
        if (continuer.activeInHierarchy && Input.anyKeyDown && !wait)
        {
            sourceSons.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
            sourceSons.PlayOneShot(buttonSound);
            SceneManager.LoadScene("MenuPrincipal");
        }
        continuer.GetComponent<Text>().color = Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time * 0.5f, 1));

        if (coucou)
        {
            if (ok)
            {
                if (epaule.transform.eulerAngles.z <= 149 && !salut)
                {
                    //epaule.transform.rotation = Quaternion.Euler(0, 0, 15 + Time.time * 100);
                    epaule.transform.rotation = Quaternion.Slerp(epaule.transform.rotation, Quaternion.Euler(0, 0, 150), Time.deltaTime * 7.5f);
                }
                else
                {
                    salut = true;
                    ok = false;
                    StartCoroutine(WaitForStopSalut());
                }
            }
        }
        else
        {
            if (epaule.transform.eulerAngles.z >= 16)
            {
                //epaule.transform.rotation = Quaternion.Euler(0, 0, 150 - Time.time * 100);
                epaule.transform.rotation = Quaternion.Slerp(epaule.transform.rotation, Quaternion.Euler(0, 0, 15), Time.deltaTime * 7.5f);
            }
            else
            {
                StartCoroutine(Coucou());
            }
        }

        if (salut)
        {
            epaule.transform.rotation = Quaternion.Slerp(epaule.transform.rotation, Quaternion.Euler(0, 0, 150 + Mathf.PingPong(Time.time*50, 15)), Time.time*50);
        }

        if (go)
        {
            StartCoroutine(Run());
        }
        else
        {
            if (masked==1)
            {
                blueMasked.transform.position += Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(2.5f, 0, 0), Time.deltaTime);
                greenMasked.transform.position += Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(2.5f, 0, 0), Time.deltaTime);
                if (greenMasked.transform.position.x > 10)
                {
                    blueMasked.transform.position = new Vector3(-10, 1, 50);
                    greenMasked.transform.position = new Vector3(-13, 1, 50);
                    go = true;
                }
            }
            else if(masked==0)
            {
                blue.transform.position += Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(2.5f, 0, 0), Time.deltaTime);
                green.transform.position += Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(2.5f, 0, 0), Time.deltaTime);
                if (green.transform.position.x > 10)
                {
                    blue.transform.position = new Vector3(-10, 1, 25);
                    green.transform.position = new Vector3(-12, 1, 25);
                    go = true;
                }
            }
        }
    }

    IEnumerator Coucou()
    {
        coucou = true;
        yield return new WaitForSeconds(Random.Range(2.5f, 7.5f));
        ok = true;
    }

    IEnumerator WaitForStopSalut()
    {
        yield return new WaitForSeconds(Random.Range(2.5f, 7.5f));
        salut = false;
        coucou = false;
    }

    IEnumerator Run()
    {
        go = false;
        masked = 2;
        yield return new WaitForSeconds(Random.Range(0, 5));
        masked = (int)Random.Range(0.5f, 1.5f);
        if (masked == 0)
        {
            yield return new WaitForSeconds(Random.Range(0, 0.5f));
            blue.transform.position += new Vector3(0, 1 - blue.transform.position.y, 0);
            yield return new WaitForSeconds(Random.Range(0, 0.5f));
            green.transform.position += new Vector3(0, 1 - green.transform.position.y, 0);
        }
        else if (masked == 1)
        {
            yield return new WaitForSeconds(Random.Range(0, 0.5f));
            blueMasked.transform.position += new Vector3(0, 1 - blueMasked.transform.position.y, 0);
            yield return new WaitForSeconds(Random.Range(0, 0.5f));
            greenMasked.transform.position += new Vector3(0, 1 - greenMasked.transform.position.y, 0);
        }
    }
}
