using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    public GameObject maskPrefab;
    public Transform maskSpawn;

    public float maskSpeed = 50;
    public float lifeTime = 3;

    public float range = 15.0f;

    public float timeBetweenFire = 0.5f;
    private float timePassed = 0.0f;

    public string tagName = "Character";

    public Collider target;

    public float rotationSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;

        if (!target)
        {
            // Detect colliders inside the range
            Collider[] insideColliders = Physics.OverlapSphere(transform.position, range);
            foreach (Collider col in insideColliders)
            {
                // TODO: Check if the character has a mask
                if (col.transform.gameObject.tag == tagName)
                {
                    target = col;
                    break;
                }
            }
        }

        if (target)
        {
            // If the character has a mask or is too far, return
            WearMask script = target.transform.gameObject.GetComponent<WearMask>();
            if ((script && script.IsMasked()) || (Vector3.Distance(target.transform.position, transform.position) > range))
            {
                target = null;
                return;
            }
            // Rotation
            Vector3 targetDirection = target.transform.position - transform.position;
            float singleStep = rotationSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            // Fire
            if (timePassed > timeBetweenFire)
            {
                Fire();
                timePassed = 0.0f;
            }
        }
    }

    private void Fire()
    {
        GameObject mask = Instantiate(maskPrefab);

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
