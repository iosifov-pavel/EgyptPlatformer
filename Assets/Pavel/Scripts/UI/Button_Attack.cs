﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
 
public class Button_Attack : MonoBehaviour{

Touch touch;
public int id=-111;
Transform stick;
Vector2 original;
Vector2 center;
Vector2 point;
[SerializeField] private GameObject aplayer;
[SerializeField] Transform player;
Player_Attack pa;
float scale;
float dist;
float razbros;
private Camera cam;

    private void Start() {
        cam = Camera.main;
        pa = aplayer.GetComponent<Player_Attack>();
        stick = transform.GetChild(0);
        original = stick.localPosition;
        center = transform.position;
        scale = transform.parent.transform.parent.GetComponent<RectTransform>().localScale.x;
        dist = (gameObject.GetComponent<RectTransform>().rect.width - 120)/2 * scale;
        razbros = (gameObject.GetComponent<RectTransform>().rect.width - 20)/2 * scale;
    }

    private void Update() {
        if(Application.isEditor)debugAttack();
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
                } else {
                    foreach(Touch _touch in touches){
                        if(_touch.fingerId==id) touch = _touch;
                    }
                }
            if(id!=-111){
                point=touch.position - center;
                if(point.magnitude>dist){
                    point = Vector2.ClampMagnitude(point,dist);
                }
                switch(touch.phase){
                case TouchPhase.Began:
                    Debug.Log("Touch Began");
                    Stick();
                    break;
                case TouchPhase.Moved:
                    Debug.Log("Touch Moved");
                    Stick();
                    break;
                case TouchPhase.Canceled:
                    Debug.Log("Touch Canceled");
                    stick.localPosition = original;
                    break;
                case TouchPhase.Ended:
                    id=-111;
                    pa.buttonAttack=false;
                    pa.angle=0;
                    Debug.Log("Touch Ended");
                    stick.localPosition = original;
                    break;
                }
            }
        }
    }

    private void Stick(){
        stick.position=center+point;
        Vector2 local = stick.localPosition;
        float power = local.magnitude;
        float angle = Vector3.Angle(Vector3.right,point);
        if(local.y>=0) pa.bUp=1;
        else pa.bUp=-1;
        bool enough = (power>=60);
        if(enough){
            pa.notEnough = false;
            pa.buttonAttack=true;
            pa.angle=angle;
        }  
        else{
            pa.notEnough = true;
            pa.buttonAttack=true;
        } 
    }

    public void ResetTouch(){
        id=-111;
        pa.buttonAttack=false;
        pa.angle=0;
        Debug.Log("Touch Ended");
        stick.localPosition = original;
    }

    void debugAttack(){
        bool md = Input.GetMouseButton(0);
        if(md){
            pa.notEnough = false;
            pa.buttonAttack=true;
            Vector2 mpos = Input.mousePosition;
            Vector3 mrpos = cam.ScreenToWorldPoint(mpos);
            if(mrpos.y>=pa.gameObject.transform.position.y) pa.bUp=1;
            else pa.bUp=-1;
            Vector2 playpos = pa.gameObject.transform.position;
            Vector2 toMouse = (Vector2)mrpos-playpos;
            float angle = Vector3.Angle(Vector3.right,toMouse);
            pa.angle = angle;
        }
        else{
            pa.buttonAttack=false;
            pa.angle=0;
        }
    }
}