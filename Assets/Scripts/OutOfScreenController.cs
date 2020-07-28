using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfScreenController : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)  // When player falls down it dies
    {
        if (collision.tag == "Player")
        {
            Debug.Log("player dies");
        }
    }
}
