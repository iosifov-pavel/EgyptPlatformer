using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{   
    //public Level_Controller rvru;
    //Player_GetCoin player_GetCoin;
    //Player_Attack player_Attack;
    
    Manager_Level LM;
    GameObject UI_win;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            LM = other.gameObject.GetComponent<Player_InfoHolder>().getLM();
            UI_win = other.gameObject.GetComponent<Player_InfoHolder>().getUI().transform.GetChild(3).gameObject;
            //player_GetCoin = other.gameObject.transform.GetChild(3).gameObject.GetComponent<Player_GetCoin>();
            //player_Attack = other.gameObject.GetComponent<Player_Attack>();
            //--------------------------
            if(LM.level.death_per_run==0 && !LM.level.complete) LM.level.death_per_run = LM.death;
            else if(LM.level.death_per_run==0) LM.level.death_per_run=0;
            else if(LM.level.death_per_run>LM.death) LM.level.death_per_run = LM.death;
            //--------------------------
            if(LM.level.time==0) LM.level.time = LM.time;
            else if(LM.level.time>LM.time) LM.level.time = LM.time;
            //--------------------------
            if(LM.level.coins==0) LM.level.coins = LM.coins;
            else if(LM.level.coins<LM.coins) LM.level.coins = LM.coins;
            //--------------------------
            if(LM.level.enemy_killed==0) LM.level.enemy_killed = LM.kills;
            else if(LM.level.enemy_killed<LM.kills) LM.level.enemy_killed = LM.kills;
            //----------------------------
            int score = LM.coins*15 - (int)LM.time + LM.kills*20 - LM.death*25;
            if(LM.level.score==0 && score>=0) LM.level.score = score;
            if(LM.level.score<score) LM.level.score = score;
            //LM.L_complete();
            LM.level.complete = true;
            Time.timeScale=0;
            LM.Save();
            UI_win.SetActive(true);
        }
    }
}
