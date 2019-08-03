﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpForce = 20f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] int enemyPoints = 50;
    public int playerLives = 3;
    [SerializeField] float damageAllowTime = 1f;
    [Range(0, 1)] [SerializeField] float playerSoundVol = 0.25f;
    [Range(0, 1)] [SerializeField] float loseSoundVol = 1f;
    [Range(0, 1)] [SerializeField] float enemyDeathVolume = 0.25f;
    [SerializeField] AudioClip takeDamageSFX;
    [SerializeField] AudioClip loseSFX;
    [SerializeField] AudioClip enemyDeathSFX;
    Vector2 deathAnimation = new Vector2(-5f, 20f);

    bool isAlive = true;

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2d;
    BoxCollider2D myFeetCollider;
    LivesMeter livesMeter;
    GameObject audioListener;
    GameSession gameSession;
    float gravityScaleAtStart;
    float timeSinceLastHit; //Enemy only can hit once in per X second
    float enterClimbingState; // Timer to control climbing animation

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2d = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        livesMeter = FindObjectOfType<LivesMeter>();
        gameSession = FindObjectOfType<GameSession>();
        audioListener = GameObject.FindWithTag("AudioListener");
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    void Update()
    {
        if (!isAlive) { return; }
        Run();
        Jump();
        Climb();
        Die();
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

    private void Die()
    {
        timeSinceLastHit += Time.deltaTime;

        if (myBodyCollider2d.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            if (playerLives > 1 && timeSinceLastHit >= damageAllowTime) //TODO inform player with effect 
                                                                        //when it takes damage
            {
                AudioSource.PlayClipAtPoint(takeDamageSFX, audioListener.transform.position, playerSoundVol);
                LoseLives();
                livesMeter.PlayerLivesMeter(playerLives);
                timeSinceLastHit = 0;
            }
            else if (timeSinceLastHit >= damageAllowTime)
            {
                AudioSource.PlayClipAtPoint(loseSFX, audioListener.transform.position, loseSoundVol);
                LoseLives();
                livesMeter.PlayerLivesMeter(playerLives);
                isAlive = false;
                myRigidbody.velocity = deathAnimation;
                myAnimator.SetTrigger("death");
                myBodyCollider2d.enabled = false;
                myFeetCollider.enabled = false;
                Destroy(gameObject, 1f);
            }
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

    private void LoseLives()
    {
        playerLives--;
    }

    public void DestroyEnemy(Collider2D enemyCollider) //TODO fix the destroy enemy system
    {
        Animator enemyAnim = enemyCollider.gameObject.GetComponent<Animator>();

        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            AudioSource.PlayClipAtPoint(enemyDeathSFX, audioListener.transform.position, enemyDeathVolume);
            enemyAnim.SetTrigger("die");
            gameSession.AddToScore(enemyPoints);
            Destroy(enemyCollider.gameObject, 0.3f);
        }
    }
}
