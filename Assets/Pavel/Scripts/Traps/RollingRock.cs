
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRock : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 2f;
    [SerializeField] bool isTriggered = true;
    [SerializeField] bool finite = false;
    [SerializeField] float finiteTime = 5f;
    [SerializeField] int dir =1;
    [SerializeField] LayerMask ground;
    Rigidbody2D rb;
    CircleCollider2D circleCollider2D;
    bool onGround = false;
    float radius;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        if(isTriggered){
            circleCollider2D.isTrigger = true;
            gameObject.layer = 12;
        }
        else{
            circleCollider2D.isTrigger = false;
            gameObject.layer = 11;
        }
        radius = circleCollider2D.radius * transform.localScale.x;
        if(finite){
            StartCoroutine(delayFade());
        }
    }

    IEnumerator delayFade(){
        yield return new WaitForSeconds(finiteTime);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        checkGround();
        checkWall();
        if(onGround){
            rb.velocity = new Vector2(dir*speed,rb.velocity.y);
        }
        else{
            rb.velocity = new Vector2(0,rb.velocity.y);
        }
    }

    void checkGround(){
        RaycastHit2D hit1,hit2;
        Vector2 rayOrigin1 = new Vector2(transform.position.x+radius, transform.position.y);
        Vector2 rayOrigin2 = new Vector2(transform.position.x-radius, transform.position.y);
        hit1 = Physics2D.Raycast(rayOrigin1,Vector2.down,radius+0.04f,ground);
        hit2 = Physics2D.Raycast(rayOrigin2,Vector2.down,radius+0.04f,ground);
        Debug.DrawRay(rayOrigin1,Vector2.down*(radius+0.04f),Color.green,0.01f);
        Debug.DrawRay(rayOrigin2,Vector2.down*(radius+0.04f),Color.green,0.01f);
        if(hit1.collider!=null || hit2.collider!=null){
            onGround = true;
        }
        else{
            onGround = false;
        }
    }

    void checkWall(){
        RaycastHit2D hit2D;
        Vector2 rayOrigin = new Vector2(transform.position.x+radius*dir, transform.position.y);
        hit2D = Physics2D.Raycast(rayOrigin,Vector2.right*dir,0.15f,ground);
        Debug.DrawRay(rayOrigin,Vector2.right*dir*0.15f,Color.red,0.01f);
        if(hit2D.collider!=null){
            dir*=-1;
        }
    }

    public CircleCollider2D getCollider(){
        return circleCollider2D;
    }
}
