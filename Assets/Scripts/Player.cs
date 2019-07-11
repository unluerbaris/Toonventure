using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpForce = 20f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float dashSpeed = 9f;

    bool isAlive = true;

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Collider2D myCollider2d;
    float gravityScaleAtStart;
    float startValueOfRunSpeed;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2d = GetComponent<Collider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
        startValueOfRunSpeed = runSpeed;
    }

    void Update()
    {
        Run();
        Dash();
        Jump();
        Climb();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal"); //-1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        RunningAnimationAndFlipSprite();
    }

    private void Dash()
    {
        if (Input.GetButton("Dash") && myCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            runSpeed = dashSpeed; 
        }
        else
        {
            runSpeed = startValueOfRunSpeed;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && myCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpForce);
            myRigidbody.velocity += jumpVelocity;
        }
    }

    private void Climb()
    {
        if (!myCollider2d.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("climbing", false);
            myRigidbody.gravityScale = gravityScaleAtStart;
            return;
        }
        myRigidbody.gravityScale = 0;
        float climbThrow = Input.GetAxis("Vertical"); //-1 to +1
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, climbThrow * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        ClimbingAnimation();
    }

    private void RunningAnimationAndFlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed && !myCollider2d.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("running", true); //Running animation is active
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
        else
        {
            myAnimator.SetBool("running", false); // Stops to play running animation
        }
    }

    private void ClimbingAnimation()
    {
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        if (playerHasVerticalSpeed)
        {
            myAnimator.SetBool("climbing", true);
        }
    }
}
