using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    public GameObject grenade;
    public GameObject epaule;

    bool throwing = false;
    int etape = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            throwing = true;
            StartCoroutine(Stop());
        }
        if (throwing)
        {
            if (etape == 1)
            {
                //epaule.transform.rotation = Quaternion.Slerp(epaule.transform.rotation, epaule.transform.rotation + Quaternion.Euler(0, 0, 30), Time.deltaTime * 7.5f);
            }
            if (etape == 2)
            {
                //epaule.transform.rotation = Quaternion.Slerp(epaule.transform.rotation, epaule.transform.rotation + Quaternion.Euler(0, 30, 0), Time.deltaTime * 7.5f);
            }
            if (etape == 3)
            {
                //epaule.transform.rotation = Quaternion.Slerp(epaule.transform.rotation, epaule.transform.rotation + Quaternion.Euler(0, 30, 0), Time.deltaTime * 7.5f);
            }
        }
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(1);
        etape++;
        yield return new WaitForSeconds(1);
        etape++;
        yield return new WaitForSeconds(1);
        throwing = false;
        etape = 1;
    }
}
