using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Sway : MonoBehaviour
{
    [SerializeField] float speed = 0;
    float accelaration = 5;
    float sway_angle = 50;
    float curr_angle;
    int dir = 1;
    // Start is called before the first frame update
    void Start()
    {
         Quaternion angle = Quaternion.Euler(0,0,-dir*sway_angle);
         transform.rotation = angle;
         curr_angle=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(curr_angle)<sway_angle){
            speed+=accelaration;
        } else speed -=accelaration-1;
        float step = dir*speed*Time.deltaTime;
        curr_angle+=step;
        if(Mathf.Abs(curr_angle)>=2*sway_angle){
            dir*=-1;
            curr_angle=0;
            speed=0;
        }
        transform.Rotate(0,0,step);
    }
}
