using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{   
    //public Level_Controller rvru;
    Player_GetCoin player_GetCoin;
    Player_Attack player_Attack;
    
    Manager_Level LM;
    GameObject UI_win;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            LM = other.gameObject.GetComponent<Player_InfoHolder>().getLM();
            UI_win = other.gameObject.GetComponent<Player_InfoHolder>().getUI().transform.GetChild(3).gameObject;
            player_GetCoin = other.gameObject.transform.GetChild(3).gameObject.GetComponent<Player_GetCoin>();
            player_Attack = other.gameObject.GetComponent<Player_Attack>();
            LM.level.complete = true;
            //--------------------------
            if(LM.level.time==0) LM.level.time = LM.time;
            else if(LM.level.time>LM.time) LM.level.time = LM.time;
            //--------------------------
            if(LM.level.coins==0) LM.level.coins = player_GetCoin.coins;
            else if(LM.level.coins<player_GetCoin.coins) LM.level.coins = player_GetCoin.coins;
            //--------------------------
            if(LM.level.enemy_killed==0) LM.level.enemy_killed = player_Attack.kills;
            else if(LM.level.enemy_killed<player_Attack.kills) LM.level.enemy_killed = player_Attack.kills;
            //----------------------------
            int score = player_GetCoin.coins*10 - (int)LM.time + player_Attack.kills*20;
            if(LM.level.score==0) LM.level.score = score;
            if(LM.level.score<score) LM.level.score = score;
            //LM.L_complete();
            Time.timeScale=0;
            UI_win.SetActive(true);
        }
    }
}
