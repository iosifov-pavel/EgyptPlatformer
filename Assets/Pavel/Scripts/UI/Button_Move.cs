using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Move : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Touch touch;
    bool butonPressed = false;
    Vector2 original;
    Vector3 center;
    [SerializeField] GameObject player;
    [SerializeField] GameObject firepoint;
    Player_Attack pa;
    Player_Movement pm;
    // Start is called before the first frame update
    void Start()
    {
        original = transform.localPosition;
        center = transform.position;
        pa = firepoint.GetComponent<Player_Attack>();
        pm = player.GetComponent<Player_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount>0 && butonPressed){
            //touch = Input.GetTouch(0);
            Touch[] touches = Input.touches;
            float width =Screen.width/2;
            foreach(Touch toucha in touches){
                if(toucha.position.x>=width) continue;
                touch = toucha;
            }
            switch(touch.phase){
            case TouchPhase.Began:
                Debug.Log("Touch Began");
            break;
            case TouchPhase.Moved:
            Debug.Log("Touch Moved");
               Vector3 dest=touch.position - new Vector2(center.x,center.y);
               bool upside = (touch.position.y>center.y);
               bool enough = (dest.sqrMagnitude>3025);
               if(dest.sqrMagnitude>10000){
                   dest = Vector3.ClampMagnitude(dest,100f);
               }
               transform.position=center+dest;
               float angle = Vector3.Angle(Vector3.right,dest);
               if(enough){
               if(angle<40){
                   pa.buttonUp=false;
                   pm.direction.x=1;
               } else if(angle>=40 && angle<80){
                   pa.buttonUp=true;
                   pm.direction.x=1;
               } else if(angle>=80 && angle<100 && upside){
                   pa.buttonUp=true;
                   pm.direction.x=0;
               } else if(angle>=100 && angle<140){
                   pa.buttonUp=true;
                   pm.direction.x=-1;
               } else if(angle>=140 && angle<180){
                   pa.buttonUp=false;
                   pm.direction.x=-1;
               }     
               }
               else {
                   pm.direction.x=0;
               }
            break;
            case TouchPhase.Ended:
            Debug.Log("Touch Ended");
            transform.localPosition = original;
            break;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData){
     butonPressed = true;
     pm.stickPressed = true;
     Debug.Log("M pressed");
    }
 
    public void OnPointerUp(PointerEventData eventData){
    butonPressed = false;
    pm.stickPressed = false;
    transform.localPosition = original;
    Debug.Log("M released");
    }
}
