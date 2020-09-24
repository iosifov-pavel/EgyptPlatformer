using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    [Header("Horizontal Movement")]
    private float speed = 18f;
    private float maxSpeed = 3.3f;
    private Vector2 direction;
    public bool isDamaged = false;
    private float jump_force = 380f;
    public bool isGrounded = true;
    private bool buttonPressed = false;
    private float fallmultiplier = 3f;
    public float gravitys = 2f;
    private float customdrag = 15f;
    private bool jump = false;
   // private float groundRadius = 0.215f;
    public LayerMask ground;
    
    [Header("Components")]
   // public Transform groundcheck;
    Rigidbody2D body;
    Transform trans;
    Animator anim;
    PolygonCollider2D polygon;
    Collider2D hit;
    Collider2D lasthit;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.drag=1f;
        trans = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        polygon = GetComponent<PolygonCollider2D>();
    }

    
    void OnDrawGizmos()
    {
        
    }
  
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        CheckGround();
        
        if(!isGrounded) {}
        else 
        {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            buttonPressed=true;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        { 
            buttonPressed=false;
        }
        }
        
    }

    void FixedUpdate()
    {
        Move();   
        Jump();
        CustomPhysics();
    }

    void Move() 
    {
        if(isDamaged) return;

        body.AddForce(new Vector2(direction.x*speed*Time.fixedDeltaTime, 0), ForceMode2D.Impulse);
        anim.SetFloat("Speed",Mathf.Abs(direction.x));

        if (Mathf.Abs(body.velocity.x) > maxSpeed) {
            body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * maxSpeed, body.velocity.y);
        }

        if((direction.x > 0 && trans.localScale.x < 0)||(direction.x < 0 && trans.localScale.x > 0))
        {
            Flip();
        }
    }

    void CustomPhysics()
    {
        bool directionchanged = (direction.x > 0 && body.velocity.x < 0) || (direction.x < 0 && body.velocity.x > 0);

        if(isGrounded)
        {
            bool oneway = false;
            PlatformEffector2D pe2d=null;
            if(hit!=null)
            pe2d = hit.gameObject.GetComponent<PlatformEffector2D>();
            if(pe2d!=null)
            oneway = pe2d.useOneWay;
            if((direction.x==0 || directionchanged) && !jump && !oneway)
            {
                body.drag = customdrag;
            }
            else
            {
                body.drag=1f;
            }
            body.gravityScale = gravitys;
        }
        else
        {
            body.drag = customdrag*0.1f;
            if(body.velocity.y<0)
            {
                body.gravityScale=gravitys;
            }
            else if(body.velocity.y>0 && lasthit.gameObject!=null && lasthit.gameObject.tag!="CantJump" && !Input.GetButton("Jump"))
            {
                body.gravityScale =  gravitys * fallmultiplier;
            }
        }

    }

    void Jump() 
    {
        anim.SetBool("Ground",isGrounded);
        anim.SetFloat("vSpeed",body.velocity.y);

        if(!isGrounded) return;
        if(hit!=null && hit.gameObject.tag=="Obstacle" && hit.gameObject.GetComponent<CanHurtYou>().contactYes==false) return;
        if(buttonPressed&&isGrounded)
        {      
            body.drag=1f;
            jump = true;
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
    
        if(hit != null)
        {
            lasthit = hit;
            jump=false;
            isGrounded=true;
        }
        else isGrounded = false;
        if(hit != null && hit.gameObject.tag=="CantJump")
        {
            isGrounded=false;
            return;
        }
        
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

