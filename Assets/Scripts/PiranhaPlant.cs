using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiranhaPlant : MonoBehaviour
{
    GameObject player;
    Animator myAnimator;
    float attackDistance = 4f;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        Attack();
    }

    private void Attack() // TODO hurt the player
    {
        FlipSprite();
        if (InAttackRange())
        {
            myAnimator.SetBool("attack", true);
        }
        else
        {
            myAnimator.SetBool("attack", false);
        }
    }

    private void FlipSprite()
    {
        float playerPosX = player.transform.position.x;
        float plantPosX = transform.position.x;
        float distance = playerPosX - plantPosX;

        if (distance < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }
    }

    private bool InAttackRange()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        return distanceToPlayer <= attackDistance;
    }
}
