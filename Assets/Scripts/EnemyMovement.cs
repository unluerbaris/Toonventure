using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Transform groundDetection;
    [SerializeField] float groundDetectionRayDistance = 2f;
    private bool movingLeft = true;

    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDetectionRayDistance);
        if (groundInfo.collider == false)
        {
            if (movingLeft == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingLeft = true;
            }
        }
    }
}
