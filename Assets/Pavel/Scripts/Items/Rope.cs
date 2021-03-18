using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    // Start is called before the first frame update
    Movement player_Movement;
    Player_Health player_Health;
    public Vector2 playerInput;
    Vector2 pre_push;
    Rigidbody2D rb_player;
    GameObject player;
    bool contact = false, ready = false, delay=false;
    float ready_time = 0.25f, delay_time=0.6f, timer=0;
    [SerializeField] bool straight = true;
    [SerializeField] Button_Jump button_Jump;
    Vector2 forward;
    float end_max,end_min;
    BoxCollider2D box;
    public float distance=0;
    void Start()
    {
        forward = transform.up;
        box = GetComponent<BoxCollider2D>();
        if(straight){
            end_max = box.bounds.max.y;
            end_min = box.bounds.min.y; 
        }
        else{
            end_max = box.bounds.max.x;
            end_min = box.bounds.min.x; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(delay || !contact) return;
        pre_push=transform.right;
        if(contact) timer+=Time.deltaTime;
        if(contact && timer>=ready_time && !ready){
            ready = true;
            playerInput=Vector2.zero;
        }
        if(ready){
            player.transform.rotation = Quaternion.identity;
            playerInput = player_Movement.GetInput();
            if(player_Health.isDamaged){
                playerInput=Vector2.zero;
                Jump();
                return;
            }
            if(player_Movement.GetJumpButton()){
                Jump();
                return;
            }
            Vector3 move;
            if(straight){
                move =  new Vector3(0,playerInput.y,0) * Time.deltaTime;
                if(player.transform.position.y+move.y >= end_max) return;
                if(player.transform.position.y+move.y <= end_min) return;
                player.transform.Translate(move);
            }
            else{
                move = new Vector3(playerInput.x,0,0) * Time.deltaTime;
                if(player.transform.position.x+move.x >= end_max) return;
                if(player.transform.position.x+move.x <= end_min) return;
                player.transform.Translate(move);
            }
        }
    }

    void Jump(){
        distance = 0;
        timer=0;
        ready=false;
        contact=false;
        player.transform.parent = null;
        player_Movement.ResetJumpCount();
        player_Movement.RestoreGravity();
        playerInput=playerInput.normalized;
        if(playerInput.y>-0.3f) rb_player.AddForce(playerInput*10f, ForceMode2D.Impulse);
        else rb_player.AddForce(playerInput*5f, ForceMode2D.Impulse);
        StartCoroutine(Delay());
    }

     private void OnTriggerEnter2D(Collider2D other) {
        if(delay || contact) return;
        if(other.gameObject.tag=="GrabWall" || other.gameObject.tag=="GrabCeiling"){
            if(!straight && other.gameObject.tag=="GrabWall") return;
            if(other.gameObject.tag=="GrabCeiling" && straight) return;
            playerInput=Vector2.zero;
            player = other.gameObject.transform.parent.gameObject;
            player_Movement = player.GetComponent<Movement>();
            bool yep = player_Movement.IsJumpOrFall();
            if(!yep) return;
            player.transform.parent = transform;
            player_Health = player.GetComponent<Player_Health>();
            rb_player = player.GetComponent<Rigidbody2D>();
            rb_player.velocity = Vector2.zero;
            rb_player.gravityScale = 0;
            player_Movement.ResetJumpCount();
            player_Movement.BlockMove(true);
            player_Movement.BlockJump(true);
            contact = true;
            if(straight){
                distance = Mathf.Abs(transform.position.x - player.transform.position.x);
            }
            else{  
                distance = Mathf.Abs(transform.position.y - player.transform.position.y);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(delay || contact) return;
        if(other.gameObject.tag=="GrabWall" || other.gameObject.tag=="GrabCeiling"){
            if(!straight && other.gameObject.tag=="GrabWall") return;
            if(other.gameObject.tag=="GrabCeiling" && straight) return;
            playerInput=Vector2.zero;
            player = other.gameObject.transform.parent.gameObject;
            player.transform.parent = transform;
            player_Movement = player.GetComponent<Movement>();
            bool yep = player_Movement.IsJumpOrFall();
            if(!yep) return;
            player_Health = player.GetComponent<Player_Health>();
            rb_player = player.GetComponent<Rigidbody2D>();
            rb_player.velocity = Vector2.zero;
            player_Movement.ResetJumpCount();
            rb_player.gravityScale = 0;
            player_Movement.BlockMove(true);
            player_Movement.BlockJump(true);
            contact = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="GrabWall"|| other.gameObject.tag=="GrabCeiling"){
            if(contact && ready){
                float dis;
                if(straight){
                    dis = Mathf.Abs(transform.position.x - player.transform.position.x);
                }
                else{
                    dis = Mathf.Abs(transform.position.y - player.transform.position.y);
                }
                if(dis>=distance+0.02f){
                    playerInput=Vector2.zero;
                    Jump();
                }
            }
        }
    }
    IEnumerator Delay(){
        playerInput=Vector2.zero;
        delay=true;
        player_Movement.setJumpButton(false);
        player_Movement.BlockMove(false);
        player_Movement.BlockJump(false);
        yield return new WaitForSeconds(0.15f);
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(delay_time-0.25f);
        delay=false;
    }
}
