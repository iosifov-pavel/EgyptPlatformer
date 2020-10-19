using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Spining : MonoBehaviour
{

    private float speed = 100f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed*Time.deltaTime;
        transform.Rotate(new Vector3(0,0,step), Space.World);
    }
}
