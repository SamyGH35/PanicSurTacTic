using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 0.25f;
    public float jumpHeight = 5000;
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

    public GameObject plafond;

    public AudioClip restock;
    public AudioSource sourceSons;
    public AudioSource sourceMusiques;

    private bool stockPositionnement = true;

    public float jumpTime = 0.25f;
    private float jumpTimeCounter;
    private bool isJumping = false;

    void Start()
    {
        sourceMusiques.volume = (float)PlayerPrefs.GetInt("VolumeMusiques") / 400;
        rb = GetComponent<Rigidbody>();
        Time.timeScale = 0;
        PlayerPrefs.SetInt("MunGun", 100);
        PlayerPrefs.SetInt("MunGrenade", 25);
        PlayerPrefs.SetInt("MunTourelle", 5);
        plafond.SetActive(false);
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

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && stockPositionnement)
        {
            Ray ray = cityCamera.ScreenPointToRay(Input.mousePosition);
            ray.origin -= new Vector3(5, 0, 5);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.collider.gameObject.name + " at " + hit.point);
                if (hit.point.y < 0.5f && hit.point.x > -192 && hit.point.x < 187 && hit.point.z > -275 && hit.point.z < 257)
                {
                    if (hit.collider.gameObject.name.Length >= 4 && (hit.collider.gameObject.name.Substring(0, 4) == "Road" || hit.collider.gameObject.name.Substring(0, 4) == "Terr" || hit.collider.gameObject.name.Substring(0, 4) == "Gras"))
                    {
                        Instantiate(stock, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                        Cursor.visible = false;
                        positionnement.SetActive(false);
                        plafond.SetActive(true);
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0) && stockPositionnement)
        {
            stockPositionnement = false;
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