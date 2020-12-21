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

    public GameObject Gun;
    public GameObject Grenade;
    public GameObject Tourelle;

    public GameObject stock;

    public GameObject borderGun;
    public Image backgroundGun;
    public Text munGun;
    public GameObject borderGrenade;
    public Image backgroundGrenade;
    public Text munGrenade;
    public GameObject borderTourelle;
    public Image backgroundTourelle;
    public Text munTourelle;

    public GameObject pnj1;
    public GameObject pnj2;
    public GameObject pnj3;

    public GameObject pnj4;
    public GameObject pnj5;
    public GameObject pnj6;

    public GameObject pnj7;
    public GameObject pnj8;
    public GameObject pnj9;

    public GameObject mur5;
    public GameObject mur6;
    public GameObject mur7;

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
    private bool scroll123 = false;

    private bool movement = false;
    private bool jump = false;
    private bool map = false;
    private bool weapons = false;
    private bool pause = false;

    public GameObject tutoriel;
    public Text tutoText;

    public float movementSpeed = 0.25f;
    public float jumpHeight = 5000;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public int minXRotation = -15;
    public int maxXRotation = 15;

    public GameObject minimapCamera;
    public GameObject mapCamera;

    private int count = 0;

    public float jumpTime = 0.25f;
    private float jumpTimeCounter;
    private bool isJumping = false;

    private bool hit = false;

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
        PlayerPrefs.SetInt("MunGrenade", 2);
        PlayerPrefs.SetInt("MunTourelle", 1);
        Gun.SetActive(false);
        Grenade.SetActive(false);
        Tourelle.SetActive(false);
        stock.SetActive(false);
        mur6.SetActive(false);
        mur7.SetActive(false);
        pnj4.SetActive(false);
        pnj5.SetActive(false);
        pnj6.SetActive(false);
        pnj7.SetActive(false);
        pnj8.SetActive(false);
        pnj9.SetActive(false);
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
            "ou l'une des touches MAJ. Maintenez pour sauter plus haut." + System.Environment.NewLine +
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
            Gun.SetActive(true);
            Cursor.visible = false;
            tutoriel.SetActive(false);
            Time.timeScale = 1;
        }
        else if (count == 12)
        {
            tutoText.text = "Bravo, vous avez protégé les TicTacs sains !" + System.Environment.NewLine + System.Environment.NewLine +
            "Maintenant, essayer la grenade à gel hydroalcoolique." + System.Environment.NewLine +
            "Plus vous maintenez le clic gauche, plus vous la lancerez loin.";
            mur6.SetActive(true);
            pnj4.SetActive(true);
            pnj5.SetActive(true);
            pnj6.SetActive(true);
            mur5.SetActive(false);
            Cursor.visible = true;
            tutoriel.SetActive(true);
            Time.timeScale = 0;
        }
        else if (count == 13)
        {
            Gun.SetActive(false);
            Grenade.SetActive(true);
            use1 = false;
            use2 = true;
            changes = true;
            Cursor.visible = false;
            tutoriel.SetActive(false);
            Time.timeScale = 1;
        }
        else if (count == 14)
        {
            tutoText.text = "Beau lancé !" + System.Environment.NewLine + System.Environment.NewLine +
            "Il vous reste encore une arme à découvrir." + System.Environment.NewLine +
            "Appuyez sur le clic gauche pour poser une tourelle automatique.";
            mur7.SetActive(true);
            pnj7.SetActive(true);
            pnj8.SetActive(true);
            pnj9.SetActive(true);
            mur6.SetActive(false);
            Cursor.visible = true;
            tutoriel.SetActive(true);
            Time.timeScale = 0;
        }
        else if (count == 15)
        {
            Grenade.SetActive(false);
            Tourelle.SetActive(true);
            use2 = false;
            use3 = true;
            changes = true;
            Cursor.visible = false;
            tutoriel.SetActive(false);
            Time.timeScale = 1;
        }
        else if (count == 16)
        {
            tutoText.text = "Plutôt pratique, non ?" + System.Environment.NewLine + System.Environment.NewLine +
            "Pour changer d'arme en cours de partie, vous pouvez scroller avec la souris ou appuyer sur les touches 1, 2 et 3" + System.Environment.NewLine +
            "pour utiliser les armes respectives." + System.Environment.NewLine +
            "Si vous êtes à proximité d'un stock de munitions, appuyez sur la touche R ou sur le bouton droit de la souris pour recharger.";
            stock.SetActive(true);
            mur7.SetActive(false);
            Cursor.visible = true;
            tutoriel.SetActive(true);
            Time.timeScale = 0;
        }
        else if (count == 17)
        {
            scroll123 = true;
            use3 = false;
            use1 = true;
            changes = true;
            Cursor.visible = false;
            tutoriel.SetActive(false);
            Time.timeScale = 1;
        }
        else if (count == 18)
        {
            tutoText.text = "Votre but est de contenir l'épidémie !" + System.Environment.NewLine + System.Environment.NewLine +
            "Pour visualiser le taux de contamination de la ville," + System.Environment.NewLine +
            "vous avez accès à une barre de contamination qui vous indique le pourcentage de la population qui est infectée." + System.Environment.NewLine +
            "Essayez de rester sous le seuil critique le plus longtemps possible pour permettre aux scientifiques de trouver le vaccin !";
        }
        else if (count == 19)
        {
            use3 = false;
            use1 = true;
            TauxContamination.SetActive(true);
            Cursor.visible = false;
            tutoriel.SetActive(false);
            Time.timeScale = 1;
        }
        else if (count == 20)
        {
            tutoText.text = "Voilà, vous semblez prêts pour être déployé de villes en villes pour sauver la population TacTic !" + System.Environment.NewLine + System.Environment.NewLine +
            "Une dernière chose ! A tout moment vous pouvez appuyer sur la touche ECHAP pour mettre le jeu en pause," + System.Environment.NewLine +
            "accéder aux options ou retourner au menu principal.";
        }
        else if (count == 21)
        {
            pause = true;
            Cursor.visible = false;
            tutoriel.SetActive(false);
            Time.timeScale = 1;
        }
        else if (count == 22)
        {
            tutoText.text = "Le tutoriel est fini !" + System.Environment.NewLine + System.Environment.NewLine +
            "Passez la porte pour continuer.";
        }
        else if (count == 23)
        {
            Cursor.visible = false;
            tutoriel.SetActive(false);
            Time.timeScale = 1;
        }
        count++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Stock(Clone)" && other.name.Substring(0,3) != "PNJ")
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
            if (transform.position.y > 0.5f)
            {
                movement = new Vector3(movement.x, 0, movement.z);
            }
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
            transform.Rotate(new Vector3(PlayerPrefs.GetInt("Inversion", -1) * Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * PlayerPrefs.GetInt("Sensibilite", 10) * Time.timeScale, Space.Self);
        }
        if (jump)
        {
            if ((Input.GetKeyDown("right shift") || Input.GetKeyDown("left shift") || Input.GetKeyDown("space")) && transform.position.y < 0.25f)
            {
                rb.AddForce(Vector3.up * jumpHeight * Time.timeScale);
                isJumping = true;
                jumpTimeCounter = jumpTime;
            }
            if ((Input.GetKey("right shift") || Input.GetKey("left shift") || Input.GetKey("space")) && isJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.AddForce(Vector3.up * jumpHeight * Time.timeScale);
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }
            if ((Input.GetKeyUp("right shift") || Input.GetKeyUp("left shift") || Input.GetKeyUp("space")))
            {
                isJumping = false;
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
            if (scroll123)
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
            }
            if (use1 && changes)
            {
                use1 = true;
                Gun.SetActive(true);
                Grenade.SetActive(false);
                Tourelle.SetActive(false);
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
                Gun.SetActive(false);
                Grenade.SetActive(true);
                Tourelle.SetActive(false);
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
                Gun.SetActive(false);
                Grenade.SetActive(false);
                Tourelle.SetActive(true);
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
            munGun.text = PlayerPrefs.GetInt("MunGun", 0).ToString();
            munGrenade.text = PlayerPrefs.GetInt("MunGrenade", 0).ToString();
            munTourelle.text = PlayerPrefs.GetInt("MunTourelle", 0).ToString();
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
        if (count == 12 && PlayerPrefs.GetInt("MunGun", 0) == 0)
        {
            PlayerPrefs.SetInt("MunGun", 10);
        }
        if (count == 12 && pnj1.GetComponent<Contagion>().IsMasked() && pnj2.GetComponent<Contagion>().IsMasked() && pnj3.GetComponent<Contagion>().IsMasked() && !Gun.GetComponent<GunBehaviour>().isFiring())
        {
            Compris();
        }
        if (count == 14 && PlayerPrefs.GetInt("MunGrenade", 0) == 0)
        {
            PlayerPrefs.SetInt("MunGrenade", 2);
        }
        if (count == 14 && pnj4.GetComponent<Contagion>().getStade() < 1 && pnj5.GetComponent<Contagion>().getStade() < 1 && pnj6.GetComponent<Contagion>().getStade() < 1)
        {
            if (!hit)
            {
                hit = true;
                StartCoroutine(Capisce());
            }
        }
        if (count == 16 && PlayerPrefs.GetInt("MunTourelle", 0) == 0)
        {
            PlayerPrefs.SetInt("MunTourelle", 1);
        }
        if (count == 16 && pnj7.GetComponent<Contagion>().IsMasked() && pnj8.GetComponent<Contagion>().IsMasked() && pnj9.GetComponent<Contagion>().IsMasked())
        {
            Compris();
        }
        sourceMusiques.volume = (float)PlayerPrefs.GetInt("VolumeMusiques") / 400;
    }

    IEnumerator Capisce()
    {
        yield return new WaitForSeconds(0.5f);
        Compris();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "Stock(Clone)" && (Input.GetKeyDown("r") || Input.GetMouseButtonDown(1)))
        {
            PlayerPrefs.SetInt("MunGun", 100);
            PlayerPrefs.SetInt("MunGrenade", 10);
            PlayerPrefs.SetInt("MunTourelle", 5);
            sourceSons.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
            sourceSons.PlayOneShot(restock);
            other.gameObject.GetComponent<ParticleSystem>().Play();
        }
    }
}