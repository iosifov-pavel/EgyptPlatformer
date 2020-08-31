using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float speed = 4f;
    public float jump_force = 90f;
    private bool isGrounded=false;
    private float groundRadius = 0.1f;
    public LayerMask ground;
    public Transform groundcheck;
    Rigidbody2D body;
    Transform trans;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    /*void Update()
    {

    }*/

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move() 
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        /* Vector3 movement = new Vector3(moveHorizontal, 0f, 0f );
         trans.Translate(movement*speed*Time.deltaTime);*/

        body.velocity = new Vector2(moveHorizontal*speed,body.velocity.y);
        anim.SetFloat("Speed",Mathf.Abs(moveHorizontal));

       

        if(moveHorizontal > 0 && trans.localScale.x < 0) 
        {
            Vector3 thisScale = trans.localScale;
            thisScale.x *= -1;
            trans.localScale = thisScale;
        }
        else if(moveHorizontal < 0 && trans.localScale.x > 0)
        {
            Vector3 thisScale = trans.localScale;
            thisScale.x *= -1;
            trans.localScale = thisScale;
        }
    }
    void Jump() 
    {
        isGrounded = Physics2D.OverlapCircle(groundcheck.position, groundRadius, ground);
        anim.SetBool("Ground",isGrounded);
        anim.SetFloat("vSpeed",body.velocity.y);

        if(!isGrounded) return;

        if (Input.GetKey(KeyCode.Space)&&isGrounded)
            {           
                anim.SetBool("Ground",false);
                body.AddForce(Vector3.up * jump_force* Time.deltaTime, ForceMode2D.Impulse);
            }
    }
    }

