using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Section_UI_Logic : MonoBehaviour
{
    //[SerializeField] GameObject SM_;
    [SerializeField] GameObject Sec_UI;
    Manager_Section M_Sec;
    Section section;
    
    [SerializeField] Sprite blocked, open, complete, locked;
    [SerializeField] List<Sprite> numbers;
    List<Transform> levels = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        //SM = GameObject.FindGameObjectWithTag("SectionManager").GetComponent<Manager_Section>();
        //Sec_UI = 
        M_Sec = GetComponent<Manager_Section>();
        section = M_Sec.section;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        void UpdateUI(){
        Transform[] childs = Sec_UI.GetComponentsInChildren<Transform>();
        foreach(Transform child in childs){
            if(child.gameObject.tag=="Level"){
                levels.Add(child);
            }
        }
        foreach(Transform lvl in levels){
            int i = levels.IndexOf(lvl);
            if(section.levels[i].complete){
                lvl.GetComponent<Image>().sprite = complete;
                lvl.GetChild(0).GetComponent<Image>().sprite = numbers[i];
                lvl.GetComponent<Button>().interactable=true;
            }
            else if(M_Sec.section.levels[i].blocked){
                lvl.GetComponent<Image>().sprite = blocked;
                lvl.GetChild(0).GetComponent<Image>().sprite = locked;
                lvl.GetComponent<Button>().interactable=false;
            }
            else{
                lvl.GetComponent<Image>().sprite = open;
                lvl.GetChild(0).GetComponent<Image>().sprite = numbers[i];
                lvl.GetComponent<Button>().interactable=true;
            }
        }
    }
}
