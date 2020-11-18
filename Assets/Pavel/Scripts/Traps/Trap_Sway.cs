using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Sway : MonoBehaviour
{
    [SerializeField] float base_speed = 40;
    [SerializeField] float dop_speed = 0;
    float sway_angle = 50;
    float curr_angle;
    int dir = 1;
    bool go = true;
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
        if(!go) return;
        float percent = Mathf.Abs(curr_angle/(2*sway_angle)*100);
        if(percent<=50){
            dop_speed=Mathf.Abs(base_speed*2*percent/100);
        } else{
            dop_speed=Mathf.Abs(base_speed*2*(100-percent)/100);
        }
        float step = dir*(base_speed+dop_speed)*Time.deltaTime;
        curr_angle+=step;
        if(Mathf.Abs(curr_angle)>=2*sway_angle){
            dir*=-1;
            curr_angle=0;
            dop_speed=0;
            StartCoroutine(Wait());
        }
        transform.Rotate(0,0,step);
    }

    IEnumerator Wait(){
        go=false;
        yield return new WaitForSeconds(0.11f);
        go=true;
    }
}
