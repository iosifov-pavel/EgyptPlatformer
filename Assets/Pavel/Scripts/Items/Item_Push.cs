using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Push : MonoBehaviour, IIntercatable
{
    // Start is called before the first frame update
    GameObject player;
    Player_Movement player_Movement=null;
    Rigidbody2D plrb;
    Player_Health player_Health=null;
    Player_Attack player_Attack=null;
    float speed = 18f;
    float mov_speed;
    bool on = false;
    Rigidbody2D rb2;
    float ext_x, ext_y;
    BoxCollider2D box;
    public bool onGround = true;
    [SerializeField] LayerMask ground;
    [SerializeField] PhysicsMaterial2D zeroFriction, strongFriction;
    Vector2 offset;
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        ext_x = box.bounds.extents.x * transform.localScale.x;
        ext_y = box.bounds.extents.y * transform.localScale.y;
        rb2 = GetComponent<Rigidbody2D>();
        //rb2.isKinematic = true;
        rb2.mass = 200f;
    }

    // Update is called once per frame
    void Update()
    {
        if(on) rb2.sharedMaterial = strongFriction;
        else rb2.sharedMaterial = zeroFriction;
        if(player_Health!=null && ( player_Health.isDamaged || !player_Movement.isGrounded  || !onGround ) && on){
            Use(player);
        }
        CheckContact();    
        if(on){
            Action();
        }
    }

    private void FixedUpdate() {
    
    }

    void CheckContact(){
        RaycastHit2D hit1,hit2;
        Vector2 rayOrigin1 = new Vector2(transform.position.x+ext_x+0.01f, transform.position.y-ext_y);
        Vector2 rayOrigin2 = new Vector2(transform.position.x-ext_x-0.01f, transform.position.y-ext_y);
        hit1 = Physics2D.Raycast(rayOrigin1,Vector2.down,0.1f,ground);
        hit2 = Physics2D.Raycast(rayOrigin2,Vector2.down,0.1f,ground);
        Debug.DrawRay(rayOrigin1,Vector2.down*(0.1f),Color.green,0.01f);
        Debug.DrawRay(rayOrigin2,Vector2.down*(0.1f),Color.green,0.01f);
        if((hit1.collider!=null || hit2.collider!=null)){
            onGround = true;
            //rb2.mass = 2000f;
            //rb2.bodyType = RigidbodyType2D.Kinematic;
        }
        else{
            onGround = false;
            rb2.velocity = new Vector2(0,rb2.velocity.y);
        }
    }

    void Action(){
        mov_speed = player_Movement.direction.x;
        Vector2 newpos_p = plrb.position + new Vector2(mov_speed*Time.deltaTime,0);
        Vector2 newpos_b = rb2.position + new Vector2(mov_speed*Time.deltaTime,0);
        plrb.MovePosition(newpos_p);
        transform.localPosition = offset;
        //rb2.MovePosition(newpos_b);
    }

    public void Use(GameObject _player){
        if(!on && Player_Interact.player_Interact.isInteracting) return;
        on = !on;
        player = _player;
        if(on){
            //rb2.mass = 10f;
            if(player_Movement==null){
                player_Movement = player.GetComponent<Player_Movement>();
                player_Health = player.GetComponent<Player_Health>();
                player_Attack = player.transform.GetChild(2).gameObject.GetComponent<Player_Attack>();
                plrb = player.GetComponent<Rigidbody2D>();
            }
            player_Movement.direction= Vector2.zero;
            player_Movement.inertia=0;
            player_Movement.moveBlock=true;
            player_Movement.jump_block=true;
            player_Attack.blockAttack = true;
            Player_Interact.player_Interact.isInteracting = true;
            plrb.velocity=Vector2.zero;
            transform.parent = player.transform;
            offset = transform.localPosition;
            rb2.mass = 1;
        }
        else{
            //rb2.mass = 200f;
            player_Movement.moveBlock=false;
            player_Movement.jump_block=false;
            player_Attack.blockAttack = false;
            Player_Interact.player_Interact.isInteracting = false;
            transform.parent = null;
            offset = Vector2.zero;
            rb2.mass = 2000f;
        }
    }
}
