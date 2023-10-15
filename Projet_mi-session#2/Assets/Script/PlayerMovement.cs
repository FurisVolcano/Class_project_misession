using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public Transform cellingCheck;
    public Transform groundCheck;
    public LayerMask groundObjects;
    public float checkRadius;
    public int maxJumpCount;
    
    Rigidbody2D rb2d;
    SpriteRenderer _spriteRenderer;
    
    private bool facingRight = true;
    private float moveDirection;
    private bool isJumping = false;
    private bool isGrounded;
    private bool isAttacking = false;
    private int jumpCount;
    Animator animator;
    


    private void Start()
    {
        animator = GetComponent<Animator>();
        jumpCount = maxJumpCount;

    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        InputProcess();
        Animate();

        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
        }
    }
    

    private void ResetMeleeCycle()
    {
      isAttacking = false;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
        if (isGrounded)
        {
            jumpCount = maxJumpCount;
        }
        Move();
    }

    private void Move()
    {
        rb2d.velocity = new Vector2(moveDirection * speed, rb2d.velocity.y);
        if (isJumping)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jumpCount--;
        }

        isJumping = false;
    }

    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
        
        if (Input.GetKey(KeyCode.None))
        {
            animator.Play("Wolf_Idle");
        }
        else if(Input.GetKey("a") || Input.GetKey("d"))
        {
            animator.Play(("Walk_Animation"));
        }
        if (Input.GetKey("space"))
        {
            animator.Play("Jump_Animation");
        }
        if (Input.GetButtonDown("Fire1"))
        {
            animator.Play("Attack#1_Animation");
        }
        if (Input.GetButtonDown("Fire2"))
        {
            animator.Play("Attack#2_Animation");
        }
        if (Input.GetButtonDown("Fire3"))
        {
            animator.Play("Wolf_Dash");
        }
        
    }

    private void InputProcess()
    {
        moveDirection = Input.GetAxisRaw("Horizontal"); //scale of -1 -> 1
        if (Input.GetButtonDown("Jump") && jumpCount > 0) 
        {
            isJumping = true;
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
