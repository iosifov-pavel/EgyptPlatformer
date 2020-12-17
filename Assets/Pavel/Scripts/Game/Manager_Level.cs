using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_Level : MonoBehaviour
{
    [SerializeField] public int level_id;
    [SerializeField] public int section_id;
    GameObject ms,mg;
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
    static bool coin_get=false;
    static int v=0;

    // Start is called before the first frame update
    void Start()
    {
        //ms = GameObject.FindGameObjectWithTag("SectionManager");
        mg = GameObject.FindGameObjectWithTag("GameManager");
        //manager_Section = ms.GetComponent<Manager_Section>();
        manager_Game = mg.GetComponent<Manager_Game>();
        level = manager_Game.game_info.sections[section_id-1].levels[level_id-1];
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
        time+=Time.deltaTime;
        float ms=time;
        int seconds = (int)ms;
        int minutes = seconds/60;
        //string g = string.Format("{0:0.00}",time);
        string t = $"{minutes}:{seconds-60*minutes}.{time - 1*seconds}";
        Debug.Log(t);

        if(killed){
            kills++;
            killed=false;
        }
        if(coin_get){
            coins+=v;
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
}


