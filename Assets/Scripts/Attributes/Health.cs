using UnityEngine;
using Toon.UI;
using Toon.Core;
using Toon.Control;

namespace Toon.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] int health = 1;
        [SerializeField] bool hasDeathForce = false;
        [SerializeField] HealthBar healthBar = null;

        bool isDead = false;
        Vector2 deathForce = new Vector2(-8f, 30f);

        AudioManager audioManager;
        GameSession gameSession;

        private void Awake()
        {
            audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
            gameSession = GameObject.FindGameObjectWithTag("GameSession").GetComponent<GameSession>();
        }

        public int GetHealth()
        {
            return health;
        }

        public void TakeDamage(float damage)
        {
            health = (int)Mathf.Max(health - damage, 0);

            if (healthBar != null)
            {
                healthBar.UpdateHealthBar(health);
            }

            if (!isDead && health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            isDead = true;
            health = 0;

            if (healthBar != null)
            {
                healthBar.UpdateHealthBar(health);
            }

            // different character types might have different components
            // first check them if they null or not, then disable 
            if (GetComponent<BoxCollider2D>() != null)
            {
                GetComponent<BoxCollider2D>().enabled = false;
            }
            if (GetComponent<PolygonCollider2D>() != null)
            {
                GetComponent<PolygonCollider2D>().enabled = false;
            }
            if (GetComponent<CapsuleCollider2D>() != null)
            {
                GetComponent<CapsuleCollider2D>().enabled = false;
            }

            if (GetComponent<EnemyAI>() != null)
            {
                audioManager.PlaySound("enemyDie");
                gameSession.AddScore(GetComponent<EnemyStats>().GetPoints());
                GetComponent<EnemyAI>().enabled = false;
            }
            if (GetComponent<PlayerControl>() != null)
            {
                audioManager.PlaySound("playerDie");
                GetComponent<PlayerControl>().enabled = false;
                StartCoroutine(gameSession.LoadGameOverScreen());
            }

            if (hasDeathForce)
            {
                GetComponent<Rigidbody2D>().velocity = deathForce;
            }

            GetComponent<Animator>().SetTrigger("die");
        }


        // ANIMATION EVENT
        void DestroyGameObject()
        {
            Destroy(gameObject);
        }
    }
}
