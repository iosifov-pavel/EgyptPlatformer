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
    Vector3 movementMultiplier;
    float maxJumpCount = 2;
    float currentJumps = 0;
    float gravityOriginal;
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
    bool flipBlock = false, moveBlock=false, jumpBlock = false;
    Vector2 targetVelocity = Vector2.zero;
    Vector2 resultVelocity = Vector2.zero;
    Rigidbody2D playerRigidbody;
    Player_Health player_Health;
    
    [Header("Dust Particles")]

    [SerializeField] AudioSource stepsSound;
    [SerializeField] ParticleSystem stepsDust;
    [SerializeField] ParticleSystem jumpOnGroundDust;
    ParticleSystem.EmissionModule stepDustEmission;
    List<Force> forces = new List<Force>();

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, new Vector2(groundedWidth,groundedHeight));
        Vector2 draw= (Vector2)transform.position + input;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,draw);
    }
    void Start(){
        player_Health = GetComponent<Player_Health>();
        movementMultiplier.x = speed;
        movementMultiplier.y = jumpForce;
        movementMultiplier.z = jumpForce;
        stepDustEmission = stepsDust.emission;
        stepDustEmission.enabled=false;
        playerRigidbody = GetComponent<Rigidbody2D>();
        MovementSmoothing = groundSmoothing;
        gravityOriginal = playerRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update(){
        CheckGround();
        if(!moveBlock)StepDust();
        if(!flipBlock)Flip();

        //GetInput();
    }

    private void FixedUpdate() {
        CalculateVelocity();
        if(player_Health.dead) return;
        Move();
        AdditionalMove();
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

    void Flip(){
        if(input.x>0){
            Vector3 newscale = transform.localScale;
            newscale.x = Mathf.Abs(newscale.x);
            transform.localScale = newscale;
        }
        else if(input.x<0){           
            Vector3 newscale = transform.localScale;
            newscale.x = -Mathf.Abs(newscale.x);
            transform.localScale = newscale;
        }
    }

    public void SetInput(Vector2 dest){
        input = dest;
    }

    public Vector2 GetInput(){
        return input;
    }
    public void ResetJumpCount(){
        currentJumps=1;
    }

    public float GetJumpCount(){
        return currentJumps;
    }

    public void BlockFlip(bool state){
        flipBlock = state;
    }

    public void BlockMove(bool state){
        moveBlock = state;
    }

    public void BlockJump(bool state){
        jumpBlock = state;
    }

    public void BlockAll(bool state){
        flipBlock = state;
        moveBlock = state;
        jumpBlock = state;
    }

    public void RestoreGravity(){
        playerRigidbody.gravityScale = gravityOriginal;
    }
    public void SetGravity(bool rewrite, float gravityMultiplier){
        if(rewrite) playerRigidbody.gravityScale = gravityMultiplier;
        else playerRigidbody.gravityScale *= gravityMultiplier;
    }

    public bool IsJumpOrFall(){
        if(isFalling || isJumping) return true;
        else return false;
    }

    private void CalculateVelocity(){
        targetVelocity = new Vector2(input.x*movementMultiplier.x, playerRigidbody.velocity.y);
        if(moveBlock) targetVelocity.x=0;
    }

    void Move(){
        playerRigidbody.velocity = Vector2.SmoothDamp(playerRigidbody.velocity,targetVelocity, ref resultVelocity, MovementSmoothing);
    }

    void AdditionalMove(){
        ClampVerticalSpeed();
        foreach(Force force in forces){
            playerRigidbody.velocity += force.force;
            if(!force.constant) force.force = Vector2.Lerp(force.force, Vector2.zero, force.xLerp);
        }
        ForceUpdate();
    }

    void ClampVerticalSpeed(){
        if(playerRigidbody.velocity.y>movementMultiplier.y){
            Vector2 clampedVelocity = playerRigidbody.velocity;
            clampedVelocity.x=0;
            clampedVelocity = Vector2.ClampMagnitude(clampedVelocity,movementMultiplier.y);
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x,clampedVelocity.y);
        }
        else if(playerRigidbody.velocity.y<movementMultiplier.z){
            Vector2 clampedVelocity = playerRigidbody.velocity;
            clampedVelocity.x=0;
            clampedVelocity = Vector2.ClampMagnitude(clampedVelocity,movementMultiplier.z);
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x,clampedVelocity.y);
        }
    }

    void ForceUpdate(){
        for(int i = forces.Count-1; i>=0;i--){
            if(forces[i].constant){
                //do nothing
            }
            else{
                if(forces[i].force.magnitude<=0.1f){
                    forces.RemoveAt(i);
                }
            } 
        }
    }
    
    void Jump(){
        if(jumpBlock) return;
        if(jumpButton && currentJumps<2){
            Player_Sounds.sounds.PlaySound("jump");
            isJumping = true;
            isFalling = false;
            currentJumps++;
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
            playerRigidbody.AddForce(Vector3.up * movementMultiplier.y, ForceMode2D.Impulse);
            jumpButton = false;
        }
    }

    public void setJumpButton(bool state){
        jumpButton = state;
    }
    public bool GetJumpButton(){
        return jumpButton;
    }

    public void SetImpulseForce(Vector2 vector, float xLerp){
        Force force = new Force(vector,xLerp);
        forces.Add(force);
    }

    public void SetConstantForce(string nameForce, Vector2 vector){
        Force force = new Force(nameForce,vector);
        forces.Add(force);
    }

    public void RemoveForce(string name){
        Force f = forces.Find(x=> x.ID == name);
        f.constant = false;
        f.force = Vector2.zero;
    }

    public void SetMultiplierMovement(Vector3 multiplier){
        //x - режет множитель скорости по горизонтали
        //y - определяет максимальную величину по вертикали для прыжка
        //z - определяет максимальную величину по вертикали для падения
        movementMultiplier.x = speed * multiplier.x;
        movementMultiplier.y = jumpForce * multiplier.y;
        movementMultiplier.z = jumpForce * multiplier.z;
    }

    public void ResetMultiplier(){
        movementMultiplier.x = speed;
        movementMultiplier.y = jumpForce;
        movementMultiplier.z = jumpForce;
    }

    void StepDust(){
        if(Mathf.Abs(input.x)!=0 && isGrounded && !player_Health.dead){
            stepDustEmission.enabled = true;
            if(!stepsSound.isPlaying)stepsSound.Play();
        } 
        else{
            stepDustEmission.enabled = false;
            stepsSound.Stop();
        } 
    }
}

public class Force{
    public Vector2 force;
    public float xLerp;
    public string ID = "";
    public bool constant = false;

    public Force(Vector2 vector, float lerp){
        force = vector;
        xLerp = lerp;
        ID = "Temporary";
        constant = false;
    }

    public Force(string id,Vector2 vector){
        constant = true;
        ID = id;
        force = vector;
    }
}
