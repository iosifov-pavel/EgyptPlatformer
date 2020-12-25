using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    // Start is called before the first frame update
    Enemy_Ray_Eyes eyes;
    Enemy_Ground_Patroling egp;
    [SerializeField] bool axe=false,spear=false,shield=false;
    GameObject axe_g, spear_g, shield_g;
    int dir;
    Transform player;
    float distance=100;
    float melee = 3;
    void Start()
    {
        eyes = GetComponent<Enemy_Ray_Eyes>();
        egp = GetComponent<Enemy_Ground_Patroling>();
        axe_g = transform.GetChild(2).gameObject;
        spear_g = transform.GetChild(3).gameObject;
        shield_g = transform.GetChild(4).gameObject;
        if(axe) axe_g.SetActive(true);
        if(spear) spear_g.SetActive(true);
        if(shield) shield_g.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        dir = (int)Mathf.Sign(transform.localScale.x)*1;
        if(eyes.Check()!=null){
            player = eyes.Check();
            distance=Mathf.Abs(transform.position.x-player.position.x);
        }
        else return;
        if(distance>3) ThrowSpear();
        else PunchAxe();
    }

    void ThrowSpear(){

    }

    void PunchAxe(){
        
    }
}
