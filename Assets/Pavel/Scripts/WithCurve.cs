using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithCurve : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AnimationCurve parabola;
    [SerializeField] [Range(50,500)] int precision=10;
    [SerializeField] float multiplier = 1;
    [SerializeField] float speed = 2f;
    [SerializeField] Transform body;
    [SerializeField] Transform start,end;
    [SerializeField] bool endless = false;
    [SerializeField] bool destroyAtTheEndPoint = false;
    [SerializeField] bool continueStraightAfterEnd = false;
    [SerializeField] bool goCircleBackward = false;
    [SerializeField] bool backward = false;
    [SerializeField] bool RotateToDirection = true;
    [SerializeField] float delayAtPoints = 0f;
    int iterator = 0;
    float step;
    float value;
    Vector3 prev_pos;
    Vector3 rotate_direction;
    Vector3 direction;
    float distance;
    Vector3 normal;
    Vector3 result_point;
    bool forward = true;
    bool afterEnd = false;
    bool cango = true;
    void Start()
    {
        if(endless){
            parabola.postWrapMode = WrapMode.Loop;
            continueStraightAfterEnd = false;
            destroyAtTheEndPoint = false;
            goCircleBackward = false;
            backward = false;
        }
        if(goCircleBackward) backward = false;
        if(backward) goCircleBackward = false;
        body.transform.position = start.position;
        step = 1f / (float)precision;
        Debug.DrawLine(start.position,end.position,Color.red,0.001f);
        direction = (end.position - start.position).normalized;
        distance = (end.position - start.position).magnitude;
        normal = new Vector3(direction.y*-1,direction.x,0);
        prev_pos = body.transform.position;
        result_point = body.transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        if(!cango) return;
        Check();
        Calculate();
        Move();
    }

    void Check(){
        if(body.transform.position == end.position){
            if(destroyAtTheEndPoint) Destroy(gameObject);
            if(endless){
                start.position = end.position;
                end.position += direction * distance;
                iterator = 0;
            }
            if(continueStraightAfterEnd){
                afterEnd = true;
            }
            if(goCircleBackward){
                Vector3 temp = end.position;
                end.position = start.position;
                start.position = temp;
                iterator = 0;
            }
            if(backward){
                forward = false;
            }
            StartCoroutine(delay());
        }
        else if(body.transform.position == start.position){
            if(backward){
                forward = true;
            }
            StartCoroutine(delay());
        }
        if(body.transform.position == result_point) {
            if(!endless && !backward && (iterator>=precision || iterator<0)) return;
            else if(backward){
                if(forward) iterator++;
                else iterator--;
            }
            else iterator++;
        }
    }

    IEnumerator delay(){
        cango = false;
        yield return new WaitForSeconds(delayAtPoints);
        cango = true;
    }

    void Calculate(){
        if(afterEnd) return;
        float iterator_step = iterator*step;
        float curve_value = parabola.Evaluate(iterator_step);
        value = curve_value*multiplier;
        Debug.Log("C " + value);
        Debug.Log("N " + normal);
        result_point = Vector3.Lerp(start.position,end.position,iterator_step);
        result_point = result_point + normal * value;
        Debug.Log("R " + result_point);
        Debug.Log("R+N " + result_point);
    }

    void Move(){
        if(prev_pos != body.transform.position){
            Debug.DrawLine(body.transform.position, prev_pos, Color.green, 10f);
            rotate_direction = body.transform.position - prev_pos;
            if(RotateToDirection) body.transform.forward = rotate_direction;
            prev_pos = body.transform.position;
        }
        if(afterEnd){
            Vector3 after_end_direction = rotate_direction;
            Vector3 new_position = body.transform.position + after_end_direction;
            body.transform.position = Vector3.MoveTowards(body.transform.position,new_position,speed*Time.deltaTime);
        }
        else{
            body.transform.position = Vector3.MoveTowards(body.transform.position,result_point,speed*Time.deltaTime);
        }
    }
}
