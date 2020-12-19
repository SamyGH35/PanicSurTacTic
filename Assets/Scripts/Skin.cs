using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
    public Material material1;
    public Material material2;
    public Material material3;
    public Material material4;
    public Material material5;
    public Material material6;
    public Material material7;

    public Material materialsColor1;
    public Material materialsColor2;
    public Material materialsColor3;
    public Material materialsColor4;
    public Material materialsColor5;
    public Material materialsColor6;
    public Material materialsColor7;

    public GameObject skin;
    public bool epauleExists;
    public bool leftArmInEpaule;
    public GameObject epaule;

    private Material[] materials;
    private Material[] materialsColor;

    // Start is called before the first frame update
    void Start()
    {
        materials = new Material[7];
        materials[0] = material1;
        materials[1] = material2;
        materials[2] = material3;
        materials[3] = material4;
        materials[4] = material5;
        materials[5] = material6;
        materials[6] = material7;
        materialsColor = new Material[7];
        materialsColor[0] = materialsColor1;
        materialsColor[1] = materialsColor2;
        materialsColor[2] = materialsColor3;
        materialsColor[3] = materialsColor4;
        materialsColor[4] = materialsColor5;
        materialsColor[5] = materialsColor6;
        materialsColor[6] = materialsColor7;
        skin.GetComponent<MeshRenderer>().material = materials[PlayerPrefs.GetInt("SkinUsed", 1) - 1];
        if (leftArmInEpaule)
        {
            if (epauleExists)
            {
                epaule.transform.Find("Bras gauche").gameObject.GetComponent<MeshRenderer>().material = materialsColor[PlayerPrefs.GetInt("SkinUsed", 1) - 1];
            }
            else
            {
                skin.transform.Find("Bras gauche").gameObject.GetComponent<MeshRenderer>().material = materialsColor[PlayerPrefs.GetInt("SkinUsed", 1) - 1];
            }
            skin.transform.Find("Bras droit").gameObject.GetComponent<MeshRenderer>().material = materialsColor[PlayerPrefs.GetInt("SkinUsed", 1) - 1];
        }
        else
        {
            if (epauleExists)
            {
                epaule.transform.Find("Bras droit").gameObject.GetComponent<MeshRenderer>().material = materialsColor[PlayerPrefs.GetInt("SkinUsed", 1) - 1];
            }
            else
            {
                skin.transform.Find("Bras droit").gameObject.GetComponent<MeshRenderer>().material = materialsColor[PlayerPrefs.GetInt("SkinUsed", 1) - 1];
            }
            skin.transform.Find("Bras gauche").gameObject.GetComponent<MeshRenderer>().material = materialsColor[PlayerPrefs.GetInt("SkinUsed", 1) - 1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
