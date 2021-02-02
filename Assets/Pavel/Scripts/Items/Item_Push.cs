using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Push : MonoBehaviour, IIntercatable
{
    // Start is called before the first frame update
    GameObject player;
    Player_Movement pm;
    Player_Health ph;
    float speed = 18f;
    bool on = false;
    Rigidbody2D rb2;
    Rigidbody2D  playerrb;
    float distance;
    Vector2 player_pos;
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        //rb2.isKinematic = true;
        rb2.mass = 200f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckContact();
        //if(on){
        //    pm.blocked=true;
        //    Vector2 diff = transform.position-player.transform.position;
        //    float dif = Mathf.Abs(diff.magnitude);
        //    if(!pm.isGrounded || ph.isDamaged || ph.dead || dif>distance+0.06f){
        //        player.transform.parent=null;
        //        rb2.velocity=Vector2.zero;
        //        pm.blocked = false;
        //        player=null;
        //        on = false;
        //        pm=null;
        //        return;
        //    }
        //    float step =(pm.direction.x * speed * Time.deltaTime);
        //    rb2.velocity=new Vector2(step,0);
        //    player.transform.localPosition = player_pos;
        //}
    }

    void CheckContact(){
        //if(rb2.velocity.magnitude>0.01f) rb2.isKinematic=false;
        //else rb2.isKinematic=true;
    }

    public void Use(GameObject _player){
        on = on==true ? false : true;
        if(on){
            rb2.mass = 10;
            //player=_player;
            //player.transform.parent=transform;
            //player_pos =new Vector2(player.transform.localPosition.x,0);
            //pm=player.GetComponent<Player_Movement>();
            //playerrb = player.GetComponent<Rigidbody2D>();
            //ph = player.GetComponent<Player_Health>();
            //Vector2 dist = transform.position-player.transform.position;
            //distance = Mathf.Abs(dist.magnitude);
            //pm.blocked = true;
        } else{

            rb2.mass = 200f;
            //player.transform.parent=null;
            //rb2.velocity=Vector2.zero;
            //rb2.isKinematic=true;
            //pm.blocked = false;
            //player=null;
            //pm=null;
        }
    }
}
