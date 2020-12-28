using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Laser : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject lasers;
    List<Laser> laser_list = new List<Laser>();
    LayerMask ground, player;
    [SerializeField] float range=20;
    [SerializeField] float rotate_speed=0;
    [SerializeField] int Lasers_count = 1;
    [SerializeField] float rays_period = 0;
    [SerializeField] float first_ray_angle = 0;
    float offset;
    [SerializeField] bool constant = true;
    [SerializeField] float attack_time = 1;
    [SerializeField] float calm_time = 2;
    bool calm = true;
    float timer = 0;
    [SerializeField] bool neededWalls = true;
    [SerializeField] bool seekPlayer = false;
    [SerializeField] float seekRadius = 20;
    [SerializeField] float seekRotationSpeed = 2;
    Vector2 original;

    //Vector2 dir;
    void Start()
    {
        ground = LayerMask.GetMask("Ground");
        player = LayerMask.GetMask("Player") | LayerMask.GetMask("Damaged");
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
        if(seekPlayer){
            rotate_speed=0;
            original = transform.right;
        }
    }

    // Update is called once per frame
    void Update(){
        if(seekPlayer) Seek();
        if(constant) Calculate();
        else{
            timer+=Time.deltaTime;
            if(calm && timer>=calm_time){
                calm=false;
                timer=0;
            } 
            if(!calm && timer>=attack_time){
                calm=true;
                timer=0;
            }
            if(calm){
                foreach(Laser l in laser_list){
                    ResetL(l);
                }
            }
            else{
                Calculate();
            }
        }
    }

    void Seek(){
        Collider2D hit = Physics2D.OverlapCircle(transform.position,seekRadius,player);
        if(hit!=null){
            Vector2 dest = hit.gameObject.transform.position - transform.position;
            transform.right = Vector2.Lerp(transform.right,dest,0.1f);
        }
        else{
            Vector2 dest = original - (Vector2)transform.position;
            transform.right = Vector2.Lerp(transform.right,dest,0.1f);
        }
    }

    void Calculate(){
        lasers.transform.Rotate(new Vector3(0,0,rotate_speed*Time.deltaTime));
        foreach(Laser l in laser_list){
            l.direction = l.laser.transform.right;
            Vector2 pos = lasers.transform.position;
            Vector2 dir = l.direction.normalized;
            RaycastHit2D hit = Physics2D.Raycast(pos,dir,range,ground);
            if(hit.collider!=null){
                SetLaser(l,hit);
            }
            else{
                ResetLaser(l);
            }
        }
    }

    void SetLaser(Laser l, RaycastHit2D ray){
        ResetL(l);
        l.start = lasers.transform.position;
        l.end = ray.point;
        l.line.SetPosition(0, lasers.transform.position);
        l.line.SetPosition(1, ray.point);
        l.laser_pos = (l.end+l.start)/2;
        float laser_long = ray.distance;
        l.laser.transform.position = l.laser_pos;
        l.size.size = new Vector2(laser_long,l.line.endWidth);
    }

    void ResetLaser(Laser l){
        if(neededWalls){
            ResetL(l);
        }
        else{
            ResetL(l);
            l.start = lasers.transform.position;
            l.end =l.start + (Vector2)l.laser.transform.right.normalized*range;
            l.line.SetPosition(0, l.start);
            l.line.SetPosition(1, l.end);
            l.laser_pos = (l.end+l.start)/2;
            float laser_long = (l.end-l.start).magnitude;
            l.laser.transform.position = l.laser_pos;
            l.size.size = new Vector2(laser_long,l.line.endWidth);
        }
    }

    void ResetL(Laser l){
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
