using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 0.25f;
    public float jumpHeight = 50000;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public int minXRotation = -15;
    public int maxXRotation = 15;

    public GameObject minimapCamera;
    public GameObject mapCamera;

    public Camera cityCamera;
    public GameObject positionnement;
    public GameObject stock;

    public AudioClip restock;
    public AudioSource sourceSons;
    public AudioSource sourceMusiques;

    private bool stockPositionnement = true;

    void Start()
    {
        sourceMusiques.volume = (float)PlayerPrefs.GetInt("VolumeMusiques") / 400;
        rb = GetComponent<Rigidbody>();
        Time.timeScale = 0;
        PlayerPrefs.SetInt("MunGun", 0);
        PlayerPrefs.SetInt("MunGrenade", 0);
        PlayerPrefs.SetInt("MunTourelle", 0);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        movement = transform.TransformDirection(movement);
        //rb.MovePosition(transform.position + movement * movementSpeed * Time.timeScale);
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
        Quaternion rotation = Quaternion.Euler(new Vector3(PlayerPrefs.GetInt("Inversion", -1) * Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0.0f) * PlayerPrefs.GetInt("Sensibilite", 10) * Time.timeScale);
        rotation *= rb.rotation;
        rb.MoveRotation(rotation);
        */
        transform.Rotate(new Vector3(PlayerPrefs.GetInt("Inversion", -1) * Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * PlayerPrefs.GetInt("Sensibilite", 10) * Time.timeScale, Space.Self);

        if ((Input.GetKeyDown("right shift") || Input.GetKeyDown("left shift") || Input.GetKeyDown("space")) && transform.position.y < 0.5f)
        {
            rb.AddForce(Vector3.up * jumpHeight * Time.timeScale);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && stockPositionnement)
        {
            Ray ray = cityCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Instantiate(stock, hit.point, Quaternion.identity);
            }
            stockPositionnement = false;
            Cursor.visible = false;
            positionnement.SetActive(false);
            Time.timeScale = 1;
        }

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