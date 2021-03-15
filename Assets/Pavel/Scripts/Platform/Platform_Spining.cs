using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Spining : MonoBehaviour
{

    [SerializeField] float speed = 100f;
    [SerializeField] float direction = 1f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        float step = speed*Time.deltaTime;
        transform.Rotate(new Vector3(0,0,step*direction), Space.Self);
    }
}
