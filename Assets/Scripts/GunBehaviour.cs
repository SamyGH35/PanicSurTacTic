using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{
    public GameObject maskPrefab;
    public Transform maskSpawn;

    public GameObject player;

    public float maskSpeed = 30;
    public float lifeTime = 3;
    public float timeBetweenFire = 0.5f;

    public AudioClip shotSound;
    public AudioSource source;

    private bool firing;

    void Start()
    {
        firing = false;
    }

    public bool isFiring()
    {
        return firing;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0 && PlayerPrefs.GetInt("MunGun", 0) > 0 && !firing)
        {
            StartCoroutine(Firing());
        }
    }

    IEnumerator Firing()
    {
        firing = true;
        Fire();
        yield return new WaitForSeconds(timeBetweenFire);
        firing = false;
    }

    private void Fire()
    {
        source.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 100;
        source.PlayOneShot(shotSound);
        PlayerPrefs.SetInt("MunGun", PlayerPrefs.GetInt("MunGun", 0) - 1);
        PlayerPrefs.SetInt("Masques tirés", PlayerPrefs.GetInt("Masques tirés", 0) + 1);

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
