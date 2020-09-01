using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private float speed = 140f;
    private float jump_force = 320f;
    private bool isGrounded=true;
   // private float groundRadius = 0.215f;
    public LayerMask ground;
   // public Transform groundcheck;
    Rigidbody2D body;
    Transform trans;
    Animator anim;
    BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    
    void OnDrawGizmos()
    {

    }
  
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Move();
        CheckGround();
        Jump();
    }

    void Move() 
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal")* speed * Time.deltaTime;

        Vector2 movement = new Vector2(moveHorizontal, body.velocity.y);

        body.velocity = movement;
        anim.SetFloat("Speed",Mathf.Abs(moveHorizontal));

       

        if(moveHorizontal > 0 && trans.localScale.x < 0) 
        {
            Flip();
        }
        else if(moveHorizontal < 0 && trans.localScale.x > 0)
        {
            Flip();
        }
    }
    void Jump() 
    {
       // isGrounded = Physics2D.OverlapCircle(groundcheck.position, groundRadius, ground);
        anim.SetBool("Ground",isGrounded);
        anim.SetFloat("vSpeed",body.velocity.y);

        if(!isGrounded) return;

        if (Input.GetKey(KeyCode.Space)&&isGrounded)
            {           
                anim.SetBool("Ground",false);
                body.velocity = new Vector2(body.velocity.x, 0);
                body.AddForce(Vector3.up * jump_force * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
    }

    void Flip()
    {
        Vector3 thisScale = trans.localScale;
        thisScale.x *= -1;
        trans.localScale = thisScale;
    }

    void CheckGround() 
    {
        Bounds bnds = box.bounds;
        Vector2 max = new Vector2(bnds.max.x-0.03f, bnds.min.y-0.03f);
        Vector2 min = new Vector2(bnds.min.x+0.03f, bnds.min.y-0.07f);
        Collider2D hit = Physics2D.OverlapArea(min,max);
        /*Debug.DrawLine(new Vector3(max.x, max.y, 0), new Vector3(max.x, min.y, 0), Color.red);
        Debug.DrawLine(new Vector3(max.x, max.y, 0), new Vector3(min.x, max.y, 0), Color.red);
        Debug.DrawLine(new Vector3(min.x, min.y, 0), new Vector3(min.x, max.y, 0), Color.red);
        Debug.DrawLine(new Vector3(min.x, min.y, 0), new Vector3(max.x, min.y, 0), Color.red);*/
        if(hit != null)
        {
            isGrounded=true;
        }
        else isGrounded = false;
    }
}

