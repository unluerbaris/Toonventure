using UnityEngine;
using Toon.Core;

namespace Toon.Attributes
{
    public class Chest : MonoBehaviour
    {
        Animator animator;
        GameSession gameSession;
        AudioManager audioManager;

        bool isTriggered = false;

        void Start()
        {
            animator = GetComponent<Animator>();
            gameSession = gameSession = FindObjectOfType<GameSession>();
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player" && isTriggered == false)
            {
                isTriggered = true;
                audioManager.PlaySound("win");
                animator.SetTrigger("open");
                //audioManager.StopPlaySound("Theme");
                StartCoroutine(gameSession.LoadWinScreen());
                //audioManager.PlaySound("Theme");
            }
        }
    }
}
