﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class CharacterController2D : MonoBehaviour
{
    
    public float Speed = 5f;
    public float jumpHeight = 10f;
    public float gravityScale = 1f;
    public int maxJumps = 2;
    private int jumpsRemaining;
    public Camera mainCamera;
    public Animator Animator;
   
   



    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    Vector3 cameraPos;
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform t;

    Vector3 originalPosition;

    AudioSource jumpsound; 

    
    void Start()
    {
        originalPosition = transform.position;


        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
        jumpsRemaining = maxJumps;
        jumpsound = GetComponent<AudioSource>();

        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }


    }

    
   void Update()
{
    
    if ((Input.GetAxis("Horizontal") != 0) && (isGrounded || Mathf.Abs(r2d.velocity.x) > 0.01f))
    {
        moveDirection = Input.GetAxis("Horizontal");
    }
    else
    {
        if (isGrounded || r2d.velocity.magnitude < 0.01f)
        {
            moveDirection = 0;
        }
    }

    
    if (moveDirection != 0)
    {
        if (moveDirection > 0 && !facingRight)
        {
            facingRight = true;
            t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
        }
        if (moveDirection < 0 && facingRight)
        {
            facingRight = false;
            t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
        }
    }

    
    if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpsRemaining > 0))
    {
        if (isGrounded)
        {
            jumpsRemaining = maxJumps - 1; 
        }
        else
        {
            jumpsRemaining--;
        }

        r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        jumpsound.Play();
    }

    // Camera follow
    if (mainCamera)
    {
        mainCamera.transform.position = new Vector3(t.position.x, cameraPos.y, cameraPos.z);
    }
}

void FixedUpdate()
{
    Bounds colliderBounds = mainCollider.bounds;
    float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
    Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
    
    
    bool wasGrounded = isGrounded; 
    isGrounded = false;
    Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
    if (colliders.Length > 0)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != mainCollider)
            {
                isGrounded = true;
                break;
            }
        }
    }

    
    if (isGrounded && !wasGrounded)
    {
        jumpsRemaining = maxJumps;
    }

    
    r2d.velocity = new Vector2((moveDirection) * Speed, r2d.velocity.y);
    
    Animator.SetFloat("Speed", Mathf.Abs(moveDirection * Speed));
    Animator.SetBool("IsJumping", (r2d.velocity.y != 0 && !isGrounded));

    
    Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
    Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);
}





    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Bronze_0_coin"))  			       
            other.gameObject.SetActive(false);  

            if(other.gameObject.CompareTag("Run 0"))
                transform.position = originalPosition; 

   
       }

}