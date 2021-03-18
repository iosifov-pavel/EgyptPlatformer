using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    Animator animatioController;
    SpriteRenderer head;
    [SerializeField] Sprite right;
    [SerializeField] Sprite stand;
    // Start is called before the first frame update
    void Start()
    {
        animatioController = GetComponent<Animator>();
        head = transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update() {
        //animatioController
    }

    public void setBoolAnimation(string name, bool state){
        animatioController.SetBool(name, state);
    }

    public void setFloatAnimation(string name, float value){
        animatioController.SetFloat(name, value);
    }

    public void setDirection(float dir){
        if(Mathf.Abs(dir)<0.2) head.sprite = stand;
        else head.sprite = right;
        //else head.sprite = 
    }
}
