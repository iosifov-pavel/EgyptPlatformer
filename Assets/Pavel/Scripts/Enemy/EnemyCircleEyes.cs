using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleEyes : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            player = other.transform;
       //     if(!constantAttack){
       //         if(!readyToAttack) return;
       //         readyToAttack = false;
       //     }
       //     sprite_head.sprite = active;
       //     playerInRange = true;
       //     target = other.transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            player = other.transform;
        //   if(!constantAttack){
        //       if(!readyToAttack) return;
        //       readyToAttack = false;
        //   }
        //   sprite_head.sprite = active;
        //   playerInRange = true;
        //   target = other.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            player = null;
        //    sprite_head.sprite = sleep;
        //    playerInRange = false;
        //    target = original;
        }
    }

    public Transform GetPlayer(){
        return player;
    }
}
