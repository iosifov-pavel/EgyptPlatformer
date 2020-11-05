using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Move : MonoBehaviour{
    Touch touch;
    bool butonPressed = false;
    Vector2 original;
    Vector3 center;
    [SerializeField] GameObject player;
    [SerializeField] GameObject firepoint;
    Player_Attack pa;
    Player_Movement pm;
    Transform stick;
    Vector3 dest;
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
            if(dest.sqrMagnitude>10000){
                dest = Vector3.ClampMagnitude(dest,170f);
            }
               
            switch(touch.phase){
            case TouchPhase.Began:
                pm.stickPressed = true;
                Debug.Log("Touch Began");
                stick.position=center+dest;
                local = stick.localPosition;
                power = local.magnitude;
                angle = Vector3.Angle(Vector3.right,dest);
                upside = (local.y>25);
                enough = (local.x>25 || local.x<-25);
                dir = local.x>=0 ? 1 : -1;
                break;
            case TouchPhase.Moved:
                Debug.Log("Touch Moved");
                stick.position=center+dest;
                local = stick.localPosition;
                power = local.magnitude;
                angle = Vector3.Angle(Vector3.right,dest);
                upside = (local.y>25);
                enough = (local.x>25 || local.x<-25);
                dir = local.x>=0 ? 1 : -1;
               
               if(enough){
                   if(angle<25 || angle>155){
                       pa.buttonUp=false;
                   } else pa.buttonUp=true;
                   pm.direction.x = dir * (power-25f) * 0.025f;
               }
               else{
                   pm.direction.x=0f;
                   if(local.y>25){
                       pa.buttonUp=true;
                   } else pa.buttonUp=false;
               }
               if(touch.position.x>width){
                   stick.localPosition = original;
               }
                break;
            case TouchPhase.Canceled:
                pm.stickPressed = false;
                pa.buttonUp=false;
                Debug.Log("Touch Canceled");
                stick.localPosition = original;
                break;
            case TouchPhase.Ended:
                pm.stickPressed = false;
                pa.buttonUp=false;
                Debug.Log("Touch Ended");
                stick.localPosition = original;
                break;
            }
        }
    }
}