using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_Section : MonoBehaviour
{
    // Start is called before the first frame update

    public Section section;
    Level active;
    [SerializeField] public int id;
    Manager_Game manager_game;
    GameObject mg;

    //private void Awake() {
    //    GameObject[] objs = GameObject.FindGameObjectsWithTag("SectionManager");
    //    if (objs.Length > 1){
    //        Destroy(this.gameObject);
    //    }
    //    DontDestroyOnLoad(this.gameObject);    
    //}
    void Start()
    {
        //section = new Section(id, sec_name);
        mg = GameObject.FindGameObjectWithTag("GameManager");
        manager_game = mg.GetComponent<Manager_Game>();
        section = manager_game.game_info.sections[id-1];
        //FirstStart();
        S_update();
    }

    public void S_update(){
        //section.levels[lvl_id].blocked=false;
        foreach(Level lvl in section.levels){
            if(lvl.complete){
                int i = section.levels.IndexOf(lvl);
                if(section.levels[i+1].blocked){
                    section.levels[i+1].blocked=false;
                }
            }
        }
        manager_game.SaveToFile();
    }


    // Update is called once per frame
    void Update()
    {
        //if(SceneManager.GetActiveScene().name=="Map" || SceneManager.GetActiveScene().name=="Main Menu"){
        //    Destroy(gameObject);
        //}
    }
}


