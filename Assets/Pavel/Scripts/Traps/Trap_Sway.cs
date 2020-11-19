using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Sway : MonoBehaviour
{
    float base_speed = 50;
    float dop_speed = 0;
    float sway_angle = 50;
    float curr_angle;
    [SerializeField] int dir = 1;
    // Start is called before the first frame update
    void Start()
    {
         Quaternion angle = Quaternion.Euler(0,0,-dir*sway_angle);
         transform.rotation = angle;
         curr_angle=0;
         //accelaration=speed/100;
    }

    // Update is called once per frame
    void Update()
    {
        float percent = Mathf.Abs(curr_angle/(2*sway_angle)*100);
        if(percent<=50){
            dop_speed=(base_speed*2*(2*percent-50)/100) + 15;
        } else{
            dop_speed=(base_speed*2*(50-2*(percent-50))/100) + 15;
        } 
        float step = dir*(base_speed+dop_speed)*Time.deltaTime;
        curr_angle+=step;
        if(Mathf.Abs(curr_angle)>=2*sway_angle){
            dir*=-1;
            curr_angle=0;
            dop_speed=0;
        }
        transform.Rotate(0,0,step);
    }
}
