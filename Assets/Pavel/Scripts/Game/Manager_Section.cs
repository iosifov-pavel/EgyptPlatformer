using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager_Section : MonoBehaviour
{
    // Start is called before the first frame update

    public Section section;
    List<Image> levels = new List<Image>();
    Level active;
    [SerializeField] public int id;
    [SerializeField] Sprite blocked, open, complete;
    [SerializeField] GameObject sectionUI;
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
        section = manager_game.game_info.sections[id-1];
        //FirstStart();
        UpdateUI();
    }

    void UpdateUI(){
        Transform[] childs = sectionUI.GetComponentsInChildren<Transform>();
        foreach(Transform child in childs){
            if(child.gameObject.tag=="Level"){
                levels.Add(child.GetComponent<Image>());
            }
        }
        foreach(Image lvl in levels){
            int i = levels.IndexOf(lvl);
            if(section.levels[i].complete){
                lvl.sprite = complete;
                lvl.GetComponent<Button>().interactable=true;
            }
            else if(section.levels[i].blocked){
                lvl.sprite = blocked;
                lvl.GetComponent<Button>().interactable=false;
            }
            else{
                lvl.sprite = open;
                lvl.GetComponent<Button>().interactable=true;
            }
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


