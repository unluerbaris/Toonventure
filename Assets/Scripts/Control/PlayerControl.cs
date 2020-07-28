﻿using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Mover mover;
    float climbThrow;
    float controlThrow;

    private void Start()
    {
        mover = GetComponent<Mover>();
    }

    private void Update()
    {
        //if (!isAlive) { return; }
        MoveInput();
        JumpInput();
        ClimbInput();
        //Die();
    }

    private void MoveInput()
    {
        controlThrow = Input.GetAxis("Horizontal"); //-1 to +1
        mover.Move(controlThrow);
    }

    private void JumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            mover.Jump();
        }
    }

    private void ClimbInput()
    {
        climbThrow = Input.GetAxis("Vertical"); //-1 to +1
        mover.Climb(climbThrow);
    }
}
