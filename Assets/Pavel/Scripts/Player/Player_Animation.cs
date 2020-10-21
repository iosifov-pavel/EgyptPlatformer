using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setBoolAnimation(string name, bool state){
        anim.SetBool(name, state);
    }

    public void setFloatAnimation(string name, float value){
        anim.SetFloat(name, value);
    }
}
