using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 1;
    [SerializeField] bool hasDeathForce = false;

    bool isDead = false;
    Vector2 deathForce = new Vector2(-5f, 20f);

    public void TakeDamage(float damage)
    {
        //takeDamage.Invoke(damage);
        health = (int)Mathf.Max(health - damage, 0);
        if (!isDead && health <= 0)
        {
            //onDie.Invoke();
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

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
        }

        if (hasDeathForce)
        {
            GetComponent<Rigidbody2D>().AddForce(deathForce, ForceMode2D.Impulse);
        }
        
        GetComponent<Animator>().SetTrigger("die");
    }


    // ANIMATION EVENT
    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
