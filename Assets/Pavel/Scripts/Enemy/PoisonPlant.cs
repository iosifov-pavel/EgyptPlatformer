using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPlant : MonoBehaviour
{
    [SerializeField] Sprite active, sleep;
    [SerializeField] GameObject neck, spot, eyes;
    [SerializeField] float speed = 1.5f;
    [SerializeField] bool constantAttack = true;
    bool readyToAttack = true;
    SpriteRenderer sprite_head,sprite_neck;
    EnemyCircleEyes enemyCircleEyes;
    float angle;
    Vector2 target, original, spot_place;
    bool playerInRange = false;
    Transform weakSpot;
    // Start is called before the first frame update
    void Start()
    {
        weakSpot = transform.GetChild(0);
        original = transform.position;
        spot_place = spot.transform.position;
        target = original;
        sprite_head = GetComponent<SpriteRenderer>();
        sprite_neck = neck.GetComponent<SpriteRenderer>();
        enemyCircleEyes = eyes.GetComponent<EnemyCircleEyes>();
    }

    // Update is called once per frame
    void Update()
    {
        SeekPlayer();
        if(constantAttack)Move();
        else MoveC();
        updateView();
    }

    void SeekPlayer(){
        Transform t = enemyCircleEyes.GetPlayer();
        if(t!=null){
           if(!constantAttack){
               if(!readyToAttack) return;
               readyToAttack = false;
           }
           sprite_head.sprite = active;
           playerInRange = true;
           target = t.position;
        }
        else{   
            sprite_head.sprite = sleep;
            playerInRange = false;
            target = original;
        }
    }
    
    void Move(){
        Vector2 toTarget = target - (Vector2)transform.position;
        Vector2 newPosition = (Vector2)transform.position + toTarget;
        transform.position = Vector2.MoveTowards(transform.position,newPosition,speed*Time.deltaTime);
        weakSpot.position = transform.position;
        //spot.transform.rotation = Quaternion.Euler(0,0,0);
    }

    void MoveC(){
        if(readyToAttack) return;
        spot.transform.position = Vector2.MoveTowards(spot.transform.position,target,speed*Time.deltaTime);
        weakSpot.position = spot.transform.position;
        if((Vector2)spot.transform.position==target){
            if(target==original){
                readyToAttack=true;
            }
            else target = original;
        }
    }

    void updateView(){
        Vector2 neck_pos = (spot.transform.position + transform.position)/2;
        neck.transform.position = neck_pos;
        Vector2 toHead = transform.position - spot.transform.position;
        angle = Vector2.SignedAngle(spot.transform.up,toHead);
        neck.transform.rotation = Quaternion.Euler(0,0,angle);
        transform.rotation = Quaternion.Euler(0,0,angle);
        spot.transform.rotation = Quaternion.Euler(0,0,0);
        spot.transform.position = spot_place;
        eyes.transform.position = spot_place;
        sprite_neck.size = new Vector2(2,toHead.magnitude/transform.localScale.y);
    }
}
