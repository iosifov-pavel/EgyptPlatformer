using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{

    float speed = 3f;

    public  Transform target;
    [SerializeField] GameObject LevelManager;
    Manager_Level LM;
    bool on_boss_stage=false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, transform.position.z);
        LM = LevelManager.GetComponent<Manager_Level>();
        on_boss_stage = LM.level.boss_stage;
    }

    // Update is called once per frame
    void LateUpdate() 
    {
        if(target==null) return;
        Vector3 position = target.position;
        position.z = transform.position.z;
        transform.position = Vector3.Lerp (transform.position, position, speed* Time.deltaTime);
    }

    public void LockCamera(Transform newT){
        target = newT;
    }
}
