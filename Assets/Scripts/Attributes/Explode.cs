using UnityEngine;

namespace Toon.Attributes
{
    public class Explode : MonoBehaviour
    {
        Animator animator;
        float currentTime = 0;
        [SerializeField] float explosionTime = 3f;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= explosionTime)
            {
                animator.SetTrigger("explode");
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                animator.SetTrigger("explode");
            }
        }

        // Animation event
        void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}
