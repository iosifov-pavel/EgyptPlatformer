using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Manager_Game : MonoBehaviour
{
    string saveName;
    string playerDataPath;
    string file;
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
        saveName = "/save"+".dat";
        playerDataPath = Application.persistentDataPath +"/Save";
        file = playerDataPath+saveName;
        DontDestroyOnLoad(gameObject);
        if(!PlayerPrefs.HasKey("Preload") || !File.Exists(file)){
            game_info = new Game();
        }
        else{
            LoadFromFile();
        }
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
        if(!File.Exists(playerDataPath)){
            Directory.CreateDirectory(playerDataPath);
        }
        if(!File.Exists(file)){
            File.Create(file).Close();
        }
        FileStream fs = new FileStream(file,FileMode.OpenOrCreate, FileAccess.ReadWrite);
        StreamWriter sr = new StreamWriter(fs);
        sr.WriteLine(s);
        sr.Close();
        fs.Close();
    }

    void LoadFromFile(){
        if(!File.Exists(file)){
            File.Create(file);
            Debug.Log("Sorry");
            return;
        }
        FileStream fs = new FileStream(file,FileMode.Open, FileAccess.Read);
        StreamReader sr = new StreamReader(fs);
        string json = sr.ReadToEnd();
        sr.Close();
        fs.Close();
        game_info = JsonUtility.FromJson<Game>(json);
    }
}



