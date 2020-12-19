using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Laser : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 start,end;
    RaycastHit2D hit2D;
    LayerMask ground;
    GameObject laser;
    BoxCollider2D size;
    Vector3 l_pos;
    float dist;
    LineRenderer line;
    //Vector2 dir;
    void Start()
    {
        start = transform.position;
        ground = LayerMask.GetMask("Ground");
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.SetPosition(0,start);
        laser = transform.GetChild(0).gameObject;
        size= laser.GetComponent<BoxCollider2D>();
        //laser.AddComponent<BoxCollider2D>();
        //laser = new GameObject();
        //laser.name = "laser";
        //laser.transform.parent=transform;
        //laser.transform.localPosition=Vector3.zero;
        //dir = new Vector2(-1,0);
    }

    // Update is called once per frame
    void Update()
    {
        hit2D = Physics2D.Raycast(transform.position,transform.right*-1,20f,ground);
        if(hit2D.collider!=null){
            end = hit2D.point;
            line.SetPosition(1,end);
            Debug.DrawLine(start,end,Color.red, 0.1f);
            SetLaser();
        }
        else {
            line.SetPosition(1,start);
            ResetLaser();
        }
    }


    void SetLaser(){
        l_pos = (end+start)/2;
        float s = hit2D.distance / 0.3f;
        laser.transform.position=l_pos;
        size.size = new Vector2(s,1);
    }

    void ResetLaser(){
        laser.transform.localPosition=Vector3.zero;
        size.size = Vector2.zero;
    }
}
