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
        GetComponent<Animator>().SetTrigger("die");
        Debug.Log($"{gameObject.name} is dead");
    }
}
