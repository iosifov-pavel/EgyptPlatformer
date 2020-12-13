using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{   
    public Level_Controller rvru;
    
    Manager_Level LM;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            LM = other.gameObject.GetComponent<Player_InfoHolder>().getLM();
            LM.level.complete = true;
            if(LM.level.time==0) LM.level.time = LM.time;
            else if(LM.level.time>LM.time) LM.level.time = LM.time;
            LM.L_complete();
            rvru.Win();
        }
    }
}
