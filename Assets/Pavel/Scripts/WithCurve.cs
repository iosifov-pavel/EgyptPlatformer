using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithCurve : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AnimationCurve parabola;
    [SerializeField] [Range(10,100)] int precision=10;
    [SerializeField] float multiplier = 1;
    [SerializeField] float speed = 2f;
    [SerializeField] Vector3 a,b;
    [SerializeField] int iterator = 0;
    public float step;
    public float value;
    Vector3 prev_pos;
    void Start()
    {
        transform.position = a;
        step = 1f / (float)precision;
        Debug.DrawLine(a,b,Color.red,1000f);
        prev_pos = transform.position;
        //iterator = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(iterator<0) iterator=0;
        if(iterator>precision) iterator = precision;
        value = iterator*step;
        float curve_value = parabola.Evaluate(value);
        float result_c_value = curve_value*multiplier;
        Debug.Log("C " +result_c_value);
        Vector3 new_base_pos = Vector3.Lerp(a,b,value);
        Debug.Log("B " +new_base_pos);
        Vector3 direction = (b - a).normalized;
        Vector3 norm = new Vector3(direction.y*-1,direction.x,0);
        Debug.Log("N " +norm);
        Vector3 result = new_base_pos + norm * result_c_value;
        Debug.Log("R " +result);
        transform.position = result;
        Debug.DrawLine(transform.position, prev_pos, Color.green, 10f);
        prev_pos = transform.position;
        //Move();
    }

    void Move(Vector2 normal, float curve_v){

    }
}
