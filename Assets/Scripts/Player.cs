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
    float onTheAirTime;
    float glideOnTheAirTimeLimit = 0.5f; // Between 0-0.5secs while on the air
                                        // dashSpeed on X axis can effect while Jumping or Gliding 

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
        Sprint();
        Jump();
        OnTheAirTime();
        Climb();
        Debug.Log(onTheAirTime);
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal"); //-1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        RunningAnimationAndFlipSprite();
    }

    private void Sprint()
    {
        if (Input.GetButton("Dash") && onTheAirTime <= glideOnTheAirTimeLimit)
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

    private void OnTheAirTime()
    {
        if (myCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground")) || 
            myCollider2d.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            onTheAirTime = 0f;
            return;
        }
        onTheAirTime += Time.deltaTime;
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
