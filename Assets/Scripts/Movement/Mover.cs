using UnityEngine;

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
        CapsuleCollider2D bodyCollider;

        Vector2 velocity;
        Vector2 climbVelocity;
        Vector2 defaultBodyColliderOffset;
        Vector2 defaultBodyColliderSize;
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

            if (gameObject.tag == "Player")
            {
                bodyCollider = GetComponent<CapsuleCollider2D>();
                defaultBodyColliderOffset = bodyCollider.offset;
                defaultBodyColliderSize = bodyCollider.size;
            }
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

        public void Climb(float climbThrow)
        {
            // If character is not on the ladder
            if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
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

            // If charater is moving vertically on the ladder
            if (hasVerticalSpeed)
            {
                myAnimator.SetBool("climbing", true);
                enterClimbingState += Time.deltaTime;
                myAnimator.speed = 1;
                myAnimator.SetBool("climbing", hasVerticalSpeed);
            }
            // If character is not moving on the ladder
            else if (!hasVerticalSpeed && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")) && enterClimbingState >= 0.1f)
            {
                myAnimator.SetBool("climbing", true);
                myAnimator.speed = 0;
            }
        }

        public void Dodge(bool isDodging)
        {
            if (isDodging)
            {
                myAnimator.SetBool("moving", false);
                bodyCollider.offset = new Vector2(0.091f, -0.475f);
                bodyCollider.size = new Vector2(1.03f, 1.03f);
                myAnimator.SetBool("duck", true);
            }
            else
            {
                bodyCollider.offset = defaultBodyColliderOffset;
                bodyCollider.size = defaultBodyColliderSize;
                myAnimator.SetBool("duck", false);
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