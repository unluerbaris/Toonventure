using UnityEngine;

namespace Toon.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 5f;

        Rigidbody2D projectileRigidBody;

        void Start()
        {
            projectileRigidBody = GetComponent<Rigidbody2D>();
            projectileRigidBody.velocity = speed * transform.right;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }
        }
    }
}
