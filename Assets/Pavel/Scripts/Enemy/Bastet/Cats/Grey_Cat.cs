using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grey_Cat : MonoBehaviour
{
    Enemy_Ray_Eyes eyes;
    Enemy_Ground_Patroling1 egp;
    Rigidbody2D rb;
    float distance;
    int dir = 1;
    Transform player;
    float active_speed;
    bool can_attack = true;
    float delay_attack = 0.5f;
    bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {
        eyes = GetComponent<Enemy_Ray_Eyes>();
        egp = GetComponent<Enemy_Ground_Patroling1>();
        active_speed = egp.speed * 2;
    }

    // Update is called once per frame
    void Update()
    {
        dir = (int)Mathf.Sign(transform.localScale.x) * 1;
        //if(eyes.Check()!=null){
        //    player = eyes.Check();
        //    distance=Mathf.Abs(transform.position.x-player.position.x);
        //}
        player = eyes.Check(); 
        if(!can_attack) return;
        if(player!=null) triggered = true;
        if(egp.changedDirection) triggered = false; 
        if(triggered) egp.speed = active_speed;
        else egp.speed = active_speed/2; 
    }
}
