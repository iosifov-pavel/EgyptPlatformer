using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Sand : MonoBehaviour
{
    // Start is called before the first frame update
    Transform floor;
    BoxCollider2D trigger,floor_collider;
    [SerializeField] bool in_contact = false;
    bool too_low=false;
    Player_Movement player_Movement;
    Player_Health player_Health;
    Vector2 original;
    float speed=1f, speed_up = 0.5f;
    float half_height;
    void Start()
    {
        floor = transform.GetChild(0);
        original = floor.position;
        trigger = GetComponent<BoxCollider2D>();
        floor_collider = floor.gameObject.GetComponent<BoxCollider2D>();
        half_height = trigger.bounds.extents.y * 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckContact();
        CheckLow();
        if(in_contact && !too_low){
            float step = speed * Time.deltaTime;
            Vector2 new_pos = (Vector2)floor.position + Vector2.down;
            floor.position = Vector2.MoveTowards(floor.position,new_pos,step);
        }
        else if(in_contact && too_low){

        }
        else{
            float step = speed_up * Time.deltaTime;
            floor.position = Vector2.MoveTowards(floor.position,original,step);
        }
    }

    void CheckContact(){
        in_contact = false;
        ContactPoint2D[] contacts = new ContactPoint2D[5];
        floor_collider.GetContacts(contacts);
        foreach(ContactPoint2D cp in contacts){
            if(cp.collider!=null){
                if(cp.collider.gameObject.tag=="Player"){
                    in_contact=true;
                    break;
                }
            }
        }
    }

    void CheckLow(){
        if(floor.position.y <= original.y-half_height){
            too_low = true;
        }
        else too_low=false;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            if(other.gameObject.layer==10 || other.gameObject.layer==9){
                StopAllCoroutines();
                player_Movement = other.gameObject.GetComponent<Player_Movement>();
                player_Health = other.gameObject.GetComponent<Player_Health>();
                other.transform.parent = floor;
                player_Movement.multylow(0.5f);
            }
        }
        else if(other.gameObject.tag=="GrabCeiling"){
            player_Health.ChangeHP(-100);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            if(other.gameObject.layer==10 || other.gameObject.layer==9){
                other.transform.parent = null;
                StopAllCoroutines();
                StartCoroutine(DelayedExit());
            }    
        }
    }
    IEnumerator DelayedExit(){
        yield return new WaitForSeconds(1);
        player_Movement.multylow(1);
    }
}
