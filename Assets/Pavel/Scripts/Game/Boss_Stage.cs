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
    CheckPoint checkPoint;
    Transform camera_lock;
    void Start()
    {
        LM = LevelM.GetComponent<Manager_Level>();
        checkPoint = GetComponent<CheckPoint>();
        camera_lock = transform.GetChild(0);
        camera_Follow = cam.GetComponent<Camera_Follow>();
        cam_cam = cam.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(checkPoint.active){
            camera_Follow.LockCamera(camera_lock);
            cam_cam.orthographicSize = cam_size;
            tile_block.SetActive(true);
        }
    }
}
