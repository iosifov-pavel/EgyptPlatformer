using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    
    [Header("Basic Control")]
    [SerializeField] float speed = 1.5f;
    [Range(0, .5f)] [SerializeField] private float groundSmoothing = .1f;
    [Range(0, .5f)] [SerializeField] private float jumpingSmoothing = .2f;
    private float MovementSmoothing;
    [SerializeField] float jumpForce = 8f;
    float maxJumpCount = 2;
    float currentJumps = 0;
    bool jumpButton = false;
    bool isJumping = false;
    bool isFalling = false;
    bool lastGroundCheck = true;
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
    
    [Header("Dust Particles")]

    [SerializeField] AudioSource stepsSound;
    [SerializeField] ParticleSystem stepsDust;
    [SerializeField] ParticleSystem jumpOnGroundDust;
    ParticleSystem.EmissionModule stepDustEmission;


    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, new Vector2(groundedWidth,groundedHeight));
    }
    void Start(){
        stepDustEmission = stepsDust.emission;
        stepDustEmission.enabled=false;
        playerRigidbody = GetComponent<Rigidbody2D>();
        MovementSmoothing = groundSmoothing;
    }

    // Update is called once per frame
    void Update(){
        CheckGround();
        StepDust();
        //GetInput();
    }

    private void FixedUpdate() {
        CalculateVelocity();
        Move();
        Jump();
    }

    void CheckGround(){
        isGrounded = false;
        Vector2 groundCheckSize = new Vector2(groundedWidth,groundedHeight);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(groundCheck.position, groundCheckSize, 0, whatIsGround);
        if(colliders.Length==0){
            if(!isFalling && !isJumping && lastGroundCheck){
                isFalling = true;
                currentJumps=1;
            }
            lastGroundCheck = false;
            MovementSmoothing = jumpingSmoothing;
            return;
        }
        else{
            isGrounded = true;
            lastGroundCheck = true;
            if(isJumping || isFalling) jumpOnGroundDust.Play();
            isJumping = false;
            isFalling = false;
            MovementSmoothing = groundSmoothing;
            currentJumps = 0;
        } 
    }

    public void GetInput(Vector2 dest){
        input = dest;
    }
    public void ResetJumpCount(){
        currentJumps=1;
    }

    private void CalculateVelocity(){
        targetVelocity = new Vector2(input.x*speed, playerRigidbody.velocity.y);
    }

    void Move(){
        playerRigidbody.velocity = Vector2.SmoothDamp(playerRigidbody.velocity,targetVelocity, ref resultVelocity, MovementSmoothing);
    }
    
    void Jump(){
        if(jumpButton && currentJumps<2){
            isJumping = true;
            isFalling = false;
            currentJumps++;
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            jumpButton = false;
        }
    }

    public void setJumpButton(bool state){
        jumpButton = state;
    }

    void StepDust(){
        if(Mathf.Abs(playerRigidbody.velocity.x)>=0.1f && isGrounded){
            stepDustEmission.enabled = true;
            if(!stepsSound.isPlaying)stepsSound.Play();
        } 
        else{
            stepDustEmission.enabled = false;
            stepsSound.Stop();
        } 
    }
}
