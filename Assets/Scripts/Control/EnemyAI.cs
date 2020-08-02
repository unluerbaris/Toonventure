using UnityEngine;

namespace Toon.Control
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 1f;
        [SerializeField] Transform groundDetection;
        [SerializeField] float groundDetectionRayDistance = 2f;
        private bool movingLeft = true;

        void Update() // Enemy walks to the left first, if it detects no ground at the end of the way it returns and turns direction.
        {             // This code changes the enemy's moving direction if it's the end of the platform
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
}
