using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Jump : MonoBehaviour
{
    //[SerializeField] float jumpHeight = 4f;
    //[SerializeField] float jumpSpeed = 6f;
    [SerializeField] float jumpDelay = 1f;
    [SerializeField] float angle = 45;
    Enemy_Ground_Patroling1 enemy_Ground_Patroling1;
    Transform point_to_jump;
    Rigidbody2D rb;
    int dir = 1;
    public bool isJumping = false, canJump = true;
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
        SeekForJump();
        if(isJumping){
           CheckGround(); 
           CheckWall();
        }
    }

    void SeekForJump(){
        if(Input.GetButtonDown("Jump") && canJump){
            Jump();
        } 
    }


    void Jump(){
        rb.bodyType = RigidbodyType2D.Dynamic;
        float force = HardLines.GetForce(transform, point_to_jump, angle);
        point_to_jump.Rotate(new Vector3(0,0,angle));
        Vector2 move = point_to_jump.right;
        point_to_jump.Rotate(new Vector3(0,0,-angle));
        rb.AddForce(move*force,ForceMode2D.Impulse);
        isJumping = true;
        canJump = false;
    }

    void CheckGround(){
        RaycastHit2D ground;
        Vector2 rayOrigin = (Vector2)transform.position - new Vector2(0,halfHeight);
        ground = Physics2D.Raycast(rayOrigin,Vector2.down,0.05f,floor);
        Debug.DrawLine(rayOrigin,rayOrigin - new Vector2(0,0.05f));
        if(ground.collider!=null && rb.velocity.y<=0){
            isJumping=false;
            rb.bodyType = RigidbodyType2D.Kinematic;
            StartCoroutine(jumpDelaying());
        }
    }

    IEnumerator jumpDelaying(){
        yield return new WaitForSeconds(jumpDelay);
        canJump = true;
    }

    void CheckWall(){

    }
}
