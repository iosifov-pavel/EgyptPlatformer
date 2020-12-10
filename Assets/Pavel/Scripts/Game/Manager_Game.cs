using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Manager_Game : MonoBehaviour
{
    // Start is called before the first frame update
    public Game game_info;
    private Section active;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateData(Section info){
        bool alredy_exist= false;
        active = info;
        if(game_info.sections.Count>0){
            foreach (Section s in game_info.sections)
            {
                if(info.name == s.name) {
                    updateSection(info,s);
                    alredy_exist=true;
                }
            }
        }
        if(alredy_exist) return;
        else {
            game_info.sections.Add(active);
            SaveAsJSON();
        }
    }

    void updateSection(Section olds, Section news){
        if(olds.levels.Count==news.levels.Count){
            foreach(Level n in news.levels){
                bool already_exist = false;
                foreach(Level o in olds.levels){
                    if(o.name==n.name){
                        updateLevel();
                        already_exist=true;
                    }
                }
                if(already_exist) continue;
                //else game_info.sections;
            }
        }
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
}


[System.Serializable]
public class Game{
    public List<Section> sections;

    public Game(){
        sections = new List<Section>();
    }
}
