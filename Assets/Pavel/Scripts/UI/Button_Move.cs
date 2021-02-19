using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Move : MonoBehaviour{
Touch touch;
int id=-111;
Vector2 original;
Vector2 center;
[SerializeField] GameObject player;
Player_Movement pm;
Transform stick;
Vector2 dest;
float scale;
float dist;
float razbros;
Vector2 local;
float angle;
float power;
float dir_x, dir_y;
bool enough;
bool enogh_y;
float last_y;
float last_x;
float delta_jump=0;
float delta_move=0;
float cumulative_jump=0;
float cumulative_reset=0;
    // Start is called before the first frame update
    void Start(){
        stick = transform.GetChild(0);
        original = stick.localPosition;
        center = transform.position;
        pm = player.GetComponent<Player_Movement>();
        scale = transform.parent.transform.parent.GetComponent<RectTransform>().localScale.x;
        dist = gameObject.GetComponent<RectTransform>().rect.width/2 * scale;
        razbros = (gameObject.GetComponent<RectTransform>().rect.width + 60)/2 * scale;
    }

    // Update is called once per frame
    void Update(){
        debugMove();
        if(Input.touchCount>0){
            Touch[] touches = Input.touches;
            if(id==-111){
                foreach(Touch _touch in touches){
                    float longs = (_touch.position-center).magnitude - (razbros);
                    if(longs<=0){
                        touch = _touch;
                        id = _touch.fingerId; 
                        break;
                    }
                }
            }
            else {
                foreach(Touch _touch in touches){
                    if(_touch.fingerId==id) touch = _touch;
                }
            }
            if(id!=-111){
                dest=touch.position - new Vector2(center.x,center.y);
                if(dest.magnitude>dist){
                    dest = Vector3.ClampMagnitude(dest,dist);
                }
                switch(touch.phase){
                case TouchPhase.Began:
                    Debug.Log("Touch Began");
                    pm.stickPressed = true;
                    cumulative_reset=0;
                    cumulative_jump=0;
                    last_y=0;
                    delta_jump=0;
                    last_x=0;
                    delta_move=0;
                    Action();
                    break;
                case TouchPhase.Moved:
                    Debug.Log("Touch Moved");
                    Action();
                    Axis();
                    break;
                case TouchPhase.Canceled:
                    pm.stickPressed = false;
                    Debug.Log("Touch Canceled");
                    stick.localPosition = original;
                    break;
                case TouchPhase.Ended:
                    pm.stickPressed = false;
                    id=-111;
                    cumulative_reset=0;
                    cumulative_jump=0;
                    last_y=0;
                    last_x=0;
                    delta_move=0;
                    delta_jump=0;
                    Debug.Log("Touch Ended");
                    stick.localPosition = original;
                    break;
                }
            }
        }
    }

    private void Action(){
        stick.position=center+dest;
        local = stick.localPosition;
        power = local.magnitude;
        angle = Vector3.Angle(Vector3.right,dest);
        enough = (Mathf.Abs(local.x)>40);
        enogh_y = (Mathf.Abs(local.y)>40);
        dir_x = local.x>=0 ? 1 : -1;
        dir_y = local.y>=0 ? 1 : -1;
        if(enough) pm.direction.x = dir_x * (power-40) * 0.03f;
        else pm.direction.x=0f;
        if(enogh_y) pm.direction.y = dir_y * (power-40) * 0.03f;
        else pm.direction.y=0f;
    }

    private void Axis(){
        delta_jump=(local.y-last_y);
        delta_move=(local.x-last_x);

        if(delta_jump>0){
            pm.stick_delta_y= new Vector2(delta_jump,0);
        } else if(delta_jump<0) {
            pm.stick_delta_y= new Vector2(0,Mathf.Abs(delta_jump));
        }
        else{
           pm.stick_delta_y= new Vector2(0,0); 
        }

        pm.stick_delta= new Vector2(delta_move,delta_jump);
        last_y=local.y;
        last_x=local.x;
    }

    public void ResetTouch(){
        pm.stickPressed = false;
        id=-111;
        cumulative_reset=0;
        cumulative_jump=0;
        last_y=0;
        last_x=0;
        delta_move=0;
        delta_jump=0;
        Debug.Log("Touch Ended");
        stick.localPosition = original;
    }

    void debugMove(){
        float tempControlX = Input.GetAxis("Horizontal");
        float tempControlY = Input.GetAxis("Vertical");
        if(tempControlX!=0 || tempControlY!=0){
            pm.stickPressed = true;
            pm.direction.x = tempControlX*3.3f;
            pm.direction.y = tempControlY*3.3f;
            pm.hor=68.1f * pm.direction.x;
            pm.ver=68.1f * pm.direction.y;
        }
        else{
            pm.stickPressed = false;
            pm.direction.x = 0;
            pm.direction.y = 0;
            pm.hor=0;
            pm.ver=0;
        }
    }
}