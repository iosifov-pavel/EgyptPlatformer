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


    public void SaveToFile(){
        if(!File.Exists(playerDataPath)){
            Debug.Log("Create Directory");
            Directory.CreateDirectory(playerDataPath);
        }
        if(!File.Exists(file)){
            Debug.Log("Create File");
            File.Create(file).Close();
        }
        string json = JsonUtility.ToJson(game_info, true);
        Debug.Log("Saving as JSON: " + json);
        FileStream fs = new FileStream(file,FileMode.OpenOrCreate, FileAccess.ReadWrite);
        StreamWriter sr = new StreamWriter(fs);
        sr.WriteLine(json);
        sr.Close();
        fs.Close();
    }

    void LoadFromFile(){
        if(!File.Exists(file)){
            GetComponent<Game_Preload>().Loading();
            Debug.Log("Sorry, Load from Blank");
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



