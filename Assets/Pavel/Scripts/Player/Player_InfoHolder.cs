using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_InfoHolder : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject UI_Holder;
    [SerializeField] GameObject LevelManager_Holder;
    private GameObject UI;
    private GameObject LevelManager;
    void Start()
    {
        UI = UI_Holder;
        LevelManager = LevelManager_Holder;
    }

    public GameObject getUI(){
        return UI;
    }

    public GameObject getLM(){
        return LevelManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
