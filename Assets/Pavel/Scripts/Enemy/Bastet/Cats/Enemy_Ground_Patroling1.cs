using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ground_Patroling1 : MonoBehaviour
{
    [SerializeField] GameObject legs;
    Animator legs_anim;
    [SerializeField] int dir = 1;
    [SerializeField] public float speed = 2f;
    Vector2 checkground;
    Vector2 checkwall;
    float width;
    float height;
    LayerMask mask;
    BoxCollider2D box;
    bool stop=false;
    // Start is called before the first frame update
    void Start(){
        //legs_anim = legs.GetComponent<Animator>();
        LayerMask m1 = LayerMask.GetMask("Ground");
        LayerMask m2 = LayerMask.GetMask("Traps");
        mask = m1 | m2;
        transform.localScale = new Vector3 (transform.localScale.x*dir, transform.localScale.y, transform.localScale.z);
        box = GetComponent<BoxCollider2D>();
        width = box.size.x / 2 ; 
        height = box.size.y / 2 ;
    }

    // Update is called once per frame
    void Update(){
        Check();
        //legs_anim.SetBool("Walk", !stop);
        if(!stop) Move();
        
    }

    void Check(){
       RaycastHit2D ground = CheckGround();
       RaycastHit2D wall = CheckWall();
       if(ground.collider==null || (wall.collider!=null && (wall.collider.gameObject.tag=="Ground" || wall.collider.gameObject.tag=="Trap"))){
           changeDirection();
       }
    }

    void Move(){
        transform.Translate(new Vector3(dir*speed*Time.deltaTime,0,0),Space.World);
    }

    void changeDirection(){
        if(dir==1){
            Vector3 thisScale = transform.localScale;
            thisScale.x *= -1;
            transform.localScale = thisScale;
            dir=-1;
        } else {
            Vector3 thisScale = transform.localScale;
            thisScale.x *= -1;
            transform.localScale = thisScale;
            dir=1;
        }
    }

    RaycastHit2D CheckGround(){    
        checkground =new Vector2(transform.position.x + width*dir, transform.position.y);
        checkground.x+=0.05f*dir;
        RaycastHit2D hit;
        hit =  Physics2D.Raycast(checkground,Vector3.down, height*transform.localScale.y+0.2f,mask);
        Debug.DrawRay(checkground,Vector3.down*(height*transform.localScale.y+0.2f),Color.red,0.02f);
        return hit;   
    }

    RaycastHit2D CheckWall(){   
        checkwall = new Vector2(transform.position.x + width*dir, transform.position.y);
        checkwall.x+=0.05f*dir;
        RaycastHit2D hit;
        hit =  Physics2D.Raycast(checkwall,new Vector2(dir,0), 0.1f,mask);
        Debug.DrawRay(checkwall,new Vector2(dir,0)*0.1f,Color.yellow,0.02f);
        return hit;   
    }

    public void StopIt(float time){
        StartCoroutine(stops(time));
    }

    IEnumerator stops(float t){
        stop=true;
        yield return new WaitForSeconds(t);
        stop = false;
    }

    public void CanGo(){
        StopAllCoroutines();
        stop=false;
    }
}
