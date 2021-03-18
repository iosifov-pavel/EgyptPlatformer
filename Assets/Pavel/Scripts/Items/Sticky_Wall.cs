using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky_Wall : MonoBehaviour
{
    // Start is called before the first frame update
    Movement player_Movement;
    Player_Health player_Health;
    [SerializeField] Button_Jump button_Jump;
    public Vector2 playerInput;
    Vector2 pre_push;
    Rigidbody2D rb_player;
    GameObject player;
    bool contact = false, ready = false, delay=false;
    float ready_time = 0.25f, delay_time=0.6f, timer=0;
    public Vector2 distanceV=Vector2.zero;
    public float distance=0;
    void Start()
    {
        pre_push=transform.right;
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
            playerInput=player_Movement.GetInput();
            float angle = Vector2.Angle(pre_push,playerInput);
                if(player_Movement.GetJumpButton() && playerInput.magnitude<=1.1f || player_Health.isDamaged) Fall();
                else if(playerInput.magnitude>1.1f){
                    if(player_Movement.GetJumpButton()) Jump();
                } 
        }
    }

    void Fall(){
        distance = 0;
        distanceV = Vector2.zero;
        player_Movement.ResetJumpCount();
        timer=0;
        ready=false;
        contact=false;
        player_Movement.RestoreGravity();
        player.transform.parent = null;
        StartCoroutine(Delay());
    }

    void Jump(){
        distance = 0;
        distanceV = Vector2.zero;
        timer=0;
        player_Movement.ResetJumpCount();
        ready=false;
        contact=false;
        player.transform.parent = null;
        player_Movement.RestoreGravity();
        playerInput=playerInput.normalized;
        if(playerInput.y>-0.3f) rb_player.AddForce(playerInput*10f, ForceMode2D.Impulse);
        else rb_player.AddForce(playerInput*5f, ForceMode2D.Impulse);
        StartCoroutine(Delay());
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(delay) return;
        if(other.gameObject.tag=="GrabWall"){
            playerInput=Vector2.zero;
            player = other.gameObject.transform.parent.gameObject;
            player_Movement = player.GetComponent<Movement>();
            player_Health = player.GetComponent<Player_Health>();
            rb_player = player.GetComponent<Rigidbody2D>();
            rb_player.velocity = Vector2.zero;
            rb_player.gravityScale = 0;
            player.transform.parent = transform;
            player_Movement.ResetJumpCount();
            player_Movement.BlockAll(true);
            contact = true;
            distanceV =transform.position - player.transform.position;
            distance = (transform.position - player.transform.position).magnitude;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="GrabWall"){
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="GrabWall"){
            float dis = (transform.position - player.transform.position).magnitude;
            if(dis>=distance+0.02f){
                if(contact && ready) Fall();
            }
        }
    }

    IEnumerator Delay(){
        playerInput=Vector2.zero;
        delay=true;
        player_Movement.setJumpButton(false);
        player_Movement.BlockAll(false);
        yield return new WaitForSeconds(0.2f);
        yield return new WaitForSeconds(delay_time-0.25f);
        delay=false;
    }
}
