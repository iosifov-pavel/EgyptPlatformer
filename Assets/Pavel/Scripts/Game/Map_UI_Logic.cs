using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map_UI_Logic : MonoBehaviour
{
    GameObject mg;
    Manager_Game manager_Game;
    List<Transform> levels = new List<Transform>();
    Game info;
    [SerializeField] List<Sprite> names;
    [SerializeField] Sprite blocked, open, complete, locked;
    // Start is called before the first frame update
    void Start()
    {
        mg = GameObject.FindGameObjectWithTag("GameManager");
        manager_Game = mg.GetComponent<Manager_Game>();
        info = manager_Game.game_info;
        Update_View();
    }

    void Update_View(){
        Transform[] childs = GetComponentsInChildren<Transform>();
        foreach(Transform child in childs){
            if(child.gameObject.tag=="Level"){
                levels.Add(child);
            }
        }
        foreach(Transform lvl in levels){
            int i = levels.IndexOf(lvl);
            if(info.sections[i].blocked){
                lvl.GetComponent<Image>().sprite = blocked;
                lvl.GetChild(0).GetComponent<Image>().sprite = locked;
                lvl.GetComponent<Button>().interactable=false;
            }
            else if(info.sections[i].complete){
                lvl.GetComponent<Image>().sprite = complete;
                lvl.GetChild(0).GetComponent<Image>().sprite = names[i];
                lvl.GetComponent<Button>().interactable=true;
            }
            else{
                lvl.GetComponent<Image>().sprite = open;
                lvl.GetChild(0).GetComponent<Image>().sprite = names[i];
                lvl.GetComponent<Button>().interactable=true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
