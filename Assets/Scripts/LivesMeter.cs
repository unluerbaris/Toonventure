using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesMeter : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayerLivesMeter(int lives)
    {
        if (lives == 2)
        {
            animator.SetTrigger("ThreeToTwo");
        }
        else if (lives == 1)
        {
            animator.SetTrigger("TwoToOne");
        }
        else if (lives == 0)
        {
            animator.SetTrigger("OneToZero");
        }
    }
}
