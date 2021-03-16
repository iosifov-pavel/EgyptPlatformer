using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager_Level : MonoBehaviour
{
    [SerializeField] public int level_id;
    [SerializeField] public int section_id;
    GameObject ms,mg;
    [SerializeField] GameObject player;
    [SerializeField] GameObject CameraLock;
    [SerializeField] Text lives;
    [SerializeField] Text coinsText;
    [SerializeField] Text timerText;
    //Manager_Section manager_Section;
    public Manager_Game manager_Game;
    public Level level;
    public float time=0;
    public int kills=0;
    static bool killed=false;
    //public int score;
    public int death=0;
    static bool is_dead=false;
    public int coins=0;
    public int collected_coins=0;
    static bool coin_get=false;
    static int v=0;
    string time_string;

    // Start is called before the first frame update
    void Start()
    {
        //ms = GameObject.FindGameObjectWithTag("SectionManager");
        mg = GameObject.FindGameObjectWithTag("GameManager");
        //manager_Section = ms.GetComponent<Manager_Section>();
        manager_Game = mg.GetComponent<Manager_Game>();
        level = manager_Game.game_info.sections[section_id-1].levels[level_id-1];
        collected_coins = manager_Game.game_info.coins;
        //level = manager_Section.section.levels[level_id-1];
        Time.timeScale=1;
    }

    public void L_complete(){
        //manager_Section.S_update(level_id);
    }

    public void Save(){
        manager_Game.SaveToFile();
    }


    // Update is called once per frame
    void Update()
    {
        coinsText.text = manager_Game.game_info.coins.ToString();
        time+=Time.deltaTime;
        float ms=time;
        int seconds = (int)ms;
        int minutes = seconds/60;
        ms = time - 1*seconds;
        string newms= "00000";
        try{
            newms = ms.ToString().Substring(2,2);
        }
        catch{
        }
        int new2ms = int.Parse(newms);
        //string g = string.Format("{0:0.00}",time);
        time_string = $"{minutes}:{seconds-60*minutes}.{new2ms}";
        timerText.text = time_string;
        //Debug.Log(t);

        if(killed){
            kills++;
            killed=false;
        }
        if(coin_get){
            coins+=v;
            manager_Game.game_info.coins+=v;
            if(manager_Game.game_info.coins>=100){
                manager_Game.game_info.Lives++;
                lives.text = manager_Game.game_info.Lives.ToString();
                manager_Game.game_info.coins-=100;
            }
            collected_coins = manager_Game.game_info.coins;
            v=0;
            coin_get=false;
        }
        if(is_dead){
            death++;
            level.total_deaths++;
            is_dead=false;
        }
    }

    public static void EnemyWasKilled(){
        killed=true;
    }

    public static void GetCoin(int value){
        coin_get=true;
        v=value;
    }
    public static void PlayerIsDead(){
        is_dead=true;
    }

    public string GetTimer(){
        return time_string;
    }
}


