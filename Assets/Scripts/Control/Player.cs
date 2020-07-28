using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpForce = 20f;
    [SerializeField] float climbSpeed = 5f;
    
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    
    BoxCollider2D myFeetCollider;
    
    float gravityScaleAtStart;
    
    float enterClimbingState; // Timer to control climbing animation

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        
        myFeetCollider = GetComponent<BoxCollider2D>();
        
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    void Update()
    {
        //if (!isAlive) { return; }
        Run();
        Jump();
        Climb();
        //Die();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal"); //-1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("running", playerHasHorizontalSpeed);
        FlipSprite();
    }

    private void Jump()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpForce);
            myRigidbody.velocity += jumpVelocity;
        }
    }

    private void Climb()//TODO check jump on the ladder action??? And better climbing function??
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")) || Input.GetButtonDown("Jump"))
        {
            enterClimbingState = 0;
            myAnimator.speed = 1;
            myAnimator.SetBool("climbing", false);
            myRigidbody.gravityScale = gravityScaleAtStart;
            return;
        }
        float climbThrow = Input.GetAxis("Vertical"); //-1 to +1
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, climbThrow * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        if (playerHasVerticalSpeed)
        {
            enterClimbingState += Time.deltaTime;
            myAnimator.speed = 1;
            myAnimator.SetBool("climbing", playerHasVerticalSpeed);
        }
        else if(!playerHasVerticalSpeed && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")) && enterClimbingState >= 0.1f)
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
