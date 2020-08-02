using UnityEngine;
using Toon.Core;

namespace Toon.Attributes
{
    public class Chest : MonoBehaviour
    {
        [SerializeField] AudioClip winSFX;
        [Range(0, 1)] [SerializeField] float winSFXVolume = 1f;

        Animator animator;
        GameSession gameSession;
        GameObject audioListener;

        bool isTriggered = false;

        void Start()
        {
            animator = GetComponent<Animator>();
            gameSession = gameSession = FindObjectOfType<GameSession>();
            audioListener = GameObject.FindWithTag("AudioListener");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player" && isTriggered == false)
            {
                isTriggered = true;
                animator.SetTrigger("open");
                AudioSource.PlayClipAtPoint(winSFX, audioListener.transform.position, winSFXVolume);
                audioListener.GetComponent<AudioSource>().enabled = false; // stop playing main theme
                StartCoroutine(gameSession.LoadWinScreen());
            }
        }
    }
}
