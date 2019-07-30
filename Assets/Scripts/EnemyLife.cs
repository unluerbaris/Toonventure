using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    Player player;
    BoxCollider2D boxCollider2D;
    Animator enemyAnim;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        enemyAnim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        player.DestroyEnemy(boxCollider2D); // controlled by player.cs
    }
}
