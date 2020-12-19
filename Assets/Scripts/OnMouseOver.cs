using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public AudioClip overSound;
    public AudioClip clickSound;

    public AudioSource source;

    public void OnPointerEnter(PointerEventData eventData)
    {
        source.volume = (float) PlayerPrefs.GetInt("VolumeSons") / 100;
        source.PlayOneShot(overSound);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        source.volume = (float) PlayerPrefs.GetInt("VolumeSons") / 100;
        source.PlayOneShot(clickSound);
    }
}
