using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panter : MonoBehaviour
{
    Enemy_Ray_Eyes eyes;
    EnemyGroundWalking egp;
    Rigidbody2D rb;
    BoxCollider2D box;
    float toFloor;
    LayerMask ground;
    float distance;
    int dir = 1;
    Transform player;
    bool can_attack = true;
    bool triggered = false;
    float delay_attack = 1f;
    [SerializeField] bool jumping = false;
    // Start is called before the first frame update
    void Start()
    {
        eyes = GetComponent<Enemy_Ray_Eyes>();
        egp = transform.parent.GetComponent<EnemyGroundWalking>();
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        toFloor = box.bounds.extents.y;
        ground = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        dir = (int)Mathf.Sign(transform.localScale.x) * 1;
        player = eyes.Check();
        //if(player==null) return; 
        if(player!=null) triggered = true;
        if(egp.ChangeDirection()) triggered = false; 
        if(triggered) egp.SpeedMultiplier(2);
        else egp.SpeedMultiplier(1);
        if(player==null) return; 
        distance=Mathf.Abs(transform.position.x-player.position.x); 
        if(!can_attack) return;
        if(player){
            if(distance<=3f){
                egp.fromOutsideScriptJump=true;
                StartCoroutine(atackDelay());
            }
        }
        //else egp.SpeedMultiplier(1);
    }

    IEnumerator atackDelay(){
        can_attack = false;
        yield return new WaitForSeconds(delay_attack);
        can_attack=true;
    }

    
}
