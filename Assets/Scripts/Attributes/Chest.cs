using UnityEngine;
using Toon.Core;

namespace Toon.Attributes
{
    public class Chest : MonoBehaviour
    {
        Animator animator;
        GameSession gameSession;

        bool isTriggered = false;

        void Start()
        {
            animator = GetComponent<Animator>();
            gameSession = gameSession = FindObjectOfType<GameSession>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player" && isTriggered == false)
            {
                isTriggered = true;
                animator.SetTrigger("open");
                StartCoroutine(gameSession.LoadWinScreen());
            }
        }
    }
}
