using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfScreenController : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.playerLives = 0;
        }
    }
}
