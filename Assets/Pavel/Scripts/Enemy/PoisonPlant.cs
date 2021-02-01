using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPlant : MonoBehaviour
{
    [SerializeField] Sprite active, sleep;
    [SerializeField] GameObject neck, head;
    [SerializeField] float speed = 1.5f;
    SpriteRenderer sprite_head,sprite_neck;
    float angle;
    Vector2 target, original;
    bool playerInRange = false;
    // Start is called before the first frame update
    void Start()
    {
        original = head.transform.position;
        target = original;
        sprite_head = head.GetComponent<SpriteRenderer>();
        sprite_neck = neck.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        updateView();
    }

    void Move(){
        Vector2 toTarget = target - (Vector2)head.transform.position;
        Vector2 newPosition = (Vector2)head.transform.position + toTarget;
        head.transform.position = Vector2.Lerp(head.transform.position,newPosition,speed*Time.deltaTime);
    }

    void updateView(){
        Vector2 neck_pos = (head.transform.position + transform.position)/2;
        neck.transform.position = neck_pos;
        Vector2 toHead = head.transform.position - transform.position;
        angle = Vector2.SignedAngle(transform.right,toHead);
        neck.transform.rotation = Quaternion.Euler(0,0,angle-90);
        head.transform.rotation = Quaternion.Euler(0,0,angle-90);
        sprite_neck.size = new Vector2(2,toHead.magnitude/transform.localScale.y);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            sprite_head.sprite = active;
            playerInRange = true;
            target = other.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            sprite_head.sprite = sleep;
            playerInRange = false;
            target = original;
        }
    }
}
