using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    [SerializeField] AudioClip hoverSFX;
    [Range(0, 1)] [SerializeField] float hoverSFXVolume = 1f;
    [SerializeField] AudioClip clickSFX;
    [Range(0, 1)] [SerializeField] float clickSFXVolume = 1f;

    GameObject audioListener;

    private void Start()
    {
        audioListener = GameObject.FindWithTag("AudioListener");
    }

    public void PlayHoverSFX()
    {
        if (hoverSFX != null)
        {
            AudioSource.PlayClipAtPoint(hoverSFX, audioListener.transform.position, hoverSFXVolume);
        }
    }

    public void PlayClickSFX()
    {
        if (clickSFX != null)
        {
            AudioSource.PlayClipAtPoint(clickSFX, audioListener.transform.position, clickSFXVolume);
        }
    }
}
