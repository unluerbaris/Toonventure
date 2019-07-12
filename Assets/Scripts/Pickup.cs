using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] AudioClip carrotPickUpSFX;
    [Range(0,1)][SerializeField] float soundVol = 0.25f;
    GameObject audioListener;

    private void Start()
    {
        audioListener = GameObject.FindWithTag("AudioListener");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(carrotPickUpSFX, audioListener.transform.position, soundVol);
        Destroy(gameObject);
    }
}
