using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    private float speed = 8f;
    public bool isDamaged = false;
    private float jump_force = 320f;
    public bool isGrounded=true;
    private bool buttonPressed = false;
    private float other_source = 0;
    public float forces = 0;
   // private float groundRadius = 0.215f;
    public LayerMask ground;
    
   // public Transform groundcheck;
    Rigidbody2D body;
    Transform trans;
    Animator anim;
    BoxCollider2D box;
    PolygonCollider2D polygon;
    Collider2D hit;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        //body.drag=23f;
        //body.
        trans = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        polygon = GetComponent<PolygonCollider2D>();
    }

    
    void OnDrawGizmos()
    {
        
    }
  
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) buttonPressed=true;
        if(Input.GetKeyUp(KeyCode.Space)) buttonPressed=false;
        CheckGround();
    }

    void FixedUpdate()
    {
        Move();
        
        Jump();
    }

    void Move() 
    {
        if(isDamaged) return;
        // Передвижение через velocity
        
        float moveHorizontal = (Input.GetAxis("Horizontal")+forces)* (speed+other_source) * Time.fixedDeltaTime;
        float aceleration = moveHorizontal+body.velocity.x;
        if(aceleration>3.3f) aceleration=3.3f;
        Vector2 movement = new Vector2(aceleration, body.velocity.y);
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
        
        // Передвижение через AddForce
        
       /* float dir = Input.GetAxisRaw("Horizontal");
        if(dir==0&&isGrounded) body.velocity=new Vector2(0,body.velocity.y);
        Vector2 moveHorizontal = new Vector2(dir*(speed+forces)*Time.fixedDeltaTime , 0+other_source);
        body.AddForce(moveHorizontal,ForceMode2D.Force);
        anim.SetFloat("Speed",Mathf.Abs(moveHorizontal.x));

        if(moveHorizontal.x > 0 && trans.localScale.x < 0) 
        {
            Flip();
        }
        else if(moveHorizontal.x < 0 && trans.localScale.x > 0)
        {
            Flip();
        }
        */

        
    }
    void Jump() 
    {
       // CheckGround();
        anim.SetBool("Ground",isGrounded);
        anim.SetFloat("vSpeed",body.velocity.y);

        if(!isGrounded) return;
        if(hit!=null && hit.gameObject.tag=="Obstacle" && hit.gameObject.GetComponent<CanHurtYou>().contactYes==false) return;
        if (buttonPressed&&isGrounded)
            {      
                buttonPressed = false;    
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
        hit = CheckBox();
        
        if(hit != null /*&& hit.gameObject.tag!="Obstacle"*/)
        {
            
            isGrounded=true;
        }
        else isGrounded = false;
    }

    void checkPlatforms()
    {
        RaycastHit2D plat = CheckRay();
        float y =1f;
        y+=1;
    }
    public Collider2D CheckBox()
    {
        Bounds bnds = polygon.bounds;
        Vector2 max = new Vector2(bnds.max.x - 0.02f, bnds.min.y - 0.021f);
        Vector2 min = new Vector2(bnds.min.x + 0.02f, bnds.min.y - 0.031f);
        Collider2D hit = Physics2D.OverlapArea(min, max);

        Debug.DrawLine(new Vector3(max.x, max.y, 0), new Vector3(max.x, min.y, 0), Color.red);
        Debug.DrawLine(new Vector3(max.x, max.y, 0), new Vector3(min.x, max.y, 0), Color.red);
        Debug.DrawLine(new Vector3(min.x, min.y, 0), new Vector3(min.x, max.y, 0), Color.red);
        Debug.DrawLine(new Vector3(min.x, min.y, 0), new Vector3(max.x, min.y, 0), Color.red);

        return hit;
    }

    RaycastHit2D CheckRay()
    {    
        Vector3 check = new Vector3(transform.position.x,transform.position.y-0.35f,transform.position.z);
        Vector3 checkTo = Vector3.down * 0.05f;

        Debug.DrawRay(check,checkTo,Color.yellow);

        RaycastHit2D hit;
        hit =  Physics2D.Raycast(check,Vector2.down, 0.05f);
        return hit;   
    }

    
}

