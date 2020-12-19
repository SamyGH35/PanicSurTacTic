using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ajout_pnj_liste : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            GetComponent<deplacement_aleatoireTest>().liste_PNJ_bis.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            GetComponent<deplacement_aleatoireTest>().liste_PNJ_bis.Remove(other.gameObject);
        }
    }
}