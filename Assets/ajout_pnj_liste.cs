using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ajout_pnj_liste : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            this.gameObject.transform.parent.GetComponent<deplacement_aleatoireTest>().liste_PNJ_bis.Add(other.gameObject);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            this.gameObject.transform.parent.GetComponent<deplacement_aleatoireTest>().liste_PNJ_bis.Remove(other.gameObject);

        }
    }
}