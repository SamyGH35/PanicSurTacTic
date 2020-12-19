using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noeud_routage : MonoBehaviour
{
    public GameObject[] tableau_noeud_choix;
    public float[] tableau_noeud_cout;
    public GameObject[] tableau_noeud_destination;

    public GameObject this_object;

    public List<GameObject> noeud_voisin;

    // Start is called before the first frame update
    void Start()
    {
        tableau_noeud_choix = GameObject.FindGameObjectsWithTag("noeud_routage");
        tableau_noeud_cout = new float[tableau_noeud_choix.Length];
        tableau_noeud_destination = new GameObject[tableau_noeud_choix.Length];

        for (int i = 0; i < tableau_noeud_choix.Length; i++)
        {
            tableau_noeud_cout[i] = Mathf.Infinity;
            if (Mathf.Abs(transform.position.x - tableau_noeud_choix[i].transform.position.x) == 0 && Mathf.Abs(transform.position.z - tableau_noeud_choix[i].transform.position.z) == 0)
            {
                this_object = tableau_noeud_choix[i];
                tableau_noeud_cout[i] = 0;
                tableau_noeud_destination[i] = this_object;
            }
        }

        for (int i = 0; i < tableau_noeud_choix.Length; i++)
        {
            for(int j = 0; j < noeud_voisin.Count; j++)
            {
                if(tableau_noeud_choix[i] == noeud_voisin[j])
                {
                    Vector3 vec_prov = new Vector3(tableau_noeud_choix[i].transform.position.x - transform.position.x, 0.0f, tableau_noeud_choix[i].transform.position.z - transform.position.z);
                    float distance_noeud = Mathf.Sqrt(vec_prov.x * vec_prov.x + vec_prov.y * vec_prov.y + vec_prov.z * vec_prov.z);
                    tableau_noeud_cout[i] = distance_noeud;
                    tableau_noeud_destination[i] = tableau_noeud_choix[i];
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < noeud_voisin.Count; i++)
        {
            for (int j = 0; j < tableau_noeud_choix.Length; j++)
            {
                Vector3 vec_prov = new Vector3(noeud_voisin[i].transform.position.x - transform.position.x, 0.0f, noeud_voisin[i].transform.position.z - transform.position.z);
                float distance_noeud = Mathf.Sqrt(vec_prov.x * vec_prov.x + vec_prov.y * vec_prov.y + vec_prov.z * vec_prov.z);

                if (distance_noeud + tableau_noeud_cout[j] < noeud_voisin[i].GetComponent<noeud_routage>().tableau_noeud_cout[j])
                {
                    noeud_voisin[i].GetComponent<noeud_routage>().tableau_noeud_cout[j] = distance_noeud + tableau_noeud_cout[j];
                    noeud_voisin[i].GetComponent<noeud_routage>().tableau_noeud_destination[j] = this_object;
                }
            }
        }
    }
}
