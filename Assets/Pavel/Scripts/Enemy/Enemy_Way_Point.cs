using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Way_Point : MonoBehaviour
{
    // Start is called before the first frame update
    List<Vector3> points;
    [SerializeField]float speed = 1;
    [SerializeField] float delay = 0.3f;
    [SerializeField] bool cycle=false;
    int active_point=0, child_count=0;
    float delta_x=0, curr_x, step;
    bool forward=true, stop=false;
    Transform body;
    void Start()
    {
        body = transform.GetChild(0);
        step = speed*Time.deltaTime;
        curr_x = transform.position.x;
        points = new List<Vector3>();
        Transform[] childs = GetComponentsInChildren<Transform>();
        foreach(Transform child in childs){
            if(child.gameObject.tag=="Point"){
                points.Add(child.position);
            }
        }
        child_count = points.Count;
        body.transform.position = points[active_point];
    }

    // Update is called once per frame
    void Update()
    {
        if(stop) return;
        delta_x = body.transform.position.x - curr_x;
        if(delta_x>=0){
            Vector3 scalenew = body.transform.localScale;
            if(scalenew.x>0) scalenew.x*=-1;
            body.transform.localScale = scalenew;
        }
        else{
            Vector3 scalenew = body.transform.localScale;
            if(scalenew.x<0) scalenew.x*=-1;
            body.transform.localScale = scalenew;
        }
        
        if(body.transform.position==points[active_point]){
            StartCoroutine(Delay());
            if(cycle){
               if(active_point==child_count-1) active_point=0;
               else active_point++;
            }
            else{
            if(active_point==child_count-1) forward=false;
            else if(active_point==0) forward=true;
            if(forward) active_point++;
            else active_point--;
            }
        } 
        curr_x = body.transform.position.x;
        body.transform.position = Vector3.MoveTowards(body.transform.position,points[active_point],step);
    }

    IEnumerator Delay(){
        stop=true;
        yield return new WaitForSeconds(delay);
        stop=false;
    }
}
