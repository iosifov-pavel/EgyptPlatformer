using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWithoutPhysic : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] BoxCollider2D playerBC;
    [SerializeField] float speed = 2f;
    [SerializeField] float groundAcceleration = 0.2f;
    Vector2 moveInput;
    Vector2 resultMove;
    public bool overlap = false;
    List<Collider2D> groundHits = new List<Collider2D>();
    [SerializeField] LayerMask ground;
    ContactFilter2D filter = new ContactFilter2D();
    // Start is called before the first frame update
    void Start()
    {
        filter.useLayerMask = true;
        filter.layerMask = ground;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckColiders();
    }

    void Move(){
        moveInput = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        moveInput*=speed;
        resultMove = Vector2.MoveTowards(resultMove, moveInput, groundAcceleration*Time.deltaTime);
        transform.Translate(resultMove*Time.deltaTime);
    }

    void CheckColiders(){
        int hitsCount = 0;
        hitsCount = Physics2D.OverlapCollider(playerBC,filter,groundHits);
        if(hitsCount==0){
            overlap = false;
        }
        else{
            overlap = true;
                foreach(Collider2D hit in groundHits){
                    ColliderDistance2D colliderDistance = hit.Distance(playerBC);
                    if (colliderDistance.isOverlapped){
	                	transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
	                }
                }
        }
    }
}
