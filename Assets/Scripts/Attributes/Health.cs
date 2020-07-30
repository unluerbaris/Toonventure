using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 1;
    [SerializeField] float damageAllowTime = 1f;

    //float timeSinceLastHit; //Enemy only can hit once in per X second
    bool isDead = false;


    //Vector2 deathAnimation = new Vector2(-5f, 20f);

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

        if (GetComponent<EnemyAI>() != null)
        {
            GetComponent<EnemyAI>().enabled = false;
        }
        
        GetComponent<Animator>().SetTrigger("die");
        Debug.Log($"{gameObject.name} is dead");
    }


    // ANIMATION EVENT
    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
