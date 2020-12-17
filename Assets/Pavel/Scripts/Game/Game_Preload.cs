using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Game_Preload : MonoBehaviour
{
    string saveName;
    string playerDataPath;
    string file;
    Manager_Game manager;
    int first_index=1, second_index=1;
    // Start is called before the first frame update
    void Start()
    {
        saveName = "/save"+".dat";
        playerDataPath = Application.persistentDataPath +"/Save";
        file = playerDataPath+saveName;
        manager= GetComponent<Manager_Game>();
        if(!PlayerPrefs.HasKey("Preload") || !File.Exists(file)){
            PlayerPrefs.SetInt("Preload",1);
            Loading();
        }
    }

    public void Loading(){
        for(first_index=1;first_index<=11;first_index++){
            string sn = string.Format("S{0}",first_index);
            manager.game_info.sections.Add(new Section(first_index,sn));
            for(second_index=1;second_index<=10;second_index++){
                string sl = string.Format("{0}L{1}",first_index,second_index);
                manager.game_info.sections[first_index-1].levels.Add(new Level(second_index,sl));
            }
        }
        manager.game_info.sections[0].levels[0].blocked = false;
        manager.game_info.sections[0].blocked = false;
        manager.SaveToFile();
    }

    // Update is called once per frame
    void Update()
    {

    }
}


[System.Serializable]
public class Game{
    public int Lives;
    public List<Section> sections;
    public Game(){
        Lives = 4;
        sections = new List<Section>();
    }
}


[System.Serializable]
public class Section{
    public string name="sec_def";
    public int section_id;
    public bool complete;
    public bool blocked;
    public List<Level> levels;
    public Section(int id, string s){
        section_id = id;
        name=s;
        levels = new List<Level>();
        complete = false;
        blocked=true;
    }
}


[System.Serializable]
public class Level{
    public int id;
    public bool complete;
    public bool blocked;
    public float time;
    public int score;
    public int total_deaths;
    public int death_per_run;
    public int enemy_killed;
    public int coins;
    public string name="default";

    public Level(int i,string s){
        id = i;
        complete = false;
        blocked = true;
        time = 0;
        score=0;
        coins = 0;
        death_per_run = 0;
        total_deaths = 0;
        enemy_killed=0;
        name = s;
    }
}
