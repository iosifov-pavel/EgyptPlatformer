using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Stage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject LevelM;
    [SerializeField] GameObject cam;
    [SerializeField] float cam_size;
    [SerializeField] GameObject tile_block;
    [SerializeField] GameObject Boss;
    Camera_Follow camera_Follow;
    Camera cam_cam;
    Manager_Level LM;
    Boss_CheckPoint checkPoint;
    Transform camera_lock;
    public bool active = false;
    void Start()
    {
        LM = LevelM.GetComponent<Manager_Level>();
        checkPoint = GetComponent<Boss_CheckPoint>();
        camera_lock = transform.GetChild(0);
        camera_Follow = cam.GetComponent<Camera_Follow>();
        cam_cam = cam.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(checkPoint.active){
            Boss.GetComponent<Boss_Health>().is_active=true;
            camera_Follow.LockCamera(camera_lock);
            cam_cam.orthographicSize = cam_size;
            tile_block.SetActive(true);
        }
    }
}
