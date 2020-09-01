using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 start;
    public Vector3 end =Vector3.zero;
    public float speed = 1f;
    private float percent=0;
    int forward = 1;
    void Start()
    {
        start = transform.position;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, end);
    }

    // Update is called once per frame
    void Update()
    {
        percent +=forward*speed*Time.deltaTime;
        float x = (end.x-start.x)*percent + start.x;
        float y = (end.y - start.y) * percent + start.y;
        transform.position = new Vector3(x,y,start.z);

        if(forward==1&& percent>= 0.9f||forward==-1&&percent<=0.1f)
        {
            forward*=-1;
        }
    }
}
