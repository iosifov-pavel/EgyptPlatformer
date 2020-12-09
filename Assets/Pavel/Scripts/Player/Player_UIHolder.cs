using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_UIHolder : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject UI_Holder;
    private GameObject UI;
    void Start()
    {
        UI = UI_Holder;
    }

    public GameObject getUI(){
        return UI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
