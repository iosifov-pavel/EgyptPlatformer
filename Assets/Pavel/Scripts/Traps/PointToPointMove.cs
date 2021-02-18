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
    [SerializeField] bool visualize = true;
    [SerializeField] Transform chainParent;
    [SerializeField] GameObject chainPrefab;
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
        if(visualize) drawChains();
    }

    void drawChains(){
        foreach(Transform point in points){
            int index = points.IndexOf(point);
            if(index == points.Count-1){
                if(cycle) CrateChain(point,points[0]);
            }
            else{
                CrateChain(point,points[index+1]);
            }
        }
    }

    void CrateChain(Transform p1, Transform p2){
                Vector2 chainPos = (p1.position + p2.position)/2;
                Vector2 dir = p1.position - p2.position;
                float angle = Vector2.Angle(Vector2.up, dir);
                GameObject chain = Instantiate(chainPrefab);
                chain.transform.parent = chainParent;
                chain.transform.position = chainPos;
                if(dir.x<0) chain.transform.rotation = Quaternion.Euler(0,0,angle);
                else  chain.transform.rotation = Quaternion.Euler(0,0,-angle);
                chain.transform.localScale = Vector3.one;
                SpriteRenderer sprite = chain.GetComponent<SpriteRenderer>();
                Vector2 newsize = sprite.size;
                newsize.y = dir.magnitude / chainParent.transform.localScale.y;
                sprite.size = newsize;
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
