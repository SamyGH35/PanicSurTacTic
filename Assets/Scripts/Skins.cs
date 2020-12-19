using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Skins : MonoBehaviour
{
    public GameObject skin1;
    public GameObject skin2;
    public GameObject skin3;
    public GameObject skin4;
    public GameObject skin5;
    public GameObject skin6;
    public GameObject skin7;

    public Material material1;
    public Material material2;
    public Material material3;
    public Material material4;
    public Material material5;
    public Material material6;
    public Material material7;
    public Material unknow;

    public Material materialsColor1;
    public Material materialsColor2;
    public Material materialsColor3;
    public Material materialsColor4;
    public Material materialsColor5;
    public Material materialsColor6;
    public Material materialsColor7;
    public Material unknowColor;

    public GameObject skin;
    public GameObject epaule;

    public GameObject pasEquipé;
    public GameObject equipé;
    public GameObject pasEncore;
    public GameObject pasAssez;
    public GameObject button;

    public Text fragVirus;

    public AudioSource sourceMusiques;
    public AudioSource sourceSons;
    public AudioClip backButtonSound;
    public AudioClip validate;

    private GameObject[] skins;
    private Material[] materials;
    private Material[] materialsColor;

    private int n;

    private bool hide = false;

    void Start()
    {
        sourceMusiques.volume = (float)PlayerPrefs.GetInt("VolumeMusiques") / 100;
        sourceSons.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
        //PlayerPrefs.SetInt("FragVirus", 800);
        fragVirus.text = PlayerPrefs.GetInt("FragVirus", 0).ToString();
        skins = new GameObject[7];
        skins[0] = skin1;
        skins[1] = skin2;
        skins[2] = skin3;
        skins[3] = skin4;
        skins[4] = skin5;
        skins[5] = skin6;
        skins[6] = skin7;
        materials = new Material[7];
        materials[0] = material1;
        materials[1] = material2;
        materials[2] = material3;
        materials[3] = material4;
        materials[4] = material5;
        materials[5] = material6;
        materials[6] = material7;
        materialsColor = new Material[7];
        materialsColor[0] = materialsColor1;
        materialsColor[1] = materialsColor2;
        materialsColor[2] = materialsColor3;
        materialsColor[3] = materialsColor4;
        materialsColor[4] = materialsColor5;
        materialsColor[5] = materialsColor6;
        materialsColor[6] = materialsColor7;
        /*
        PlayerPrefs.SetInt("SkinUsed", 1);
        PlayerPrefs.SetInt("Skin", 1);
        PlayerPrefs.SetInt("Skin1", 1);
        PlayerPrefs.SetInt("Skin2", 0);
        PlayerPrefs.SetInt("Skin3", 0);
        PlayerPrefs.SetInt("Skin4", 0);
        PlayerPrefs.SetInt("Skin5", 0);
        PlayerPrefs.SetInt("Skin6", 0);
        PlayerPrefs.SetInt("Skin7", 0);
        */
        PlayerPrefs.SetInt("Skin", PlayerPrefs.GetInt("SkinUsed", 1));
        if (PlayerPrefs.GetInt("Skin1", 1) == 1)
        {
            skin1.GetComponent<Button>().interactable = true;
            if (PlayerPrefs.GetInt("Skin", 1) == 1)
            {
                skin1.transform.Find("Border").gameObject.SetActive(true);
            }
            skin1.transform.Find("Name").gameObject.SetActive(true);
            skin1.transform.Find("?").gameObject.SetActive(false);
            skin1.transform.Find("Price").gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Skin2", 0) == 1)
        {
            skin2.GetComponent<Button>().interactable = true;
            if (PlayerPrefs.GetInt("Skin", 1) == 2)
            {
                skin2.transform.Find("Border").gameObject.SetActive(true);
            }
            skin2.transform.Find("Name").gameObject.SetActive(true);
            skin2.transform.Find("?").gameObject.SetActive(false);
            skin2.transform.Find("Price").gameObject.SetActive(false);
            skin2.transform.Find("Button").gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Skin3", 0) == 1)
        {
            skin3.GetComponent<Button>().interactable = true;
            if (PlayerPrefs.GetInt("Skin", 1) == 3)
            {
                skin3.transform.Find("Border").gameObject.SetActive(true);
            }
            skin3.transform.Find("Name").gameObject.SetActive(true);
            skin3.transform.Find("?").gameObject.SetActive(false);
            skin3.transform.Find("Price").gameObject.SetActive(false);
            skin3.transform.Find("Button").gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Skin4", 0) == 1)
        {
            skin4.GetComponent<Button>().interactable = true;
            if (PlayerPrefs.GetInt("Skin", 1) == 4)
            {
                skin4.transform.Find("Border").gameObject.SetActive(true);
            }
            skin4.transform.Find("Name").gameObject.SetActive(true);
            skin4.transform.Find("?").gameObject.SetActive(false);
            skin4.transform.Find("Price").gameObject.SetActive(false);
            skin4.transform.Find("Button").gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Skin5", 0) == 1)
        {
            skin5.GetComponent<Button>().interactable = true;
            if (PlayerPrefs.GetInt("Skin", 1) == 5)
            {
                skin5.transform.Find("Border").gameObject.SetActive(true);
            }
            skin5.transform.Find("Name").gameObject.SetActive(true);
            skin5.transform.Find("?").gameObject.SetActive(false);
            skin5.transform.Find("Price").gameObject.SetActive(false);
            skin5.transform.Find("Button").gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Skin6", 0) == 1)
        {
            skin6.GetComponent<Button>().interactable = true;
            if (PlayerPrefs.GetInt("Skin", 1) == 6)
            {
                skin6.transform.Find("Border").gameObject.SetActive(true);
            }
            skin6.transform.Find("Name").gameObject.SetActive(true);
            skin6.transform.Find("?").gameObject.SetActive(false);
            skin6.transform.Find("Price").gameObject.SetActive(false);
            skin6.transform.Find("Button").gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Skin7", 0) == 1)
        {
            skin7.GetComponent<Button>().interactable = true;
            if (PlayerPrefs.GetInt("Skin", 1) == 7)
            {
                skin7.transform.Find("Border").gameObject.SetActive(true);
            }
            skin7.transform.Find("Name").gameObject.SetActive(true);
            skin7.transform.Find("?").gameObject.SetActive(false);
            skin7.transform.Find("Price").gameObject.SetActive(false);
            skin7.transform.Find("Button").gameObject.SetActive(false);
        }
    }

    public void Skin(int n)
    {
        this.hide = false;
        this.n = n;
        PlayerPrefs.SetInt("Skin", n);
        for (int i = 0; i < skins.Length; i++)
        {
            if (i == n - 1)
            {
                skins[i].transform.Find("Border").gameObject.SetActive(true);
            }
            else
            {
                skins[i].transform.Find("Border").gameObject.SetActive(false);
            }
        }
        if (PlayerPrefs.GetInt("SkinUsed", 1) == n)
        {
            pasEquipé.SetActive(false);
            equipé.SetActive(true);
            pasEncore.SetActive(false);
            pasAssez.SetActive(false);
            button.transform.Find("Text").gameObject.GetComponent<Text>().text = "EQUIPE";
            button.GetComponent<Button>().interactable = false;
        }
        else
        {
            pasEquipé.SetActive(true);
            equipé.SetActive(false);
            pasEncore.SetActive(false);
            pasAssez.SetActive(false);
            button.transform.Find("Text").gameObject.GetComponent<Text>().text = "EQUIPER";
            button.GetComponent<Button>().interactable = true;
        }
    }

    public void Detail(int n)
    {
        if (PlayerPrefs.GetInt("Skin" + n, 0) == 0)
        {
            this.hide = true;
            this.n = n;
            PlayerPrefs.SetInt("Skin", n);
            for (int i = 0; i < skins.Length; i++)
            {
                if (i == n - 1)
                {
                    skins[i].transform.Find("Border").gameObject.SetActive(true);
                }
                else
                {
                    skins[i].transform.Find("Border").gameObject.SetActive(false);
                }
            }
            pasEquipé.SetActive(false);
            equipé.SetActive(false);
            pasEncore.SetActive(true);
            pasAssez.SetActive(false);
            button.transform.Find("Text").gameObject.GetComponent<Text>().text = "ACHETER";
            button.GetComponent<Button>().interactable = true;
        }
    }

    public void Button()
    {
        if (PlayerPrefs.GetInt("Skin" + this.n, 0) == 0)
        {
            if (PlayerPrefs.GetInt("FragVirus", 0) < 200)
            {
                if (pasAssez.activeInHierarchy)
                {
                    pasEquipé.SetActive(false);
                    equipé.SetActive(false);
                    pasEncore.SetActive(true);
                    pasAssez.SetActive(false);
                    button.transform.Find("Text").gameObject.GetComponent<Text>().text = "ACHETER";
                }
                else
                {
                    pasEquipé.SetActive(false);
                    equipé.SetActive(false);
                    pasEncore.SetActive(false);
                    pasAssez.SetActive(true);
                    button.transform.Find("Text").gameObject.GetComponent<Text>().text = "COMPRIS";
                }
            }
            else
            {
                sourceSons.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
                sourceSons.PlayOneShot(validate);
                PlayerPrefs.SetInt("FragVirus", PlayerPrefs.GetInt("FragVirus", 0) - 200);
                fragVirus.text = PlayerPrefs.GetInt("FragVirus", 0).ToString();
                hide = false;
                skins[this.n - 1].GetComponent<Button>().interactable = true;
                pasEquipé.SetActive(true);
                equipé.SetActive(false);
                pasEncore.SetActive(false);
                pasAssez.SetActive(false);
                PlayerPrefs.SetInt("Skin", this.n);
                PlayerPrefs.SetInt("Skin" + n, 1);
                skins[this.n - 1].transform.Find("Name").gameObject.SetActive(true);
                skins[this.n - 1].transform.Find("?").gameObject.SetActive(false);
                skins[this.n - 1].transform.Find("Price").gameObject.SetActive(false);
                skins[this.n - 1].transform.Find("Button").gameObject.SetActive(false);
                button.transform.Find("Text").gameObject.GetComponent<Text>().text = "EQUIPER";
            }
            /*
            for (int i = 0; i < skins.Length; i++)
            {
                if (i == n - 1)
                {
                    skins[i].transform.Find("Border").gameObject.SetActive(true);
                }
                else
                {
                    skins[i].transform.Find("Border").gameObject.SetActive(false);
                }
            }
            */
        }
        else
        {
            PlayerPrefs.SetInt("SkinUsed", this.n);
            pasEquipé.SetActive(false);
            equipé.SetActive(true);
            pasEncore.SetActive(false);
            pasAssez.SetActive(false);
            button.transform.Find("Text").gameObject.GetComponent<Text>().text = "EQUIPE";
            button.GetComponent<Button>().interactable = false;
        }
    }

    public void Retour()
    {
        StartCoroutine(SoundAndReturn());
    }

    IEnumerator SoundAndReturn()
    {
        float time = Time.timeScale;
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.075f);
        Time.timeScale = time;
        SceneManager.LoadScene("MenuPrincipal");
    }

    IEnumerator SoundThenReturn()
    {
        float time = Time.timeScale;
        Time.timeScale = 0.1f;
        sourceSons.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
        sourceSons.PlayOneShot(backButtonSound);
        yield return new WaitForSeconds(0.075f);
        Time.timeScale = time;
        SceneManager.LoadScene("MenuPrincipal");
    }

    private bool coucou = true;
    private bool salut = false;
    private bool ok = true;

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            StartCoroutine(SoundThenReturn());
        }
        if (hide)
        {
            skin.GetComponent<MeshRenderer>().material = unknow;
            epaule.transform.Find("Bras gauche").gameObject.GetComponent<MeshRenderer>().material = unknowColor;
            skin.transform.Find("Bras droit").gameObject.GetComponent<MeshRenderer>().material = unknowColor;
        }
        else
        {
            skin.GetComponent<MeshRenderer>().material = materials[PlayerPrefs.GetInt("Skin", 1) - 1];
            epaule.transform.Find("Bras gauche").gameObject.GetComponent<MeshRenderer>().material = materialsColor[PlayerPrefs.GetInt("Skin", 1) - 1];
            skin.transform.Find("Bras droit").gameObject.GetComponent<MeshRenderer>().material = materialsColor[PlayerPrefs.GetInt("Skin", 1) - 1];
        }
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
            epaule.transform.rotation = Quaternion.Slerp(epaule.transform.rotation, Quaternion.Euler(0, 0, 150 + Mathf.PingPong(Time.time * 50, 15)), Time.time * 50);
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
}
