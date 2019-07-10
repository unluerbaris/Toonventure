using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpForce = 20f;

    bool isAlive = true;

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Collider2D myCollider2d;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2d = GetComponent<Collider2D>();
    }

    void Update()
    {
        Run();
        Jump();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal"); //-1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        RunningAnimationAndFlipSprite();
    }

    private void Jump()
    {
        JumpAnimation();
        if (Input.GetButtonDown("Jump") && myCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpForce);
            myRigidbody.velocity += jumpVelocity;
        }
    }

    private void RunningAnimationAndFlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            myAnimator.SetBool("running", true); //Running animation is active
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
        else
        {
            myAnimator.SetBool("running", false); // Stops to play running animation
        }
    }

    private void JumpAnimation() // Do it when on the air without X velocity
    {
        bool isOnTheAir = !myCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if (isOnTheAir)
        {
            myAnimator.SetBool("jumping", true);
        }
        else
        {
            myAnimator.SetBool("jumping", false);
        }
    }
}
