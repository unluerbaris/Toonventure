using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 1;
    [SerializeField] bool hasDeathForce = false;
    [SerializeField] HealthBar healthBar = null;
    [SerializeField] GameSession gameSession = null;

    bool isDead = false;
    Vector2 deathForce = new Vector2(-8f, 30f);

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
            GetComponent<EnemyAI>().enabled = false;
        }
        if (GetComponent<PlayerControl>() != null)
        {
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
