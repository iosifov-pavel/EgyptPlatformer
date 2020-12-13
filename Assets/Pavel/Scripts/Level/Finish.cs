using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{   
    public Level_Controller rvru;
    Player_GetCoin player_GetCoin;
    
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
            LM.level.complete = true;
            if(LM.level.time==0) LM.level.time = LM.time;
            else if(LM.level.time>LM.time) LM.level.time = LM.time;
            if(LM.level.score==0) LM.level.score = player_GetCoin.coins;
            else if(LM.level.score<player_GetCoin.coins) LM.level.score = player_GetCoin.coins;
            LM.L_complete();
            Time.timeScale=0;
            UI_win.SetActive(true);
        }
    }
}
