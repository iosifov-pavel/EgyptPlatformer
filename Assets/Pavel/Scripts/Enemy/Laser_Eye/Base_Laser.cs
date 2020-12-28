using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Laser : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject lasers;
    List<Laser> laser_list = new List<Laser>();
    LayerMask ground;
    [SerializeField] float range=20;
    [SerializeField] int Lasers_count = 1;
    [SerializeField] float rotate_speed=0;
    [SerializeField] float rays_period = 0;
    float offset;
    [SerializeField] float first_ray_angle = 0;

    //Vector2 dir;
    void Start()
    {
        ground = LayerMask.GetMask("Ground");
        lasers = transform.GetChild(0).gameObject;
        lasers.transform.localPosition = Vector2.zero;
        for(int i=0;i<Lasers_count;i++){
            Laser l = new Laser(transform.position, lasers.transform);
            l.laser.name ="L" + i.ToString();
            laser_list.Add(l);
            l.laser.transform.Rotate(new Vector3(0,0,offset));
            offset+=rays_period;
        }
        lasers.transform.Rotate(new Vector3(0,0,first_ray_angle));
    }

    // Update is called once per frame
    void Update(){
        lasers.transform.Rotate(new Vector3(0,0,rotate_speed*Time.deltaTime));
        foreach(Laser l in laser_list){
            l.direction = l.laser.transform.right;
            Vector2 pos = l.laser.transform.position;
            Vector2 dir = l.direction;
            RaycastHit2D hit = Physics2D.Raycast(pos,dir,range,ground);
            if(hit.transform!=null){
                SetLaser(l,hit);
            }
            else{
                ResetLaser(l);
            }
        }
    }


    void SetLaser(Laser l, RaycastHit2D ray){
        l.start = lasers.transform.position;
        l.end = ray.point;
        l.line.SetPosition(0, lasers.transform.position);
        l.line.SetPosition(1, ray.point);
        l.laser_pos = (l.end+l.start)/2;
        float laser_long = ray.distance*2f;
        l.laser.transform.position = l.laser_pos;
        l.size.size = new Vector2(laser_long,l.line.endWidth);
    }

    void ResetLaser(Laser l){
        l.laser.transform.localPosition=Vector3.zero;
        l.size.size = Vector2.zero;
        l.start = lasers.transform.position;
        l.end = lasers.transform.position;
        l.line.SetPosition(1, lasers.transform.position);
        l.line.SetPosition(0, lasers.transform.position);
    }
}

public class Laser{
public GameObject laser;
public Vector2 start,end;
public Vector2 direction;
public BoxCollider2D size;
public Vector3 laser_pos;
public float dist;
public LineRenderer line;

    public Laser(Vector2 s, Transform lasers){
        laser = new GameObject();
        laser.transform.parent = lasers;
        laser.transform.position = lasers.position;
        laser.AddComponent<LineRenderer>();
        laser.AddComponent<Enemy_Damage>();
        laser.AddComponent<BoxCollider2D>();
        size = laser.GetComponent<BoxCollider2D>();
        size.offset = Vector2.zero;
        size.isTrigger=true;
        line = laser.GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.SetPosition(0,laser.transform.position);
        line.SetPosition(1,laser.transform.position);
        line.startWidth = 0.2f;
        line.endWidth = 0.2f;
        direction = laser.transform.right;
        start = s;
    }
}
