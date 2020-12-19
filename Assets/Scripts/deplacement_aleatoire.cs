using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deplacement_aleatoire : MonoBehaviour
{
    private Rigidbody rb;
    public float movementSpeed = 0.5f;
    public float rotationSpeed = 3;

    float proxi_noeud = 2;
    float proxi_pnj = 2.5f;
    float repousse_pnj = -1f;

    GameObject[] noeud_routage;
    public GameObject[] liste_PNJ;
    GameObject noeud_routage_choix;
    GameObject noeud_routage_etape;

    public Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        noeud_routage = GameObject.FindGameObjectsWithTag("noeud_routage");
        liste_PNJ = GameObject.FindGameObjectsWithTag("PNJ");

        noeud_routage_choix = noeud_routage[0];
        Vector3 vec_prov = new Vector3(noeud_routage[0].transform.position.x - transform.position.x, 0.0f, noeud_routage[0].transform.position.z - transform.position.z);
        float distance_noeud = Mathf.Sqrt(vec_prov.x * vec_prov.x + vec_prov.y * vec_prov.y + vec_prov.z * vec_prov.z);



        for (int i = 1; i < noeud_routage.Length; i++)
        {
            vec_prov = new Vector3(noeud_routage[i].transform.position.x - transform.position.x, 0.0f, noeud_routage[i].transform.position.z - transform.position.z);
            float distance_noeud_prov = Mathf.Sqrt(vec_prov.x * vec_prov.x + vec_prov.y * vec_prov.y + vec_prov.z * vec_prov.z);

            if (distance_noeud_prov < distance_noeud)
            {
                distance_noeud = distance_noeud_prov;
                noeud_routage_choix = noeud_routage[i];
            }
        }

        noeud_routage_etape = noeud_routage_choix;
    }

    // Update is called once per frame
    void Update()
    {
           
        //transform.Rotate(new Vector3(0.0f, 0.0f, 0.0f) * rotationSpeed);
    }

    void FixedUpdate()
    {
        movement = new Vector3(noeud_routage_etape.transform.position.x - transform.position.x, 0.0f, noeud_routage_etape.transform.position.z - transform.position.z);
        movement = movement.normalized;

        if (Mathf.Abs(transform.position.x - noeud_routage_choix.transform.position.x) < proxi_noeud && Mathf.Abs(transform.position.z - noeud_routage_choix.transform.position.z) < proxi_noeud)
        {
            noeud_routage_choix = noeud_routage[Random.Range(0, noeud_routage.Length)];
        }

        if (Mathf.Abs(transform.position.x - noeud_routage_etape.transform.position.x) < proxi_noeud && Mathf.Abs(transform.position.z - noeud_routage_etape.transform.position.z) < proxi_noeud)
        {
            for (int i = 0; i < noeud_routage_etape.GetComponent<noeud_routage>().tableau_noeud_choix.Length; i++)
            {
                if (noeud_routage_etape.GetComponent<noeud_routage>().tableau_noeud_choix[i] == noeud_routage_choix)
                {
                    noeud_routage_etape = noeud_routage_etape.GetComponent<noeud_routage>().tableau_noeud_destination[i];
                    i = 1000000;
                }
            }
        }

        for (int i = 0; i < liste_PNJ.Length; i++)
        {
            if (Mathf.Abs(transform.position.x - liste_PNJ[i].transform.position.x) < proxi_pnj && Mathf.Abs(transform.position.z - liste_PNJ[i].transform.position.z) < proxi_pnj)
            {
                Vector3 vec_prov = new Vector3(liste_PNJ[i].transform.position.x - transform.position.x, 0.0f, liste_PNJ[i].transform.position.z - transform.position.z);
                float vec_prov_norme = Mathf.Sqrt(vec_prov.x * vec_prov.x + vec_prov.y * vec_prov.y  + vec_prov.z * vec_prov.z);
                //vec_prov = vec_prov * repousse_pnj;
                vec_prov = vec_prov.normalized;
                vec_prov = vec_prov * repousse_pnj * (proxi_pnj - vec_prov_norme);
                movement = new Vector3(movement.x + vec_prov.x, movement.y, movement.z + vec_prov.z) ;
                movement = movement.normalized;
            }
        }

        movement = new Vector3(movement.x * movementSpeed, movement.y * movementSpeed, movement.z * movementSpeed);
        transform.Translate(movement);
    }
}