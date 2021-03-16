using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child_Movement : MonoBehaviour, IChild
{
    public bool On{get;set;} = false;
    public bool Done{get;set;} = true;
    [SerializeField] bool constantMove = false;
    Platform_Move platform_Move;
    // Start is called before the first frame update
    void Start()
    {
        platform_Move = GetComponent<Platform_Move>();
        if(constantMove) platform_Move.enabled = true;
        else platform_Move.enabled = On;
    }

    // Update is called once per frame
    void Update()
    {
        if(constantMove){
            if(On) {
                platform_Move.destination = platform_Move.point;
            }
            else{
                platform_Move.destination = platform_Move.original;
            }
        }
       else{
           platform_Move.enabled = On;
       } 
    }
}
