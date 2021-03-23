using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Move : MonoBehaviour
{
    //public Vector3 p1;
    [SerializeField] Vector3 move_point;
    public  Vector3 original,point,destination;
    [SerializeField] bool sameSpeed = true;
    [SerializeField] private float speed = 1f;
    [SerializeField] float speedBack = 1f;
    [SerializeField] float delay = 0;
    float resultSpeed;
    float timer = 0;
    bool start = false;

    
    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        if(!start)Gizmos.DrawLine(transform.position, transform.position + move_point);
        else Gizmos.DrawLine(original, point);
    }

    private void OnEnable() {
        timer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        original = transform.position;
        point = original + move_point;
        start = true;
        destination = point;
        resultSpeed = speed;
    }

    bool GetReady(){
        timer+=Time.deltaTime;
        if(timer>=delay) return true;
        else return false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GetReady()) return;
        float step =  resultSpeed * Time.deltaTime; 
        transform.position =  Vector3.MoveTowards(transform.position, destination, step);
        if(transform.position==point){
            destination = original;
            if(!sameSpeed) resultSpeed = speedBack;
        }
        else if(transform.position==original){
            destination = point;
            if(!sameSpeed) resultSpeed = speed;
        }
    }


}

