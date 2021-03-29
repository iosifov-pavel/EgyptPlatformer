using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundWalking : MonoBehaviour
{
    [SerializeField] Transform start, end, body;
    [SerializeField] bool vertical = false;
    [SerializeField] float speed = 2;
    [SerializeField] float wallWaitTime = 1f;
    [SerializeField] bool jumping = false;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float jumpPeriodTime = 1f;
    [SerializeField] float gravityScale = 1;
    [SerializeField] Vector2 gravityDirection = Vector2.down;
    [SerializeField] float checkGroundDistance = 0.22f;
    [SerializeField] LayerMask whatIsGround;
    float timer=0;
    float resultSpeed;
    float jumpTimer=0;
    float startPoint, endPoint;
    int dir = 1;
    //float gravity = -9.81f;
    Rigidbody2D rb;
    Vector2 move;
    Vector3 oldPosition;
    bool onGround=false;
    bool directionChanged = false;
    public bool fromOutsideScriptJump=false;
    [SerializeField] bool forward = true;
    // Start is called before the first frame update
    private void OnDrawGizmos() {
        if(body==null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(start.position,end.position);
        Gizmos.DrawLine(body.position, body.position+body.transform.up*(-checkGroundDistance));
    }
    void Start()
    {
        resultSpeed = speed;
        if(vertical){
            startPoint = start.position.y;
            endPoint = end.position.y;
        }
        else{
            startPoint = start.position.x;
            endPoint = end.position.x;
        }
        rb = body.GetComponent<Rigidbody2D>();
        oldPosition = body.position;
    }

    private void Update() {
        try{
        CheckGround();
        }
        catch{
            if(body==null) Destroy(gameObject);
        }
    }

    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(body.position,-body.up,checkGroundDistance,whatIsGround);
        if(hit.collider!=null) onGround = true;
        else onGround = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        try{
            Check();
            Move();
            if(jumping || fromOutsideScriptJump) Jump();
            CheckWalls();
        }
        catch{
            if(body==null) Destroy(gameObject);
        }
    }

    private void CheckWalls()
    {
        if(vertical){
            if(Mathf.Abs(body.position.y-oldPosition.y)<=Mathf.Epsilon){
                timer+=Time.deltaTime;
                if(timer>=wallWaitTime){
                    forward=!forward;
                    dir*=-1;
                    Flip();
                    timer=0;
                }
            }  
            else timer=0;          
        }
        else{
            if(Mathf.Abs(body.position.x-oldPosition.x)<=Mathf.Epsilon){
                timer+=Time.deltaTime;
                if(timer>=wallWaitTime){
                    forward=!forward;
                    dir*=-1;
                    Flip();
                    timer=0;
                }
            }
            else timer=0;
        }
        oldPosition = body.position;
    }

    void Flip(){
        StartCoroutine(flipdir());
        if(dir==1){
            Vector3 newScale = body.localScale;
            newScale.x = Mathf.Abs(newScale.x);
            body.localScale = newScale;
        }
        else{
            Vector3 newScale = body.localScale;
            newScale.x = -Mathf.Abs(newScale.x);
            body.localScale = newScale;
        }
    }
    IEnumerator flipdir(){
        directionChanged = true;
        yield return new WaitForSeconds(0.025f);
        directionChanged = false;
    }

    private void Move()
    {
        if(vertical) rb.velocity = new Vector2(rb.velocity.x,dir*resultSpeed);
        else rb.velocity = new Vector2(dir*resultSpeed,rb.velocity.y);
        Vector2 gravityV = gravityDirection*gravityScale*(Physics2D.gravity.magnitude)*Time.deltaTime;
        rb.velocity+=gravityV;
    }

    private void Check()
    {
        if(vertical){
            if(body.position.y>=endPoint){
                forward = false;
                dir=-1;
                Flip();
            } 
            else if(body.position.y<=startPoint){
                forward = true;
                dir=1;
                Flip();
            }
        }
        else{
            if(body.position.x>=endPoint){
                forward = false;
                dir=-1;
                Flip();
            } 
            else if(body.position.x<=startPoint){
                forward = true;
                dir=1;
                Flip();
            }
        }

    }

    void Jump(){
        jumpTimer+=Time.deltaTime;
        if((jumpTimer>=jumpPeriodTime && onGround) || (fromOutsideScriptJump && onGround)){
            jumpTimer=0;
                Vector2 newVelo = rb.velocity;
            if(vertical){
                newVelo.x=0;
                rb.velocity = newVelo;
            }
            else{
                newVelo.y=0;
                rb.velocity = newVelo;
            }
            rb.AddForce(-gravityDirection*jumpForce, ForceMode2D.Impulse);
            fromOutsideScriptJump = false;
        }
    }


    public void StopWalk(){
        resultSpeed = 0;
    }

    public void WalkAgain(){
        resultSpeed = speed;
    }

    public void SpeedMultiplier(int m){
        resultSpeed = speed * m;
    }

    public bool ChangeDirection(){
        return directionChanged;
    }
}
