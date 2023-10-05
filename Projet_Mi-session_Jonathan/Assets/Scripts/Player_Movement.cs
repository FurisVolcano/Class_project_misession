using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private float x;
    private float y;
    Rigidbody2D rb2d;
     [SerializeField]
    private float speed;
    

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        rb2d.velocity =Time.deltaTime * speed * new Vector3(x,y,0).normalized;


    }
}