using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Laser : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 start,end;
    RaycastHit2D hit2D;
    LayerMask ground;
    //Vector2 dir;
    void Start()
    {
        start = transform.position;
        ground = LayerMask.GetMask("Ground");
        //dir = new Vector2(-1,0);
    }

    // Update is called once per frame
    void Update()
    {
        hit2D = Physics2D.Raycast(transform.position,transform.right*-1,20f,ground);
        if(hit2D.collider!=null){
            end = hit2D.point;
            Debug.DrawLine(start,end,Color.red, 0.1f);
        }
    }
}
