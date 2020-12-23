using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panter : MonoBehaviour
{
    Enemy_Ray_Eyes eyes;
    Enemy_Ground_Patroling1 egp;
    Rigidbody2D rb;
    float distance;
    int dir = 1;
    Transform player;
    float active_speed;
    bool can_attack = true, is_jumping=false;
    float delay_attack = 0.5f;
    [SerializeField] float jump_f=3;
    // Start is called before the first frame update
    void Start()
    {
        eyes = GetComponent<Enemy_Ray_Eyes>();
        egp = GetComponent<Enemy_Ground_Patroling1>();
        rb = GetComponent<Rigidbody2D>();
        active_speed = egp.speed * 2;
    }

    // Update is called once per frame
    void Update()
    {
        dir = (int)Mathf.Sign(transform.localScale.x) * 1;
        if(is_jumping){
            ContactPoint2D[] cp = new ContactPoint2D[4];
            bool on_ground=false;
            rb.GetContacts(cp);
            foreach(ContactPoint2D c in cp){
                if(c.collider!=null && c.collider.gameObject.tag=="Ground"){
                    on_ground = true;
                    break;
                }
            }
            if(on_ground){
                egp.enabled=true;
                rb.mass=1;
                rb.bodyType= RigidbodyType2D.Kinematic;
                rb.velocity = Vector2.zero;
                can_attack=true;
                is_jumping=false;
            }
        }
        if(eyes.Check()!=null){
            player = eyes.Check();
            distance=Mathf.Abs(transform.position.x-player.position.x);
        }
        else{
            egp.speed = active_speed/2;
            return;
        } 
        if(!can_attack) return;
        egp.speed= active_speed;
        if(distance<=4){
            Jump();
        }

    }

    void Jump(){
        egp.enabled = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.mass = 1.2f;
        rb.velocity = Vector2.right*dir*egp.speed*1.5f;
        rb.AddForce(Vector2.up*jump_f, ForceMode2D.Impulse);
        can_attack=false;
        is_jumping=true;
    }
}
