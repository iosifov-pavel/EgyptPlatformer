using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDash : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform body;
    [SerializeField] Transform trigger;
    [SerializeField] EnemyCircleEyes enemyCircleEyes;
    [SerializeField] float speed = 10;
    [SerializeField] float chasingSpeed = 2.5f;
    [SerializeField] float pauseTime = 2f;
    [SerializeField] float prepareTime = 1f;
    [SerializeField] bool isChasing = false;
    [SerializeField] bool triggerMoving = false;
    Rigidbody2D body_rb;
    bool canAttack = true;
    bool prepared = false;
    bool triggered = false;
    bool startPrepare = false;
    Transform player;
    void Start()
    {
        body_rb = body.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        try{
            Move();
        }
        catch{
            if(body==null) Destroy(gameObject);
        }
    }

    void Move(){
        if(triggerMoving){
            trigger.position = body.position;
        }
        player = enemyCircleEyes.GetPlayer();
        if(player!=null && !triggered){
            triggered = true;
        }
        else if(player==null){
            body.position = Vector2.Lerp(body.position,transform.position,0.05f);
            triggered = false;
            startPrepare = false;
            body.gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(body.gameObject.GetComponent<SpriteRenderer>().color, Color.white,0.05f);
        } 
        if(triggered){
            if(!canAttack){
                body.gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(body.gameObject.GetComponent<SpriteRenderer>().color, Color.white,0.05f);
                return;
            } 
            body.gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(body.gameObject.GetComponent<SpriteRenderer>().color, Color.red,0.05f);
            if(!startPrepare)StartCoroutine(getPrepared());
            if(prepared){
                Vector3 toPlayer = player.position - body.transform.position;
                if(isChasing){
                    body_rb.velocity = Vector2.zero;
                    body_rb.AddForce(toPlayer.normalized*chasingSpeed*0.6f, ForceMode2D.Impulse);
                }
                else{
                    body_rb.AddForce(toPlayer.normalized*speed, ForceMode2D.Impulse);
                    StartCoroutine(delay());
                }
            }
        }   
    }

    IEnumerator getPrepared(){
        startPrepare = true;
        yield return new WaitForSeconds(prepareTime);
        prepared = true;
    }

    IEnumerator delay(){
        prepared = false;
        startPrepare = false;
        canAttack = false;
        yield return new WaitForSeconds(pauseTime);
        canAttack = true;
    }
}
