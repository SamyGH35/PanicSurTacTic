using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Tutoriel : MonoBehaviour
{
    public GameObject Minimap;
    public GameObject Map;
    public GameObject Weapons;
    public GameObject TauxContamination;

    public GameObject borderGun;
    public Image backgroundGun;
    public Text munGun;
    public GameObject borderGrenade;
    public Image backgroundGrenade;
    public Text munGrenade;
    public GameObject borderTourelle;
    public Image backgroundTourelle;
    public Text munTourelle;

    public AudioClip backButtonSound;
    public AudioClip mapSound;
    public AudioClip changeWeapon;
    public AudioClip restock;
    public AudioSource sourceSons;
    public AudioSource sourceMusiques;

    public GameObject Pause;

    public Slider contamination;
    public Text taux;

    private bool use1 = true;
    private bool use2 = false;
    private bool use3 = false;
    private bool changes = false;

    private bool movement = false;
    private bool jump = false;
    private bool map = false;
    private bool weapons = false;
    private bool pause = false;

    public GameObject tutoriel;
    public Text tutoText;

    public float movementSpeed = 0.25f;
    public float jumpHeight = 50000;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public int minXRotation = -15;
    public int maxXRotation = 15;

    public GameObject minimapCamera;
    public GameObject mapCamera;

    private int count = 0;
    private int weaponCount = 0;

    void Start()
    {
        sourceMusiques.volume = (float)PlayerPrefs.GetInt("VolumeMusiques") / 400;
        rb = GetComponent<Rigidbody>();
        Cursor.visible = true;
        Time.timeScale = 0;
        tutoText.text = "Bonjour " + PlayerPrefs.GetString("Nom","") + ", bienvenue dans le tutoriel !" + System.Environment.NewLine + System.Environment.NewLine +
            "Ici, vous allez apprendre à vous servir des commandes pour commencer à jouer sur de bonnes bases.";
        Minimap.SetActive(false);
        Weapons.SetActive(false);
        TauxContamination.SetActive(false);
        PlayerPrefs.SetInt("MunGun", 10);
        PlayerPrefs.SetInt("MunGrenade", 1);
        PlayerPrefs.SetInt("MunTourelle", 1);
    }

    public void Compris()
    {
        if (count == 0)
        {
            tutoText.text = "Pour commencer, il faut se déplacer !" + System.Environment.NewLine + System.Environment.NewLine +
            "Pour ce faire, utilisez les touches ZQSD (ou WASD en fonction de votre clavier) ou les flèches directionnelles." + System.Environment.NewLine +
            "Pour tourner ou incliner votre personnage, utilisez la souris.";
        }
        else if (count == 1)
        {
            movement = true;
            Cursor.visible = false;
            tutoriel.SetActive(false);
            Time.timeScale = 1;
        }
        else if (count == 2)
        {
            tutoText.text = "Pour passer certains obstacles, vous allez devoir sauter." + System.Environment.NewLine + System.Environment.NewLine +
            "Pour sauter, utilisez au choix la touche ESPACE" + System.Environment.NewLine +
            "ou l'une des touches MAJ." + System.Environment.NewLine +
            "A vous de jouer !";
        }
        else if (count == 3)
        {
            jump = true;
            Cursor.visible = false;
            tutoriel.SetActive(false);
            Time.timeScale = 1;
        }
        else if (count == 4)
        {
            tutoText.text = "Au cours de vos parties vous allez croiser beaucoup de TacTics." + System.Environment.NewLine + System.Environment.NewLine +
            "Initialement les TacTics sont bleus, comme celui-là.";
        }
        else if (count == 5)
        {
            Cursor.visible = false;
            tutoriel.SetActive(false);
            Time.timeScale = 1;
        }
        else if (count == 6)
        {
            tutoText.text = "Un nouveau virus est apparu sur la planète TacTic, et il a la particularité de changer la couleur des TacTics infectés." + System.Environment.NewLine + System.Environment.NewLine +
            "Vu sa couleur verte, ce TacTic est malade !" + System.Environment.NewLine +
            "N'ayez pas peur, vous êtes immunisé," + System.Environment.NewLine +
            "c'est d'ailleurs pour cela que l'Etat vous a recruté !";
        }
        else if (count == 7)
        {
            Minimap.SetActive(true);
            Cursor.visible = false;
            tutoriel.SetActive(false);
            Time.timeScale = 1;
        }
        else if (count == 8)
        {
            tutoText.text = "Vous avez dû remarquer la Minimap." + System.Environment.NewLine + System.Environment.NewLine +
            "Elle vous permet de voir les TacTics à proximité." + System.Environment.NewLine +
            "Vous pouvez maintenir les touches TAB ou M" + System.Environment.NewLine +
            "pour l'afficher en grand." + System.Environment.NewLine +
            "C'est le moment d'essayer !";
        }
        else if (count == 9)
        {
            map = true;
            Cursor.visible = false;
            tutoriel.SetActive(false);
            Time.timeScale = 1;
        }
        else if (count == 10)
        {
            tutoText.text = "Attention, un TacTic contaminé s'approche des TacTics sains !" + System.Environment.NewLine + System.Environment.NewLine +
            "Vous devez empêcher ça, c'est votre mission !" + System.Environment.NewLine +
            "Appuyez sur le clic gauche pour lancer des masques avec votre arme.";
        }
        else if (count == 11)
        {
            weapons = true;
            Weapons.SetActive(true);
            Cursor.visible = false;
            tutoriel.SetActive(false);
            Time.timeScale = 1;
        }
        else if (count == 12 && weaponCount > 0)
        {
            tutoText.text = "Bravo, vous avez protégé les TicTacs sains !" + System.Environment.NewLine + System.Environment.NewLine +
            "Maintenant, essayer la grenade à gel hydroalcoolique." + System.Environment.NewLine +
            "Appuyez sur le clic gauche pour la lancer.";
        }
        else if (count == 13 && weaponCount > 0)
        {
            use1 = false;
            use2 = true;
            Cursor.visible = false;
            tutoriel.SetActive(false);
            Time.timeScale = 1;
        }
        else if (count == 14 && weaponCount > 1)
        {
            tutoText.text = "Beau lancé !" + System.Environment.NewLine + System.Environment.NewLine +
            "Il vous reste encore une arme à découvrir." + System.Environment.NewLine +
            "Appuyez sur le clic gauche pour poser une tourelle automatique.";
        }
        else if (count == 15 && weaponCount > 1)
        {
            use2 = false;
            use3 = true;
            Cursor.visible = false;
            tutoriel.SetActive(false);
            Time.timeScale = 1;
        }
        else if (count == 16 && weaponCount > 2)
        {
            tutoText.text = "Plutôt pratique, non ?" + System.Environment.NewLine + System.Environment.NewLine +
            "Pour changer d'arme en cours de partie, vous pouvez scroller avec la souris ou appuyer sur les touches 1, 2 et 3" + System.Environment.NewLine +
            "pour utiliser les armes respectives." + System.Environment.NewLine +
            "Si vous êtes à proximité d'un stock de munitions, appuyez sur la touche R ou sur le bouton droit de la souris pour recharger.";
        }
        else if (count == 17 && weaponCount > 2)
        {
            use3 = false;
            use1 = true;
            Cursor.visible = false;
            tutoriel.SetActive(false);
            Time.timeScale = 1;
        }
        else if (count == 18 && weaponCount > 2)
        {
            tutoText.text = "Votre but est de contenir l'épidémie !" + System.Environment.NewLine + System.Environment.NewLine +
            "Pour visualiser la taux de contamination de la ville," + System.Environment.NewLine +
            "vous avez accès à une barre de contamination qui vous indique le pourcentage de la population qui est infectée." + System.Environment.NewLine +
            "Essayez de rester sous le seuil critique le plus longtemps possible pour permettre aux scientifiques de trouver le vaccin !";
        }
        else if (count == 19 && weaponCount > 2)
        {
            use3 = false;
            use1 = true;
            TauxContamination.SetActive(true);
            Cursor.visible = false;
            tutoriel.SetActive(false);
            Time.timeScale = 1;
        }
        else if (count == 20 && weaponCount > 2)
        {
            tutoText.text = "Voilà, vous semblez prêts pour être déployé de villes en villes pour sauver la population TacTic !" + System.Environment.NewLine + System.Environment.NewLine +
            "Une dernière chose ! A tout moment vous pouvez appuyer sur la touche ECHAP pour mettre le jeu en pause," + System.Environment.NewLine +
            "accéder aux options ou retourner au menu principal.";
        }
        else if (count == 21 && weaponCount > 2)
        {
            pause = true;
            Cursor.visible = false;
            tutoriel.SetActive(false);
            Time.timeScale = 1;
        }
        else if (count == 22 && weaponCount > 2)
        {
            tutoText.text = "Le tutoriel est fini !" + System.Environment.NewLine + System.Environment.NewLine +
            "Passez la porte pour continuer.";
        }
        else if (count == 23 && weaponCount > 2)
        {
            Cursor.visible = false;
            tutoriel.SetActive(false);
            Time.timeScale = 1;
        }
        count++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Stock(Clone)")
        {
            if (count == 24)
            {
                PlayerPrefs.SetInt("TutoFini", 1);
                SceneManager.LoadScene(PlayerPrefs.GetString("NextScene", "MenuPrincipal"));
                Cursor.visible = true;
            }
            else
            {
                other.enabled = false;
                Cursor.visible = true;
                tutoriel.SetActive(true);
                Time.timeScale = 0;
                Compris();
            }
        }
    }

    void FixedUpdate()
    {
        if (movement)
        {
            Vector3 movement = new Vector3(movementX, 0.0f, movementY);
            movement = transform.TransformDirection(movement);
            //transform.position += movement * movementSpeed * Time.timeScale;
            //rb.MovePosition(transform.position + movement * movementSpeed * Time.timeScale);
            //rb.position += movement * movementSpeed * Time.timeScale;
            Vector3 targetPosition = transform.position + movement * movementSpeed * Time.timeScale;
            RaycastHit raycastHit;
            Physics.Raycast(transform.position, movement, out raycastHit, movementSpeed * Time.timeScale);
            if (raycastHit.collider == null)
            {
                rb.MovePosition(targetPosition);
            }
            else if (raycastHit.collider.isTrigger)
            {
                rb.MovePosition(targetPosition);
            }
            /*
            Quaternion rotation = Quaternion.Euler(new Vector3(PlayerPrefs.GetInt("Inversion", -1) * Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0.0f) * rotationSpeed * Time.timeScale);
            rotation *= rb.rotation;
            rb.MoveRotation(rotation);
            */
            transform.Rotate(new Vector3(PlayerPrefs.GetInt("Inversion", -1) * Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * PlayerPrefs.GetInt("Sensibilite", 10) * Time.timeScale, Space.Self);
        }
        if (jump)
        {
            if (transform.position.y < 1 && (Input.GetKeyDown("right shift") || Input.GetKeyDown("left shift") || Input.GetKeyDown("space")))
            {
                rb.AddForce(Vector3.up * jumpHeight * Time.timeScale);
            }
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    public void Recommencer()
    {
        StartCoroutine(WaitForReturn());
    }

    IEnumerator WaitForReturn()
    {
        float time = Time.timeScale;
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.075f);
        SceneManager.LoadScene("Tutoriel");
        Time.timeScale = time;
    }

    public void Contamination()
    {
        taux.text = contamination.value.ToString() + "%";
    }

    public void Retour()
    {
        Pause.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    void Update()
    {
        if (movement)
        {
            mapCamera.transform.position = new Vector3(transform.position.x, 10, transform.position.z);
            minimapCamera.transform.position = new Vector3(transform.position.x, 10, transform.position.z);
            Quaternion rotation = Quaternion.identity;
            rotation.eulerAngles = new Vector3(90, transform.rotation.eulerAngles.y, 0);
            mapCamera.transform.rotation = rotation;
            minimapCamera.transform.rotation = rotation;

            Vector3 eulerRotation = transform.rotation.eulerAngles;
            if (eulerRotation.x > 180)
            {
                if (eulerRotation.x < 360 + minXRotation)
                {
                    transform.Rotate(new Vector3(-eulerRotation.x + minXRotation, 0, -eulerRotation.z));
                }
                else
                {
                    transform.Rotate(new Vector3(0, 0, -eulerRotation.z));
                }
            }
            else
            {
                if (eulerRotation.x > maxXRotation)
                {
                    transform.Rotate(new Vector3(-eulerRotation.x + maxXRotation, 0, -eulerRotation.z));
                }
                else
                {
                    transform.Rotate(new Vector3(0, 0, -eulerRotation.z));
                }
            }
        }
        if (weapons)
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
                backgroundGrenade.color = new Color(1, 1, 1, 0.5f);
                borderTourelle.SetActive(false);
                backgroundTourelle.color = new Color(1, 1, 1, 0.5f);
                borderGun.SetActive(true);
                backgroundGun.color = new Color(1, 1, 1, 1);
                sourceSons.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
                sourceSons.PlayOneShot(changeWeapon);
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
                sourceSons.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
                sourceSons.PlayOneShot(changeWeapon);
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
                sourceSons.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
                sourceSons.PlayOneShot(changeWeapon);
                changes = false;
            }
        }
        if (map)
        {
            if ((Input.GetKeyDown("m") || Input.GetKeyDown("tab")) && Time.timeScale != 0)
            {
                sourceSons.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
                sourceSons.PlayOneShot(mapSound);
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
        }
        if (pause)
        {
            if (Input.GetKeyDown("escape") && !Pause.activeInHierarchy)
            {
                sourceSons.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
                sourceSons.PlayOneShot(mapSound);
                Time.timeScale = 0;
                Pause.SetActive(true);
                Cursor.visible = true;
            }
            else if (Input.GetKeyDown("escape") && !SceneManager.GetSceneByName("Options").isLoaded)
            {
                sourceSons.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
                sourceSons.PlayOneShot(backButtonSound);
                Pause.SetActive(false);
                Time.timeScale = 1;
                Cursor.visible = false;
            }
        }
        if (Input.GetMouseButtonDown(0) && (count == 12 || count == 14 || count == 16))
        {
            weaponCount++;
        }
        sourceMusiques.volume = (float)PlayerPrefs.GetInt("VolumeMusiques") / 400;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "Stock(Clone)" && (Input.GetKeyDown("r") || Input.GetMouseButtonDown(1)))
        {
            PlayerPrefs.SetInt("MunGun", 100);
            PlayerPrefs.SetInt("MunGrenade", 25);
            PlayerPrefs.SetInt("MunTourelle", 5);
            sourceSons.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
            sourceSons.PlayOneShot(restock);
        }
    }
}
