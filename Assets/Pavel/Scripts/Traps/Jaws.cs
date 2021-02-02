using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jaws : MonoBehaviour
{
    Transform left,right, decoy;
    Vector2 left_point,right_point;
    Vector2 decoy_pos, original, left_pos, right_pos;
    [SerializeField] Transform target_pos;
    Vector2 target_p;
    EnemyCircleEyes eyes;
    bool triggered = false, canAttack = false, jumped = false, done=false, cangoback=false;
    [SerializeField] float up_speed = 2f;
    [SerializeField] float time_before_attack = 1f;
    [SerializeField] float rot_speed = 1.5f, up_time=1f ,coldown = 1f;
    void Start()
    {
        original = transform.position;
        decoy = transform.GetChild(0);
        decoy_pos = decoy.position;
        left = transform.GetChild(1);
        left_pos = left.position;
        right = transform.GetChild(2);
        right_pos = right.position;
        left_point = transform.GetChild(4).position;
        right_point = transform.GetChild(5).position;
        eyes = decoy.gameObject.GetComponent<EnemyCircleEyes>();
        target_p = target_pos.position;
    }

    // Update is called once per frame
    void Update()
    {
        target_pos.position = target_p;
        decoy.position = decoy_pos;
        if(triggered==true){
            decoy.gameObject.SetActive(false);
            Jump();
        }
        else {
            Transform player = eyes.GetPlayer();
            if(player!=null){
                triggered = true;
            }
        }
    }

    void Jump(){
        if(canAttack) CloseTeeth();
        else{
            if(jumped && !canAttack){
                StartCoroutine(delayAttack());
            }
            transform.position = Vector2.MoveTowards(transform.position,target_p,up_speed*Time.deltaTime);
            if((Vector2)transform.position==target_p && !jumped && !canAttack){
                jumped = true;
            }
        }
    }

    IEnumerator delayAttack(){
        jumped = false;
        yield return new WaitForSeconds(time_before_attack);
        canAttack = true;
        left_point = transform.GetChild(4).position;
        right_point = transform.GetChild(5).position;
    }

    void CloseTeeth(){
        if(cangoback) goBack();
        else{
            if(done && !cangoback) StartCoroutine(upstay());
            if(Mathf.Abs(left.rotation.eulerAngles.z)>=90 && Mathf.Abs(right.rotation.eulerAngles.z)>=90){
                done = true;
            }
            else{
                left.RotateAround(left_point, new Vector3(0,0,-1), rot_speed*Time.deltaTime);
                right.RotateAround(right_point, new Vector3(0,0,1), rot_speed*Time.deltaTime);
            }
        }
    }

    IEnumerator upstay(){
        yield return new WaitForSeconds(up_time);
        cangoback = true;
    }

    void goBack(){
        bool rot_back = false, move_back = false;
        if((Vector2)transform.position == original){
            move_back = true;
        }
        if(Mathf.Abs(left.rotation.eulerAngles.z)>=350 && Mathf.Abs(right.rotation.eulerAngles.z)<=10){
            rot_back = true;
        }
        if(rot_back==true){}
        else{
            left_point = transform.GetChild(4).position;
            right_point = transform.GetChild(5).position;
            left.RotateAround(left_point, new Vector3(0,0,1), rot_speed*Time.deltaTime);
            right.RotateAround(right_point, new Vector3(0,0,-1), rot_speed*Time.deltaTime);
        }
        if(move_back==true){}
        else{
            transform.position = Vector2.MoveTowards(transform.position,original,up_speed*Time.deltaTime);
        }
        if(move_back && rot_back){
            StartCoroutine(restartAttack());
        }
    }


    IEnumerator restartAttack(){
        yield return new WaitForSeconds(coldown);
        triggered = false;
        canAttack = false;
        jumped = false;
        done = false;
        cangoback = false;
        decoy.gameObject.SetActive(true);
        StopAllCoroutines();
    }
}
