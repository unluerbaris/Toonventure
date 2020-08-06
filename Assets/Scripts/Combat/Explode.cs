using UnityEngine;

namespace Toon.Combat
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
                ExplodeBehaviour();
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                ExplodeBehaviour();
            }
        }

        private void ExplodeBehaviour()
        {
            animator.SetTrigger("explode");
        }

        // Animation event
        void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}
