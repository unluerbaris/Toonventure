using System.Collections;
using UnityEngine;
using Toon.Core;

namespace Toon.Attributes
{
    public class PlayerCollisionBehaviour : MonoBehaviour
    {
        [SerializeField] float collisionAllowTime = 3f;
        [SerializeField] float timeBetweenBlinks = 0.1f;
        float timeSinceLastCollision = Mathf.Infinity;
        bool readyForCollision = true;

        [SerializeField] GameSession gameSession = null;

        AudioManager audioManager;

        private void Awake()
        {
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }

        // Detects most of the enemies
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Enemy" && (collision.collider is PolygonCollider2D || collision.collider is CircleCollider2D) && readyForCollision)
            {
                TakeDamageBehaviour();
            }
        }

        // Detects screen border
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Background")
            {
                GetComponent<Health>().Die();
            }
        }

        // Detects piranha plant
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy" && readyForCollision)
            {
                TakeDamageBehaviour();
            }
            if (collision.gameObject.tag == "Pickup")
            {
                audioManager.PlaySound("pickup");
                gameSession.AddScore(collision.gameObject.GetComponent<Pickup>().GetScore());
                Destroy(collision.gameObject);
            }
        }

        private void Update()
        {
            timeSinceLastCollision += Time.deltaTime;
        }

        private void TakeDamageBehaviour()
        {
            audioManager.PlaySound("takeDamage");
            readyForCollision = false;
            gameObject.tag = "PlayerDisable";
            timeSinceLastCollision = 0f;
            GetComponent<Health>().TakeDamage(1);

            if (GetComponent<Health>().GetHealth() <= 0) return;

            StartCoroutine(BlinkAnimation());
        }

        IEnumerator BlinkAnimation()
        {
            audioManager.PlaySound("blinking");

            while (timeSinceLastCollision < collisionAllowTime)
            {
                GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.4f);
                yield return new WaitForSeconds(timeBetweenBlinks);
                GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 1f);
                yield return new WaitForSeconds(timeBetweenBlinks);
            }

            audioManager.StopPlaySound("blinking");

            readyForCollision = true;
            gameObject.tag = "Player";
            StopCoroutine("BlinkAnimation");
        }
    }
}
