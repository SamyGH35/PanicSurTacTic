using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviourTest : MonoBehaviour
{
    public GameObject maskPrefab;
    public Transform maskSpawn;

    public float maskSpeed = 30;
    public float lifeTime = 3;

    public float timeBetweenFire = 0.5f;
    private float timePassed = 0.0f;

    public int maxLoad = 10;
    public int load = 10;
    public float reloadTime = 2.0f;
    private float reloadTimePassed = 0.0f;
    private bool reloading = false;

    public AudioClip shotSound;
    public AudioClip reloadSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        if (reloading)
        {
            reloadTimePassed += Time.deltaTime;
            if (reloadTimePassed > reloadTime)
            {
                reloading = false;
                load = maxLoad;
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && timePassed > timeBetweenFire && load > 0 && Time.timeScale!=0)
            {
                Fire();
                timePassed = 0.0f;
            }

            if (Input.GetKeyDown(KeyCode.R) && load < maxLoad)
            {
                reloading = true;
                reloadTimePassed = 0;
                audioSource.PlayOneShot(reloadSound);
            }
        }
    }

    private void Fire()
    {
        load--;
        audioSource.PlayOneShot(shotSound);

        GameObject mask = Instantiate(maskPrefab);
        Physics.IgnoreCollision(mask.GetComponent<Collider>(), maskSpawn.parent.GetComponent<Collider>());

        mask.transform.position = maskSpawn.position;
        Vector3 rotation = mask.transform.rotation.eulerAngles;
        mask.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
        mask.GetComponent<Rigidbody>().AddForce(maskSpawn.forward * maskSpeed, ForceMode.Impulse);

        StartCoroutine(DestroyMaskAfterTime(mask, lifeTime));
    }

    private IEnumerator DestroyMaskAfterTime(GameObject mask, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(mask);
    }
}
