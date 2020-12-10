using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_Section : MonoBehaviour
{
    // Start is called before the first frame update
    public Section section;
    Level active;
    string name_sc;
    [SerializeField] int id;
    [SerializeField] Sprite blocked, open, complete;
    Manager_Game manager_game;
    GameObject manager_g;

    private void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SectionManager");
        if (objs.Length > 1){
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);    
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        name_sc = SceneManager.GetActiveScene().name;
        section = new Section(id, name_sc);
        manager_g = GameObject.FindGameObjectWithTag("GameManager");
        manager_game = manager_g.GetComponent<Manager_Game>();
    }

    public void getActiveLevelInfo(Level info){
        bool alredy_exist= false;
        active = info;
        if(section.levels.Count>0){
            foreach (Level l in section.levels)
            {
                if(info.name == l.name) alredy_exist = true;
            }
        }
        if(alredy_exist) return;
        else{
            section.levels.Add(active);
            manager_game.updateData(section);
            //string json = JsonUtility.ToJson(section, true);
            //Debug.Log("Saving as JSON: " + json);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name=="Map" || SceneManager.GetActiveScene().name=="Main Menu"){
            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public class Section{
    public List<Level> levels;
    public string name="sec_def";
    public int section_id;
    public bool complete;
    public Section(int id, string s){
        section_id = id;
        name=s;
        levels = new List<Level>();
        complete = false;
    }
}
