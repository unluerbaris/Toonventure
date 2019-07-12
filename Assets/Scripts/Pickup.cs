using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] int score = 50;
    [SerializeField] AudioClip carrotPickUpSFX;
    [Range(0,1)][SerializeField] float soundVol = 0.25f;
    GameObject audioListener;
    GameSession gameSession;

    private void Start()
    {
        audioListener = GameObject.FindWithTag("AudioListener");
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(carrotPickUpSFX, audioListener.transform.position, soundVol);
        gameSession.AddToScore(score);
        Destroy(gameObject);
    }
}
