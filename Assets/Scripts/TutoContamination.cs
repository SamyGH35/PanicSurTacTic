using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoContamination : MonoBehaviour
{
    public Slider contamination;
    public Text taux;

    void Update()
    {
        contamination.value = Contagion.contamination * 100 / Contagion.nbInstance;
        taux.text = contamination.value + "%";
    }
}
