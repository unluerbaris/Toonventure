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
    }
}
