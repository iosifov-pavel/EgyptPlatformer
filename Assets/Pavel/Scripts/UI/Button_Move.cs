using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Move : MonoBehaviour{
    Touch touch;
    Vector2 original;
    Vector3 center;
    [SerializeField] GameObject player;
    [SerializeField] GameObject firepoint;
    Player_Attack pa;
    Player_Movement pm;
    Transform stick;
    Vector3 dest;
    float scale;
    float dist;
            Vector2 local;
            float angle;
            float power;
            float dir;
            bool upside;
            bool enough;
    // Start is called before the first frame update
    void Start(){
        stick = transform.GetChild(0);
        original = stick.localPosition;
        center = transform.position;
        pa = firepoint.GetComponent<Player_Attack>();
        pm = player.GetComponent<Player_Movement>();
        scale = transform.parent.transform.parent.GetComponent<RectTransform>().localScale.x;
        dist = gameObject.GetComponent<RectTransform>().rect.width/2 * scale;
        touch.phase = TouchPhase.Ended;
    }

    // Update is called once per frame
    void Update(){
        if(Input.touchCount>0){
            //touch = Input.GetTouch(0);
            Touch[] touches = Input.touches;
            float width = Screen.width/2+50f;
            foreach(Touch _touch in touches){
                if(_touch.position.x>width) continue;
                touch = _touch;
            }

            dest=touch.position - new Vector2(center.x,center.y);
            if(dest.magnitude>dist){
                dest = Vector3.ClampMagnitude(dest,dist);
            }
               
            switch(touch.phase){
            case TouchPhase.Began:
                Debug.Log("Touch Began");
                pm.stickPressed = true;
                Action();
                break;
            case TouchPhase.Moved:
                Debug.Log("Touch Moved");
                Action();
                break;
            case TouchPhase.Canceled:
                pm.stickPressed = false;
                pa.buttonUp=0;
                Debug.Log("Touch Canceled");
                stick.localPosition = original;
                break;
            case TouchPhase.Ended:
                pm.stickPressed = false;
                pa.buttonUp=0;
                Debug.Log("Touch Ended");
                stick.localPosition = original;
                break;
            }
        }
    }

    private void Action(){
        stick.position=center+dest;
        local = stick.localPosition;
        power = local.magnitude;
        angle = Vector3.Angle(Vector3.right,dest);
        upside = (local.y>40);
        enough = (local.x>40 || local.x<-40);
        dir = local.x>=0 ? 1 : -1;

        if(enough){
            if(angle<25 || angle>155){
                pa.buttonUp=0;
            } else if((angle>=25 || angle<=155) && local.y>0){
                pa.buttonUp=2;
            } else pa.buttonUp = 0;
            pm.direction.x = dir * (power-40) * 0.01f;
        }
        else{
            pm.direction.x=0f;
            if(local.y>25){
                pa.buttonUp=1;
            } else pa.buttonUp=0;
        }
    }
}