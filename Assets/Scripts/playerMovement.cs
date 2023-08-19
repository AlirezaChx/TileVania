using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    private Rigidbody2D rb2d;
    private Animator myAnimator;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] float jumpVelocity = 7.5f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform gun;
    private CapsuleCollider2D myBodyCollider;
    private BoxCollider2D myFeetCollider;
    private float defaultGravity;
    private bool isAlive = true;
    private EnemyMoveMents enemy; 
    
    
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        defaultGravity = rb2d.gravityScale;
        myFeetCollider = GetComponent<BoxCollider2D>();
        
    }

    void Update()
    {
        if (!isAlive) {return;}
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) {return;}
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, rb2d.velocity.y);
        rb2d.velocity = playerVelocity;
        
        bool playerHasHorizontalSpeed = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning",playerHasHorizontalSpeed);
        
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rb2d.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb2d.velocity.x), 1f);
        }
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) {return;}
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){return;}

        if (value.isPressed)
        {
            rb2d.velocity += new Vector2(0f, jumpVelocity);
        }
    }
    void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb2d.gravityScale = defaultGravity;
            myAnimator.SetBool("isClimbing",false);
            return;
        }
        

        Vector2 climbVelocity = new Vector2(rb2d.velocity.x,moveInput.y * climbSpeed);
        rb2d.velocity = climbVelocity;
        rb2d.gravityScale = 0f;
        bool playerHasVerticalSpeed = Mathf.Abs(rb2d.velocity.y) > Mathf.Epsilon;
        if (playerHasVerticalSpeed)
        {
            myAnimator.SetBool("isClimbing",true);
        }
        else
        {
            myAnimator.SetBool("isClimbing",false);
        }
        

    }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            rb2d.velocity = deathKick;
            FindObjectOfType<gameSession>().ProcessPlayerDeath();
        }
    }
    
    void OnFire(InputValue value)
    {
        if (!isAlive){return;}

        {
            Instantiate(bullet, gun.position, transform.rotation);
        }
    }
}