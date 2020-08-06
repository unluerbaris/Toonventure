using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toon.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [SerializeField] float destroyProjectileTime = 5f;

        Rigidbody2D projectileRigidBody;

        void Start()
        {
            projectileRigidBody = GetComponent<Rigidbody2D>();
            projectileRigidBody.velocity = speed * transform.right;
        }
    }
}
