using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPoint : MonoBehaviour
{
    [SerializeField] Transform point;
    [SerializeField] float speed = 20f;
    [SerializeField] int dir = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(point.position,new Vector3(0,0,1),speed*dir*Time.deltaTime);
    }
}
