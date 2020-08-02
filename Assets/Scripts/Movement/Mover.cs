﻿using UnityEngine;

namespace Toon.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float jumpForce = 20f;
        [SerializeField] float climbSpeed = 5f;

        Rigidbody2D myRigidbody;
        Animator myAnimator;
        BoxCollider2D myFeetCollider;

        Vector2 velocity;
        Vector2 climbVelocity;
        bool hasHorizontalSpeed;
        bool hasVerticalSpeed;
        float gravityScaleAtStart;
        float enterClimbingState; // Timer to control climbing animation

        void Start()
        {
            myRigidbody = GetComponent<Rigidbody2D>();
            myAnimator = GetComponent<Animator>();
            myFeetCollider = GetComponent<BoxCollider2D>();
            gravityScaleAtStart = myRigidbody.gravityScale;
        }

        public void Move(float controlThrow)
        {
            velocity = new Vector2(controlThrow * moveSpeed, myRigidbody.velocity.y);
            myRigidbody.velocity = velocity;
            hasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
            myAnimator.SetBool("moving", hasHorizontalSpeed);
            FlipSprite();
        }

        public void Jump()
        {
            if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

            Vector2 jumpVelocity = new Vector2(0f, jumpForce);
            myRigidbody.velocity += jumpVelocity;
        }

        public void Climb(float climbThrow)//TODO check jump on the ladder action??? And better climbing function??
        {
            if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")) || Input.GetButtonDown("Jump"))
            {
                enterClimbingState = 0;
                myAnimator.speed = 1;
                myAnimator.SetBool("climbing", false);
                myRigidbody.gravityScale = gravityScaleAtStart;
                return;
            }

            climbVelocity = new Vector2(myRigidbody.velocity.x, climbThrow * climbSpeed);
            myRigidbody.velocity = climbVelocity;
            myRigidbody.gravityScale = 0f;

            hasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
            if (hasVerticalSpeed)
            {
                enterClimbingState += Time.deltaTime;
                myAnimator.speed = 1;
                myAnimator.SetBool("climbing", hasVerticalSpeed);
            }
            else if (!hasVerticalSpeed && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")) && enterClimbingState >= 0.1f)
            {
                myAnimator.speed = 0;
                // myAnimator.SetBool("climbing", false);
            }
        }

        private void FlipSprite()
        {
            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
            if (playerHasHorizontalSpeed)
            {
                transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
            }
        }
    }

}