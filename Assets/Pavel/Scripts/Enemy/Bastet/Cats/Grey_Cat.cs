using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grey_Cat : MonoBehaviour
{
    Enemy_Ray_Eyes eyes;
    EnemyGroundWalking egp;
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
        egp = transform.parent.GetComponent<EnemyGroundWalking>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = (int)Mathf.Sign(transform.localScale.x) * 1;
        player = eyes.Check(); 
        if(!can_attack) return;
        if(player!=null) triggered = true;
        if(egp.ChangeDirection()) triggered = false; 
        if(triggered) egp.SpeedMultiplier(2);
        else egp.SpeedMultiplier(1);
    }
}
