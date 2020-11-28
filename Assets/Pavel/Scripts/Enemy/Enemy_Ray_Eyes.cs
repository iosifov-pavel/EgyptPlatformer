using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ray_Eyes : MonoBehaviour
{
    // Start is called before the first frame update
    float height;
    BoxCollider2D box;
    int dir;
    Vector2 eyes;
    RaycastHit2D hit;
    LayerMask player;
    LayerMask player2;
    LayerMask p;
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        height = box.size.y / 2;
        eyes = Vector2.zero;
        player = LayerMask.GetMask("Player");
        player2 = LayerMask.GetMask("Damaged");
        p = player | player2;
    }

    // Update is called once per frame
    void Update()
    {
        dir=(int)Mathf.Sign(transform.localScale.x)*1;
        Ray();
    }


    void Ray(){
        eyes = transform.position;
        hit =  Physics2D.Raycast(eyes,Vector3.right*dir, 4f,p);
        Debug.DrawRay(eyes,Vector3.right*dir*4f,Color.red,0.02f);
    }

    public Transform Check(){
        if(hit.collider!=null){
            return hit.collider.transform.root;
        }
        else return null;
    }
}
