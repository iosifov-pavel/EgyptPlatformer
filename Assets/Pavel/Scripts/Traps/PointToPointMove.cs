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
    [SerializeField] bool lerping = true;
    [SerializeField] float lerpingPercent = 50f;
    [SerializeField] float startLerpingTimer = 0.2f;
    [SerializeField] bool visualize = true;
    [SerializeField] Transform chainParent;
    [SerializeField] GameObject chainPrefab;
    [SerializeField] float delayOnPoints = 0.1f;
    [SerializeField] float delayOnStart = 0;
    float timer = 0;
    Transform destination;
    Transform previous;
    Transform nextPoint;
    float lerpingTimer;
    bool stop = false;
    bool startLerping = false;
    private void OnDrawGizmos() {
        foreach(Transform point in points){
            if(points.IndexOf(point)==0){
                Gizmos.color = Color.green;
            }
            else if(points.IndexOf(point)==points.Count-1){
                Gizmos.color = Color.red;
            }
            else Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(point.position, 0.1f);
            Gizmos.color = Color.blue;
            if(points.IndexOf(point)==points.Count-1){
                if(cycle) Gizmos.DrawLine(point.position,points[0].position);
            }
            else{
                int next = points.IndexOf(point) +1;
                Gizmos.DrawLine(point.position,points[next].position);
            }
        }
    }
    
    private void OnEnable() {
        timer = 0;
    }
    
    void Start()
    {
        body.position = points[0].position;
        destination = points[1];
        previous = points[0];
        nextPoint = GetNewPoint(points[1]);
        if(visualize) drawChains();
        lerpingTimer = startLerpingTimer;
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

    bool GetReady(){
        timer+=Time.deltaTime;
        if(timer>=delayOnStart) return true;
        else return false;
    }

    void CrateChain(Transform p1, Transform p2){
                Vector3 chainPos = (p1.position + p2.position)/2;
                chainPos.z = 50;
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
        if(!GetReady()) return;
        if(body==null) Destroy(gameObject);
        if(stop) return;
        try{
            CheckArrive(destination);
        }
        catch{}
    }

    void CheckArrive(Transform point){
        if(lerping){
            float distanceToPoint = (body.position-point.position).magnitude;
            float distancePP = (point.position-previous.position).magnitude;
            float distanceToNextPoint = (nextPoint.position-body.position).magnitude;
            float percent = distanceToPoint * 100 / distancePP;
            if(percent<=lerpingPercent && !startLerping){
                startLerping = true;
            } 
            else if((percent>lerpingPercent && startLerping) || (startLerping && distanceToNextPoint<=distanceToPoint)){
                startLerping = false;
                previous = destination;
                destination = nextPoint;
                point = nextPoint;
                nextPoint = GetNewPoint(nextPoint);
                lerpingTimer = startLerpingTimer;
            }
            if(startLerping){
                Vector3 new_pos = Vector3.Lerp(point.position,nextPoint.position,lerpingTimer);
                lerpingTimer+=Time.deltaTime;
                body.position = Vector3.MoveTowards(body.position,new_pos, speed*Time.deltaTime);
            }
            else{
                body.position = Vector3.MoveTowards(body.position,point.position, speed*Time.deltaTime);
            }
        }
        else{
            if((Vector3)body.position == (Vector3)point.position){
                StartCoroutine(Delay());
                previous = destination;
                destination = GetNewPoint(point);
            }
            else body.position = Vector3.MoveTowards(body.position,point.position, speed*Time.deltaTime);
        }
    }

    Transform GetNewPoint(Transform point){
        Transform new_point = null;
        if(cycle){
            if(forward){
                if(points.IndexOf(point)==points.Count-1){
                    new_point = points[0];
                }
                else new_point = points[points.IndexOf(point)+1];
            }
            else{
                if(points.IndexOf(point)==0){
                    new_point = points[points.Count-1];
                }
                else new_point = points[points.IndexOf(point) - 1];
            }
        }
        else{
            if(points.IndexOf(point) == points.Count - 1){
                forward=false;
            }
            else if(points.IndexOf(point) == 0){
                forward = true;
            }
            if(forward) new_point = points[points.IndexOf(point) + 1];
            else new_point = points[points.IndexOf(point) - 1];
        }
        return new_point;
    }

    IEnumerator Delay(){
        stop = true;
        yield return new WaitForSeconds(delayOnPoints);
        stop = false;
    }
}
