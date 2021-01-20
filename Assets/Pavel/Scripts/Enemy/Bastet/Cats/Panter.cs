using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panter : MonoBehaviour
{
    Enemy_Ray_Eyes eyes;
    Enemy_Ground_Patroling1 egp;
    Rigidbody2D rb;
    BoxCollider2D box;
    float toFloor;
    LayerMask ground;
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
        box = GetComponent<BoxCollider2D>();
        toFloor = box.bounds.extents.y;
        ground = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        dir = (int)Mathf.Sign(transform.localScale.x) * 1;
        if(is_jumping){
            bool on_ground=false;
            float dist = toFloor + 0.05f;
            RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.down, dist, ground);
            Debug.DrawRay(transform.position,Vector2.down*dist,Color.green,0.01f);
            if(hit.collider!=null){
                on_ground=true;
            }
            if(on_ground){
                egp.enabled=true;
                rb.mass=1;
                rb.bodyType= RigidbodyType2D.Kinematic;
                rb.velocity = Vector2.zero;
                StartCoroutine(atackDelay());
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
        rb.velocity = Vector2.right*dir*egp.speed;
        rb.AddForce(Vector2.up*jump_f, ForceMode2D.Impulse);
        can_attack=false;
        is_jumping=true;
    }

    IEnumerator atackDelay(){
        yield return new WaitForSeconds(delay_attack);
        can_attack=true;
    }
}
