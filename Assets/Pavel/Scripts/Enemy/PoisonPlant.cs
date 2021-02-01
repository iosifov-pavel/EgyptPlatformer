using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPlant : MonoBehaviour
{
    [SerializeField] Sprite active, sleep;
    [SerializeField] GameObject neck, head;
    SpriteRenderer sprite_head,sprite_neck;
    float angle;
    // Start is called before the first frame update
    void Start()
    {
        sprite_head = head.GetComponent<SpriteRenderer>();
        sprite_neck = neck.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        updateView();
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
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            sprite_head.sprite = sleep;
        }
    }
}
