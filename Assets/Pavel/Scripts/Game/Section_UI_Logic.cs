using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Section_UI_Logic : MonoBehaviour
{
    //[SerializeField] GameObject SM_;
    Manager_Section SM;
    
    [SerializeField] Sprite blocked, open, complete;
    
    List<Image> levels = new List<Image>();
    // Start is called before the first frame update
    void Start()
    {
        SM = GameObject.FindGameObjectWithTag("SectionManager").GetComponent<Manager_Section>();
        
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        void UpdateUI(){
        Transform[] childs = GetComponentsInChildren<Transform>();
        foreach(Transform child in childs){
            if(child.gameObject.tag=="Level"){
                levels.Add(child.GetComponent<Image>());
            }
        }
        foreach(Image lvl in levels){
            int i = levels.IndexOf(lvl);
            if(SM.section.levels[i].complete){
                lvl.sprite = complete;
                lvl.GetComponent<Button>().interactable=true;
            }
            else if(SM.section.levels[i].blocked){
                lvl.sprite = blocked;
                lvl.GetComponent<Button>().interactable=false;
            }
            else{
                lvl.sprite = open;
                lvl.GetComponent<Button>().interactable=true;
            }
        }
    }
}
