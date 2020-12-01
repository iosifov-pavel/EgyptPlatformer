using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider_Attack : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] SpriteRenderer web;
    LineRenderer web;
    BoxCollider2D box;
    float offset;
    Vector2 point_for_ground;
    Vector3 web_point;
    Vector2 ray,ray2,ray_ground;
    float speed_up=2f;
    float speed_down=3f;
    float time = 0.5f;
    RaycastHit2D hit,hit2,ground_hit;
    Vector3 original;
    LayerMask player;
    LayerMask player2;
    LayerMask p, ground;
    bool down=false,up=false,isDown=false, isUp=true;
    void Start()
    {
        ray=Vector2.down;
        ray2=Vector2.down;
        player = LayerMask.GetMask("Player");
        player2 = LayerMask.GetMask("Damaged");
        ground = LayerMask.GetMask("Ground");
        p = player | player2;
        original=transform.position;
        web_point = original + new Vector3(0,0.5f,0);
        web = GetComponent<LineRenderer>();
        web.positionCount = 2;
        web.SetPosition(0, web_point);
        web.SetPosition(1,original);
        box = GetComponent<BoxCollider2D>();
        offset = box.size.y*transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        Spider_Sense();
        if(down) Jump();
        if(up) Back();   
        DrawWeb();
    }

    void Spider_Sense(){
        if(isUp){
            Vector2 pos,pos2;
            pos = (Vector2)transform.position + new Vector2(0.1f,0);
            pos2 = (Vector2)transform.position - new Vector2(0.1f,0);

            hit = Physics2D.Raycast(pos,ray,2f,p);
            hit2 = Physics2D.Raycast(pos2,ray2,2f,p);
            if(hit.collider!=null || hit2.collider!=null){
                down=true;
                up=false;
                isUp=false;
                isDown=false;
            }
        }
        point_for_ground = (Vector2)transform.position - new Vector2(0,offset);
        ground_hit = Physics2D.Raycast(point_for_ground,ray,0.05f,ground);

        if(ground_hit.collider!=null){
           if(down && !isDown) StartCoroutine(Delay());
        }
    }

    void DrawWeb(){
        web.SetPosition(1,transform.position);
    }

    void Jump(){
        float step = speed_down*Time.deltaTime;
        transform.Translate(Vector3.down*step);
    }

    void Back(){
        if(transform.position.y>=original.y){
            isUp=true;
            down = false;
            up=false;
            isDown=false;
            //Flip();
        }
        float step = speed_up*Time.deltaTime;
        transform.Translate(Vector3.up*step);
    }

    IEnumerator Delay(){
        isDown=true;
        down=false;
        yield return new WaitForSeconds(time);
        up = true;
        isDown = false;
        //Flip();
    }

    void Flip(){
        Vector3 thisScale = transform.localScale;
        thisScale.y *= -1;
        transform.localScale = thisScale;
    }
}
