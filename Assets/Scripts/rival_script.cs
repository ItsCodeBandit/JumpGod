using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rival_script : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float jumpInterval = 1f;

    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    private float nextJumpTime;
    AudioSource jumpsound;

    private void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
        target = GameObject.Find("The_Thing").transform; 
        nextJumpTime = Time.time + jumpInterval; 
        jumpsound = GetComponent<AudioSource>();
    }

    private void Update(){
        if(target){
            
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = new Vector2(direction.x, direction.y);
        }

        
        if (Time.time >= nextJumpTime) {
            Jump();
            nextJumpTime = Time.time + jumpInterval; 
            jumpsound.Play();
            
        }
    }

    private void FixedUpdate(){
        
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
    }

    private void Jump(){
       
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        
    }
}
