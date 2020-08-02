using UnityEngine;

namespace Toon.UI
{
    public class HealthBar : MonoBehaviour
    {
        Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void UpdateHealthBar(int lives)
        {
            if (lives == 2)
            {
                animator.SetTrigger("toTwo");
            }
            else if (lives == 1)
            {
                animator.SetTrigger("toOne");
            }
            else if (lives == 0)
            {
                animator.SetTrigger("toZero");
            }
        }
    }
}
