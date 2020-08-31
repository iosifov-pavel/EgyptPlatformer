using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public float speed = 4f;
    public float jump_force = 70f;
    private bool _isGrounded;
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
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveHorizontal, 0f, 0f );

        anim.SetFloat("Speed",Mathf.Abs(moveHorizontal));

        trans.Translate(movement*speed*Time.deltaTime);

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
        if (Input.GetKey(KeyCode.W))
            {           
                body.AddForce(Vector3.up * jump_force);
            }
    }
    }

