using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_InfoHolder : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject UI_Holder;
    [SerializeField] GameObject LevelManager_Holder;
    private GameObject UI;
    private Manager_Level lvl;
    void Start()
    {
        UI = UI_Holder;
        lvl = LevelManager_Holder.GetComponent<Manager_Level>();
    }

    public GameObject getUI(){
        return UI;
    }

    public Manager_Level getLM(){
        return lvl;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
