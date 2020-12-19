using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public Text nombreDeFragmentDeVirus;
    public GameObject help;

    public GameObject fragVirus;
    public GameObject tutoriel;
    public GameObject aide;
    public GameObject quitter;
    public GameObject solo;
    public GameObject coop;
    public GameObject skins;
    public GameObject options;

    public Text helpText;

    public AudioClip backButtonSound;
    public AudioSource sourceSons;
    public AudioSource sourceMusiques;

    public GameObject player;
    public GameObject blue;
    public GameObject green;
    public GameObject blueMasked;
    public GameObject greenMasked;

    void Start()
    {
        sourceMusiques.volume = (float)PlayerPrefs.GetInt("VolumeMusiques") / 100;
        nombreDeFragmentDeVirus.text = PlayerPrefs.GetInt("FragVirus", 0).ToString();
    }

    public void Tuto()
    {
        PlayerPrefs.SetString("NextScene", "MenuPrincipal");
        SceneManager.LoadScene("Tutoriel");
    }

    public void Solo()
    {
        if (PlayerPrefs.GetInt("TutoFini", 0) == 1)
        {
            SceneManager.LoadScene("SoloTest");
        }
        else
        {
            PlayerPrefs.SetString("NextScene", "SoloTest");
            SceneManager.LoadScene("Tutoriel");
        }
    }

    public void Help()
    {
        help.SetActive(true);

        fragVirus.SetActive(true);
        tutoriel.SetActive(true);
        aide.SetActive(false);
        quitter.SetActive(true);
        solo.SetActive(true);
        coop.SetActive(true);
        skins.SetActive(true);
        options.SetActive(true);

        helpText.text = "Ceci est le bouton sur lequel vous venez de cliquer : le bouton aide." + System.Environment.NewLine +
            "Cliquez dessus si vous avez un doute sur l'utilité des boutons du menu principal." + System.Environment.NewLine +
            "Pour quitter l'aide, vous pouvez appuyer sur le bouton \"FERMER L'AIDE\" ou sur la touche \"ECHAP\" à tout moment.";
    }

    public void Next()
    {
        if (!aide.activeInHierarchy)
        {
            aide.SetActive(true);
            tutoriel.SetActive(false);
            helpText.text = "Cliquez sur ce bouton pour lancer le tutoriel." + System.Environment.NewLine +
                "Il vous apprendra tout ce qu'il faut savoir pour bien commencer" + System.Environment.NewLine +
                "à jouer sur de bonnes bases !";
        }
        else if (!tutoriel.activeInHierarchy)
        {
            tutoriel.SetActive(true);
            fragVirus.SetActive(false);
            helpText.text = "Ce nombre représente les fragments de virus en votre possession." + System.Environment.NewLine +
                "Les fragments de virus servent uniquement à acheter des skins," + System.Environment.NewLine +
                "ils s'obtiennent à la fin de chaque ville en fonction de vos performances.";
        }
        else if (!fragVirus.activeInHierarchy)
        {
            fragVirus.SetActive(true);
            solo.SetActive(false);
            helpText.text = "Cliquez sur ce bouton pour lancer une partie en solo." + System.Environment.NewLine +
                "La population TacTic compte sur vous pour les protéger !";
        }
        else if (!solo.activeInHierarchy)
        {
            solo.SetActive(true);
            coop.SetActive(false);
            helpText.text = "Cliquez sur ce bouton pour lancer une partie en multijoueur." + System.Environment.NewLine +
                "Vous et vos amis allez devoir coopérer pour sauver la planète TacTic !";
        }
        else if (!coop.activeInHierarchy)
        {
            coop.SetActive(true);
            skins.SetActive(false);
            helpText.text = "Ce bouton vous permet d'accéder à la boutique des skins." + System.Environment.NewLine +
                "Servez vous des fragments de virus que vous avez récoltés pour faire vos achats !";
        }
        else if (!skins.activeInHierarchy)
        {
            skins.SetActive(true);
            options.SetActive(false);
            helpText.text = "Pour accéder aux options du jeu, cliquez sur ce bouton." + System.Environment.NewLine +
                "Les options sont aussi accessibles en jeu depuis le menu pause.";
        }
        else if (!options.activeInHierarchy)
        {
            options.SetActive(true);
            quitter.SetActive(false);
            helpText.text = "Si vous souhaitez quitter le jeu," + System.Environment.NewLine +
                "appuyez sur ce bouton.";
        }
        else if (!quitter.activeInHierarchy)
        {
            quitter.SetActive(true);
            helpText.text = "L'aide est terminée !" + System.Environment.NewLine +
                "N'hésitez pas à revenir pour vous rafraîchir la mémoire.";
        }
        else
        {
            FinHelp();
        }
    }

    public void FinHelp()
    {
        help.SetActive(false);
    }

    public void Quitter()
    {
        Application.Quit();
    }

    private bool go = true;
    private int masked = 2;
    private bool jump = true;
    private bool stop = false;

    void Update()
    {
        if (Input.GetKeyDown("escape") && help.activeInHierarchy)
        {
            sourceSons.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
            sourceSons.PlayOneShot(backButtonSound);
            FinHelp();
        }
        sourceMusiques.volume = (float)PlayerPrefs.GetInt("VolumeMusiques") / 100;

        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0, 180, 5 - Mathf.PingPong(Time.time * 10, 10)), Time.time * 10); //Quaternion.Euler(0, 180, Mathf.PingPong(Time.time * 10, 10) - 5);

        if (jump)
        {
            //player.transform.position = new Vector3(player.transform.position.x, -2.5f + Mathf.PingPong(Time.time * 2, 1), player.transform.position.z);
            player.transform.position = Vector3.Lerp(player.transform.position, new Vector3(5, -2.5f + Mathf.PingPong(Time.time * 2, 1), 0), Time.time * 2);
            if (!stop)
            {
                StartCoroutine(StopJump());
            }
        }
        else
        {
            if (!stop)
            {
                StartCoroutine(Jump());
            }
        }

        if (go)
        {
            StartCoroutine(Run());
        }
        else
        {
            if (masked == 1)
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
            else if (masked == 0)
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

    IEnumerator Jump()
    {
        stop = true;
        yield return new WaitForSeconds(Random.Range(0, 10));
        jump = true;
        stop = false;
    }

    IEnumerator StopJump()
    {
        stop = true;
        yield return new WaitForSeconds(0.9f);
        jump = false;
        stop = false;
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
