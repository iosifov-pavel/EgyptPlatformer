using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Game_Preload : MonoBehaviour
{
    string saveName;
    string playerDataPath;
    string file;
    Manager_Game manager;
    int first_index=1, second_index=1;
    Scene scene;
    //public bool scenesIsloaded = false, sceneIsLoading=false;
    //public bool levelIsLoading=false, levelIsLoaded=false;
    public bool first=true,second=false;
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
        manager.SaveAsJSON();
    }

    // Update is called once per frame
    void Update()
    {
         //if(first) FirstStep();
         //if(second) SecondStep();
    }

    //void FirstStep(){
    //    int count = SceneManager.sceneCountInBuildSettings;
    //    if(count<=0) return;
    //    if(!sceneIsLoading){
    //        string s= string.Format("S{0}",first_index);
    //        SceneManager.LoadScene(s, LoadSceneMode.Single);
    //        scene = SceneManager.GetSceneByName(s);
    //        sceneIsLoading=true;
    //    }
    //    if(scene.isLoaded && sceneIsLoading && scenesIsloaded){
    //        first_index++;
    //        sceneIsLoading=false;
    //        scenesIsloaded=false;
    //    }
    //    if(first_index==12){
    //        first_index=1;
    //        first=false;
    //        second=true;
    //        sceneIsLoading=false;
    //        scenesIsloaded=false;
    //    }
    //}

    //void SecondStep(){
    //    if(!sceneIsLoading){
    //        string s= string.Format("{0}L{1}",first_index,second_index);
    //        SceneManager.LoadScene(s, LoadSceneMode.Additive);
    //        scene = SceneManager.GetSceneByName(s);
    //        if(!scene.IsValid()) {
    //            first_index++;
    //            second_index=1;
    //            return;
    //        }
    //        sceneIsLoading=true;
    //    }
    //    if(sceneIsLoading && scenesIsloaded){
    //        second_index++;
    //        sceneIsLoading=false;
    //        scenesIsloaded=false;
    //    }
    //    if(first_index==12){
    //        first=false;
    //        second=false;
    //    }
    //}
}


[System.Serializable]
public class Game{
    public List<Section> sections;

    public Game(){
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
    public int deaths;
    public string name="default";

    public Level(int i,string s){
        id = i;
        complete = false;
        blocked = true;
        time = 0;
        score=0;
        deaths = 0;
        name = s;
    }
}
