using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPoint : MonoBehaviour
{
    [SerializeField] Transform point;
    [SerializeField] float speed = 20f;
    [SerializeField] int dir = 1;
    [SerializeField] bool staticChildRotation = false;
    [SerializeField] bool swing = false;
    [SerializeField] float swingAngle = 90f;
    float count=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(swing){
            float angle = speed*dir*Time.deltaTime;
            transform.RotateAround(point.position,new Vector3(0,0,1),speed*dir*Time.deltaTime);
            count+=Mathf.Abs(angle);
            if(count>=Mathf.Abs(swingAngle)){
                count=0;
                dir*=-1;
            }
        }
        else{
            transform.RotateAround(point.position,new Vector3(0,0,1),speed*dir*Time.deltaTime);
            if(staticChildRotation) transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
}
