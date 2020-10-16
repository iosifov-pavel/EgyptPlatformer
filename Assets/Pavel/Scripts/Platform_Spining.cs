using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Spining : MonoBehaviour
{
    public float maxAngle;
    public float minAngle;
    private float current;
    private float speed = 6f;
    // Start is called before the first frame update
    void Start()
    {
        current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float step = maxAngle*Time.deltaTime;
        transform.Rotate(new Vector3(0,0,step), Space.World);
        current+=step;
        if(current>=maxAngle){
            float tmp = minAngle;
            minAngle=maxAngle;
            maxAngle=tmp;
            current=0;
        }
    }
}
