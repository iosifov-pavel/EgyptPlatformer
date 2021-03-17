using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 2f;
    [Range(0, .5f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] float jumpForce = 8f;
    [Header("Ground Settings")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask whatIsGround;
    public bool isGrounded = true;
    [SerializeField] float groundedWidth = 0.05f;
    [SerializeField] float groundedHeight = 0.05f;
    Vector2 input = Vector2.zero;
    Vector2 targetVelocity = Vector2.zero;
    Vector2 resultVelocity = Vector2.zero;
    Rigidbody2D playerRigidbody;

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, new Vector2(groundedWidth,groundedHeight));
    }
    void Start(){
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        CheckGround();
        GetInput();
        CalculateVelocity();
        Move();
        Jump();
    }

    void CheckGround(){
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(groundCheck.position, new Vector2(groundedWidth, groundedHeight), 0, whatIsGround);
        //Collider2D[] cols = Physics2D.OverlapBox()
        if(colliders.Length==0){
            return;
        }
        else isGrounded = true;
    }

    private void GetInput(){

    }

    private void CalculateVelocity(){
        targetVelocity = new Vector2(input.x*speed, playerRigidbody.velocity.y);
    }

    void Move(){
        playerRigidbody.velocity = Vector2.SmoothDamp(playerRigidbody.velocity,targetVelocity, ref resultVelocity, m_MovementSmoothing);
    }
    
    void Jump(){

    }

    public void SetMove(Vector2 dest){
        input = dest;
    }
}
