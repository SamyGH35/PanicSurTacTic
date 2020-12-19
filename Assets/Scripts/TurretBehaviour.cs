using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    public GameObject maskPrefab;
    public Transform maskSpawn;

    public int nbOfMaskToShoot = 100;
    private int shoot = 0;

    public float maskSpeed = 50;
    public float lifeTime = 3;

    public float range = 15.0f;

    public float timeBetweenFire = 0.5f;
    private float timePassed = 0.0f;

    public string tagName = "Character";

    public Collider target=null;

    public float rotationSpeed = 3.0f;

    public AudioClip shotSound;
    public AudioSource source;

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        if (target==null)
        {
            // Detect colliders inside the range
            Collider[] insideColliders = Physics.OverlapSphere(transform.position, range);
            foreach (Collider col in insideColliders)
            {
                Contagion script = col.transform.gameObject.GetComponent<Contagion>();
                if (script && !script.IsMasked())
                {
                    target = col;
                    break;
                }
            }
        }

        if (target!=null)
        {
            Debug.Log(target.gameObject.name);
            // If the character has a mask or is too far, return
            Contagion script = target.transform.gameObject.GetComponent<Contagion>();
            if ((script && script.IsMasked()) || Vector3.Distance(target.transform.position, transform.position) > range)
            {
                target = null;
                return;
            }
            else
            {
                // Rotation
                /*
                Vector3 targetDirection = target.transform.position - transform.position;
                float singleStep = rotationSpeed * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
                transform.rotation = Quaternion.LookRotation(new Vector3(transform.rotation.x, newDirection.y, transform.rotation.z));
                */
                Vector3 targetDirection = target.transform.position - transform.position;
                targetDirection.y = 0;
                Quaternion rotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

                // Fire
                if (timePassed > timeBetweenFire)
                {
                    Fire();
                    timePassed = 0.0f;
                }
            }
        }
        if (shoot >= nbOfMaskToShoot)
        {
            StartCoroutine(DestroyAfterTime(gameObject, 0.5f));
        }
    }

    private void Fire()
    {
        shoot++;

        source.volume = (float)PlayerPrefs.GetInt("VolumeSons") / 200;
        source.PlayOneShot(shotSound);

        GameObject mask = Instantiate(maskPrefab);

        //Physics.IgnoreCollision(mask.GetComponent<Collider>(), GetComponent<Collider>());

        mask.transform.position = maskSpawn.position;
        Vector3 rotation = mask.transform.rotation.eulerAngles;
        mask.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
        mask.GetComponent<Rigidbody>().AddForce(maskSpawn.forward * maskSpeed, ForceMode.Impulse);

        StartCoroutine(DestroyAfterTime(mask, lifeTime));
    }

    private IEnumerator DestroyAfterTime(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
}
