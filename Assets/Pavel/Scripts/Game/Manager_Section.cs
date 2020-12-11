﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_Section : MonoBehaviour
{
    // Start is called before the first frame update
    public Section section;
    Level active;
    [SerializeField] public int id;
    [SerializeField] Sprite blocked, open, complete;
    Manager_Game manager_game;
    GameObject mg;

    private void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SectionManager");
        if (objs.Length > 1){
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);    
    }
    void Start()
    {
        //section = new Section(id, sec_name);
        mg = GameObject.FindGameObjectWithTag("GameManager");
        manager_game = mg.GetComponent<Manager_Game>();
        //FirstStart();
    }

    //void FirstStart(){
    //    manager_g.GetComponent<Game_Preload>().getSection(section);
    //    Destroy(this.gameObject);
    //}

    public void getActiveLevelInfo(Level info){
       //bool alredy_exist= false;
       //active = info;
       //if(section.levels.Count>0){
       //    foreach (Level l in section.levels)
       //    {
       //        if(info.name == l.name) alredy_exist = true;
       //    }
       //}
       //if(alredy_exist) return;
       //else{
       //    section.levels.Add(active);
       //    manager_game.updateData(section);
       //    //string json = JsonUtility.ToJson(section, true);
       //    //Debug.Log("Saving as JSON: " + json);
       //} 
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name=="Map" || SceneManager.GetActiveScene().name=="Main Menu"){
            Destroy(gameObject);
        }
    }
}


