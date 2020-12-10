using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_Level : MonoBehaviour
{
    [SerializeField] int level_id;
    GameObject ms;
    Manager_Section manager_Section;
    public Level info;
    string name_lvl;

    // Start is called before the first frame update
    void Start()
    {
        name_lvl = SceneManager.GetActiveScene().name;
        info = new Level(level_id,name_lvl);
        ms = GameObject.FindGameObjectWithTag("SectionManager");
        manager_Section = ms.GetComponent<Manager_Section>();
        manager_Section.getActiveLevelInfo(info);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public Level get_level_info(){
    //    return info;
    //}
}

public class Level{
    public int id;
    public bool complete;
    public bool blocked;
    public float time;
    public int score;
    public string name="default";

    public Level(int i,string s){
        id = i;
        complete = false;
        blocked = false;
        time = 0;
        score=0;
        name = s;
    }
}
