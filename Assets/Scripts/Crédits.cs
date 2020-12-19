using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Crédits : MonoBehaviour
{
    public GameObject continuer;

    void Start()
    {
        Cursor.visible = true;
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        continuer.SetActive(true);
        yield return new WaitForSeconds(55);
        SceneManager.LoadScene("MenuPrincipal");
    }

    void Update()
    {
        if (continuer.activeInHierarchy && Input.anyKeyDown)
        {
            SceneManager.LoadScene("MenuPrincipal");
        }
        continuer.GetComponent<Text>().color = Color.Lerp(Color.white, new Color(255,255,255,0), Mathf.PingPong(Time.time * 0.5f, 1));
    }
}
