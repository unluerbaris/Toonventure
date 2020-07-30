using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.collider is BoxCollider2D)
        {
            GetComponent<Health>().TakeDamage(1);
        }
    }
}
