using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Jump : MonoBehaviour
{
    //[SerializeField] float jumpHeight = 4f;
    //[SerializeField] float jumpSpeed = 6f;
    [SerializeField] float jumpDelay = 1f;
    [SerializeField] float angle = 45;
    [SerializeField] bool checkingWalls = false;
    Enemy_Ground_Patroling1 enemy_Ground_Patroling1;
    Transform point_to_jump;
    Rigidbody2D rb;
    public int dir = 1;
    public bool isJumping = false, canJump = true, placeIsGood=false, hitWall = false,needToCheckGround=false;
    float halfHeight =0, halfWidth = 0;
    LayerMask floor;
    // Start is called before the first frame update
    void Start()
    {
        floor = LayerMask.GetMask("Ground");
        point_to_jump = transform.GetChild(0);
        rb = GetComponent<Rigidbody2D>();
        enemy_Ground_Patroling1 = GetComponent<Enemy_Ground_Patroling1>();
        halfHeight = GetComponent<BoxCollider2D>().bounds.extents.y;
        halfWidth = GetComponent<BoxCollider2D>().bounds.extents.x;
    }

    // Update is called once per frame
    void Update()
    {
        dir = (int)Mathf.Sign(transform.localScale.x) * 1;
        if(checkingWalls)SeekForJump();
        if(canJump) Jump();
        if(isJumping){
           if(needToCheckGround)CheckGround(); 
           CheckWall();
        }
    }

    void SeekForJump(){
        canJump=false;
    }


    void Jump(){
        rb.bodyType = RigidbodyType2D.Dynamic;
        float force = HardLines.GetForce(transform, point_to_jump, angle);
        point_to_jump.Rotate(new Vector3(0,0,angle * dir));
        Vector2 move = point_to_jump.right;
        move = new Vector2 (move.x * dir, move.y);
        point_to_jump.Rotate(new Vector3(0,0,-angle * dir));
        rb.AddForce(move*force,ForceMode2D.Impulse);
        isJumping = true;
        canJump = false;
        StartCoroutine(checkTheGround());
    }

    void CheckGround(){
        RaycastHit2D g1,g2;
        Vector2 rayOrigin1 = (Vector2)transform.position + new Vector2(halfWidth-0.05f,-halfHeight);
        Vector2 rayOrigin2 = (Vector2)transform.position + new Vector2(-halfWidth+0.05f,-halfHeight);
        g1 = Physics2D.Raycast(rayOrigin1,Vector2.down,0.05f,floor);
        g2 = Physics2D.Raycast(rayOrigin2,Vector2.down,0.05f,floor);
        Debug.DrawLine(rayOrigin1,rayOrigin1 - new Vector2(0,0.05f));
        Debug.DrawLine(rayOrigin2,rayOrigin2 - new Vector2(0,0.05f));
        if(g1.collider!=null || g2.collider!=null && rb.velocity.y<=0f){
            isJumping=false;
            needToCheckGround = false;
            hitWall=false;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
            StartCoroutine(jumpDelaying());
        }
    }

    IEnumerator jumpDelaying(){
        yield return new WaitForSeconds(jumpDelay);
        canJump = true;
    }

    IEnumerator checkTheGround(){
        yield return new WaitForSeconds(0.1f);
        needToCheckGround = true;
    }

    void CheckWall(){
        RaycastHit2D w1,w2;
        Vector2 rayOrigin1 = (Vector2)transform.position + new Vector2(halfWidth,halfHeight-0.15f) * dir;
        Vector2 rayOrigin2 = (Vector2)transform.position + new Vector2(halfWidth,-halfHeight+0.15f) * dir;
        w1 = Physics2D.Raycast(rayOrigin1,Vector2.right * dir,0.1f,floor);
        w2 = Physics2D.Raycast(rayOrigin2,Vector2.right * dir,0.1f,floor);
        Debug.DrawLine(rayOrigin1,rayOrigin1 + new Vector2(0.1f,0) * dir);
        Debug.DrawLine(rayOrigin2,rayOrigin2 + new Vector2(0.1f,0) * dir);
        if(w1.collider!=null || w2.collider!=null && isJumping){
            hitWall = true;
            Vector2 new_vel = rb.velocity;
            new_vel.x*=-1;
            rb.velocity = new_vel;
            Vector2 new_scale_x = transform.localScale;
            new_scale_x.x *=-1;
            transform.localScale = new_scale_x;
        }       
    }
}
