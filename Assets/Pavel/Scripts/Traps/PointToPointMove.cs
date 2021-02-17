using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToPointMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<Transform> points;
    [SerializeField] bool cycle = true;
    [SerializeField] float speed = 4f;
    [SerializeField] Transform body;
    [SerializeField] bool forward = true;
    Transform destination;
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        foreach(Transform point in points){
            Gizmos.DrawSphere(point.position, 0.1f);
            if(points.IndexOf(point)==points.Count-1){
                if(cycle) Gizmos.DrawLine(point.position,points[0].position);
            }
            else{
                int next = points.IndexOf(point) +1;
                Gizmos.DrawLine(point.position,points[next].position);
            }
        }
    }
    void Start()
    {
        body.position = points[0].position;
        destination = points[0];
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPoint(destination);
        CheckArrive(destination);
    }

    void MoveToPoint(Transform point){
        body.position = Vector2.MoveTowards(body.position,point.position, speed*Time.deltaTime);
    }

    void CheckArrive(Transform point){
        if((Vector2)body.position == (Vector2)point.position){
            if(cycle){
                if(forward){
                    if(points.IndexOf(point)==points.Count-1){
                        destination = points[0];
                    }
                    else destination = points[points.IndexOf(point)+1];
                }
                else{
                    if(points.IndexOf(point)==0){
                        destination = points[points.Count-1];
                    }
                    else destination = points[points.IndexOf(point) - 1];
                }
            }
            else{
                if(points.IndexOf(point) == points.Count - 1){
                    forward=false;
                }
                else if(points.IndexOf(point) == 0){
                    forward = true;
                }
                if(forward) destination = points[points.IndexOf(point) + 1];
                else destination = points[points.IndexOf(point) - 1];
            }
        }
    }
}
