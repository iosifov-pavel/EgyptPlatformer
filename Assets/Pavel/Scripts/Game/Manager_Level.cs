using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_Level : MonoBehaviour
{
    [SerializeField] int level_id;
    [SerializeField] int section_id;
    GameObject ms,mg;
    Manager_Section manager_Section;
    Manager_Game manager_Game;
    public Level level;

    // Start is called before the first frame update
    void Start()
    {
        ms = GameObject.FindGameObjectWithTag("SectionManager");
        mg = GameObject.FindGameObjectWithTag("GameManager");
        manager_Section = ms.GetComponent<Manager_Section>();
        manager_Game = mg.GetComponent<Manager_Game>();
        level = manager_Section.section.levels[level_id-1];
    }

    public void L_update(){
        manager_Section.S_update(level_id);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}


