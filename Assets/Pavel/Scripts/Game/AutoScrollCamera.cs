using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScrollCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform player;
    [SerializeField] GameObject death;
    [SerializeField] Vector2 move = Vector2.right;
    [SerializeField] float speed = 1;
    [SerializeField] Transform point;
    [SerializeField] Transform cam;
    Player_Movement player_Movement;
    Rigidbody2D playerbody;
    Rigidbody2D rb2;
    Vector2 original;
    bool triggered = false;
    bool moving = false;
    void Start()
    {
        player_Movement = player.gameObject.GetComponent<Player_Movement>();
        playerbody = player.gameObject.GetComponent<Rigidbody2D>();
        original = transform.position;
        rb2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x,player.position.y,transform.position.z);
        if(triggered){
            Vector2 temp = playerbody.velocity;
            temp.x=0;
            playerbody.velocity = temp;
            player_Movement.moveBlock = true;
            player_Movement.jump_block = true;
            cam.position = transform.position + Random.insideUnitSphere*0.15f;
            death.transform.position = Vector3.MoveTowards(death.transform.position, point.position, 4*Time.deltaTime);
            if(death.transform.position == point.position){
                moving = true;
                triggered = false;
                player_Movement.moveBlock=false;
                player_Movement.jump_block = false;
                StartCoroutine(LetsGo());
            } 
        }
    }

    IEnumerator LetsGo(){
        yield return new WaitForSeconds(1f);
        rb2.velocity = speed * move.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(triggered || moving) return;
        if(other.gameObject.tag=="Player"){
            //rb2.velocity = move.normalized*speed;
            triggered = true;
            death.SetActive(true);
        }
    }
}

