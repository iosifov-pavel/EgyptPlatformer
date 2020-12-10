using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Game : MonoBehaviour
{
    // Start is called before the first frame update
    Game game_info;
    Section active;

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
                if(info.name == s.name) alredy_exist = true;
            }
        }
        if(alredy_exist) return;
        else game_info.sections.Add(active);
    }
}


[System.Serializable]
public class Game{
    public List<Section> sections;

    public Game(){
        sections = new List<Section>();
    }
}
