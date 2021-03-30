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
    LayerMask resultMask;
    [SerializeField] float distance = 4f;
    [SerializeField] bool dontCareAboutGround = false;
    [SerializeField] LayerMask ground;
    Transform target=null;
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        height = box.size.y / 2;
        eyes = Vector2.zero;
        player = LayerMask.GetMask("Player");
        player2 = LayerMask.GetMask("Damaged");
    }

    // Update is called once per frame
    void Update()
    {
        dir=(int)Mathf.Sign(transform.localScale.x)*1;
        Ray();
    }


    void Ray(){
        eyes = transform.position;
        if(dontCareAboutGround){
            resultMask = player | player2;
        }
        else{
            resultMask = player | player2 | ground;
        }
        hit =  Physics2D.Raycast(eyes,Vector3.right*dir, distance,resultMask);
        if(hit.collider != null && dontCareAboutGround){
            target = hit.transform;
        }
        else if(hit.collider != null && !dontCareAboutGround){
            if(hit.collider.gameObject.tag != "Player")target = null;
            else target = hit.transform;
        }
        else target = null;
        Debug.DrawRay(eyes,Vector3.right*dir*distance,Color.red,0.02f);
    }

    public Transform Check(){
        return target;
    }
}
