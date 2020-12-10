﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Manager_Game : MonoBehaviour
{
    // Start is called before the first frame update
    public Game game_info;
    private Section active;
    public bool notfirst=false;

    private void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");
        if (objs.Length > 1){
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);    
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        game_info = new Game();
        FirstStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateData(Section info){
        //bool alredy_exist= false;
        //active = info;
        //if(game_info.sections.Count>0){
        //    foreach (Section s in game_info.sections)
        //    {
        //        if(info.name == s.name) {
        //            updateSection(info,s);
        //            alredy_exist=true;
        //        }
        //    }
        //}
        //if(alredy_exist) return;
        //else {
        //    game_info.sections.Add(active);
        //    SaveAsJSON();
        //}
    }

    void updateSection(Section olds, Section news){
        //if(olds.levels.Count==news.levels.Count){
        //    foreach(Level n in news.levels){
        //        bool already_exist = false;
        //        foreach(Level o in olds.levels){
        //            if(o.name==n.name){
        //                updateLevel();
        //                already_exist=true;
        //            }
        //        }
        //        if(already_exist) continue;
        //        //else game_info.sections;
        //    }
        //}
    }

    void updateLevel(){

    }

    public void SaveAsJSON(){
        string json = JsonUtility.ToJson(game_info, true);
        Debug.Log("Saving as JSON: " + json);
        SaveToFile(json);
    }

    public void SaveToFile(string s){
        string saveName = "/save"+".dat";
        string playerDataPath = Application.persistentDataPath +"/Save";
        string file = playerDataPath+saveName;
        if(!File.Exists(playerDataPath)){
            Directory.CreateDirectory(playerDataPath);
        }
        FileStream fs = new FileStream(file,FileMode.OpenOrCreate, FileAccess.ReadWrite);
        StreamWriter sr = new StreamWriter(fs);
        sr.WriteLine(s);
        sr.Close();
        fs.Close();
    }

    void FirstStart(){
        string saveName = "/save"+".dat";
        string playerDataPath = Application.persistentDataPath +"/Save";
        string file = playerDataPath+saveName;
        int count = SceneManager.sceneCountInBuildSettings;
        if(count<=0) return;
        for(int i =1;i<12;i++){
            string s= string.Format("S{0}",i);
            //SceneManager.LoadScene(s, LoadSceneMode.Additive);
            Scene scene = SceneManager.GetSceneByName(s);
            SceneManager.LoadScene(scene.name, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(scene);
        }
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        float a =1;
        a++;
    }

    public void getSection(Section sc){
        game_info.sections.Add(sc);
    }
}


[System.Serializable]
public class Game{
    public List<Section> sections;

    public Game(){
        sections = new List<Section>();
    }
}
