using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset_Playing_UI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject m,a;
    Button_Move button_Move;
    Button_Attack button_Attack;
    void Start()
    {
        button_Move = m.GetComponent<Button_Move>();
        button_Attack = a.GetComponent<Button_Attack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetInput(){
        button_Move.ResetTouch();
        button_Attack.ResetTouch();
    }
}
