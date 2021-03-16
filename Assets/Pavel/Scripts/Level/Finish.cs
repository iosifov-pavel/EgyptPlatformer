using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{   
    //public Level_Controller rvru;
    //Player_GetCoin player_GetCoin;
    //Player_Attack player_Attack;
    
    Manager_Level LM;
    GameObject UI_win;
    Text timer,coins,enemy,total;
    [SerializeField] AudioSource source;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            FinishLevel(other.gameObject.transform);
        }
    }

    public void FinishLevel(Transform other){
            source.PlayOneShot(source.clip);
            LM = other.gameObject.GetComponent<Player_InfoHolder>().getLM();
            UI_win = other.gameObject.GetComponent<Player_InfoHolder>().getUI().transform.GetChild(3).gameObject;
            timer = UI_win.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Text>();
            coins = UI_win.transform.GetChild(1).GetChild(3).GetChild(0).GetComponent<Text>();
            enemy = UI_win.transform.GetChild(1).GetChild(4).GetChild(0).GetComponent<Text>();
            total = UI_win.transform.GetChild(1).GetChild(5).GetChild(0).GetComponent<Text>();
            //player_GetCoin = other.gameObject.transform.GetChild(3).gameObject.GetComponent<Player_GetCoin>();
            //player_Attack = other.gameObject.GetComponent<Player_Attack>();
            //--------------------------
            if(LM.level.death_per_run==0 && !LM.level.complete) LM.level.death_per_run = LM.death;
            else if(LM.level.death_per_run==0) LM.level.death_per_run=0;
            else if(LM.level.death_per_run>LM.death) LM.level.death_per_run = LM.death;
            //--------------------------
            timer.text = LM.GetTimer();
            if(LM.level.time==0){
                LM.level.time = LM.time;
                timer.text += "   New Best!";
            } 
            else if(LM.level.time>LM.time){
                LM.level.time = LM.time;
                timer.text += "   New Best!";
            } 
            //--------------------------
            coins.text = LM.coins.ToString();
            if(LM.level.coins==0){
                LM.level.coins = LM.coins;
                if(LM.coins!=0) coins.text += "   New Best!";
            } 
            else if(LM.level.coins<LM.coins){
                LM.level.coins = LM.coins;
                coins.text += "   New Best!";
            } 
            //--------------------------
            enemy.text = LM.kills.ToString();
            if(LM.level.enemy_killed==0){
                LM.level.enemy_killed = LM.kills;
                if(LM.kills!=0) enemy.text += "   New Best!";
            } 
            else if(LM.level.enemy_killed<LM.kills){
                LM.level.enemy_killed = LM.kills;
                enemy.text += "   New Best!";
            } 
            //----------------------------
            int score = LM.coins*10 - (int)LM.time*5 + LM.kills*20;
            if(score<=0) score=0;
            total.text = score.ToString();
            if(LM.level.score==0 && score>=0){
                LM.level.score = score;
                if(score!=0) total.text += "   New Best!";
            } 
            if(LM.level.score<score){
                LM.level.score = score;
                total.text += "   New Best!";
            } 
            //LM.L_complete();
            LM.level.complete = true;
            Time.timeScale=0;
            LM.Save();
            UI_win.SetActive(true);
    }
}
