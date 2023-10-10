using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Movement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    
    
    Rigidbody2D rb2d;
    private bool facingRight = true;
    private float moveDirection;
    private bool isJumping = false;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    
    private void Update()
    {
        InputProcess();
        Animate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb2d.velocity = new Vector2(moveDirection * speed, rb2d.velocity.y);
        if (isJumping)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce));
        }

        isJumping = false;
    }

    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void InputProcess()
    {
        moveDirection = Input.GetAxisRaw("Horizontal"); //scale of -1 -> 1
        if (Input.GetButtonDown("Jump")) 
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
