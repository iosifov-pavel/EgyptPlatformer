using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    public bool inWind = false;
    public GameObject windZone;
    private float speed = 100f;

    float mass;
    public float multiplier = 1f;
    [SerializeField] private float maxSpeed = 2.8f;
    public Vector2 direction;
    Vector2 move;
    public Vector2 stick_delta_y,stick_delta;
    public int facing=0;
    //---------------------------
    List<string> source_names = new List<string>();
    List<Vector2> sources = new List<Vector2>();
    List<int> source_times = new List<int>();
    //--------------------------
    float jump_force = 5f;
    float jump_time = -111f;
    public float jump_time_max = 0.16f;
    int jump_max = 2;
    public int jumps;
    public bool isJumping=false, jump_block=false, buttonJump=false;
    private float gravity = 2.2f;
    public bool cant_jump=false;
    public Vector2 verical;
    public float hor,ver;
    float inertia=0;
    float last_velocity=0;
    bool air_direction_change=false;
    //-------------------------------
    RaycastHit2D hit1,hit2;
    BoxCollider2D box;
    bool lastcheck=false;
    public bool isFalling=false;
    public bool isGrounded=true;
    float offset;
    float height;
    Vector2 ray;
    public bool onSlope = false;
    public float[] slopeangle = new float[2];
    LayerMask ground;
    //----------------------------
    Rigidbody2D rb;
    Player_Health ph;
    Transform tran;
    BoxCollider2D checkground;
    Player_Animation anima;
    PhysicsMaterial2D normal;
    [SerializeField] PhysicsMaterial2D zero;
    [SerializeField] PhysicsMaterial2D slope;
    [SerializeField] PhysicsMaterial2D stop_material;
    //--------------------------------------
    public bool stickPressed = false;
    public bool blocked = false;
    
    public int lives;
   // PhysicsMaterial2D OnSlope;
    // Start is called before the first frame update
    void Start(){
        jumps=0;
        rb = GetComponent<Rigidbody2D>();
        rb.drag=1f;
        tran = GetComponent<Transform>();
        ph = GetComponent<Player_Health>();
        checkground = tran.GetChild(1).gameObject.GetComponent<BoxCollider2D>();
        anima = GetComponent<Player_Animation>();
        normal = rb.sharedMaterial;
        lastcheck=isGrounded;
        ray = Vector2.down;
        box = GetComponent<BoxCollider2D>();
        offset = box.size.x/2 * transform.localScale.x;
        height = box.size.y/2 * transform.localScale.y;
        ground = LayerMask.GetMask("Ground");
        mass= GetComponent<Rigidbody2D>().mass;
    }

    // Update is called once per frame
    void Update(){
        //lives=Game_Manager.lives;
        if(stickPressed){
        } else {
             direction = new Vector2(0, 0);
             stick_delta = new Vector2(0,0);
             //stick_delta_y = new Vector2(0,0);
             hor = 0; 
             ver = 0;
             //verical = new Vector2(0,0);
        }
        hor+=stick_delta.x;
        ver+=stick_delta.y;
        //if(!blocked) verical+=stick_delta_y;
        //if(Mathf.Abs(hor)>160) hor = 0;
        //if(Mathf.Abs(ver)>160) ver=0;
        if(!blocked) anima.setDirection(direction.x);
        CheckGround();
        DeepCheckGround();
        anima.setBoolAnimation("Ground", isGrounded);
    }

    void Jump(){
        //if(isJumping) verical.x=0;
        //if(verical.x>enough_for_jump){
        //    if(!isJumping && jumps<jump_count){
        //        can_jump=true;
        //        jumps++;
        //        reset=false;
        //    }
        //    verical.x=0;
        //    verical.y=0;
        //    if(jumps==2&& jump_time==-111) jump_time=jump_time_max-0.08f;
        //    else if(jump_time==-111) jump_time=jump_time_max;
        //}
//
        //if(can_jump){
        //   if(jump_time>=0) jump_time-=Time.deltaTime;
        //   else if(jump_time<0) can_jump=false;
        //}
//
        //if(verical.y>enough_for_reset && !reset && !isGrounded){
        //    verical.x=50;
        //    verical.y=0;
        //    isJumping=false;
        //    reset=true;
        //    can_jump=false;
        //    jump_time=-111;
        //}
    }

    public void ResetJumpCount(){
        jumps=1;
    }

    private void FixedUpdate() {
        if(ph.dead){
            CustomPhysics();
            return;
        }
        GetInput(); 
        if(!blocked){
        PreMove();
        Horizontal();
        Jump();
        Vertical();
        AdditionalMove();
        CustomPhysics();
        PostMove();
        }

        if(inWind)
        {
           rb.AddForce(windZone.GetComponent<Enemy_Wind_Local>().diraction*windZone.GetComponent<Enemy_Wind_Local>().strength);
        }
    }

    void GetInput(){
        if( Mathf.Abs(direction.x )<0.2) direction.x = 0;
        if(direction.x==0) facing=0;
        else facing = (int)Mathf.Sign(direction.x);
        move = new Vector2((direction.x)*Time.deltaTime*speed, rb.velocity.y);
    }

    void Horizontal(){
        if (Mathf.Abs(move.x) > maxSpeed) {
            move = new Vector2(Mathf.Sign(move.x) * maxSpeed, rb.velocity.y);
        }
        rb.velocity = move;
        anima.setFloatAnimation("Velocity",Mathf.Abs(rb.velocity.x));
        anima.setFloatAnimation("Direction",Mathf.Abs(direction.x));

        if((direction.x > 0 && tran.localScale.x < 0)||(direction.x < 0 && tran.localScale.x > 0)){
            Flip();
        }
    }

    void Vertical(){
        if(jump_block || cant_jump) return;
        anima.setFloatAnimation("vSpeed",rb.velocity.y);
        if(buttonJump && jumps<2){
            air_direction_change=false;
            isJumping=true;  
            rb.drag=2f;
            anima.setBoolAnimation("Ground",false);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            if(jumps==2) rb.AddForce(Vector3.up * (jump_force), ForceMode2D.Impulse);
            else rb.AddForce(Vector3.up * jump_force, ForceMode2D.Impulse);
        }
    }

    void AdditionalMove(){
        Vector2 summary=Vector2.zero;
        if(source_names.Count<=0) return;
        foreach(string name in source_names){
            int i = source_names.IndexOf(name);
            if(source_times[i]==-1) summary+=sources[i];
            if(source_times[i]>0){
                summary+=sources[i];
                source_times[i]--;
            }
            if(source_times[i]==0){
                source_names.RemoveAt(i);
                sources.RemoveAt(i);
                source_times.RemoveAt(i); 
            }
        }
        rb.velocity*=multiplier;
        rb.velocity+=summary;
    }

    void CheckGround(){  
        bool check=false;
        Collider2D[] hits = new Collider2D[10];
        Physics2D.OverlapCollider(checkground, new ContactFilter2D(),hits);
        foreach(Collider2D hit in hits){
            if(hit!=null && (hit.gameObject.tag=="Ground" || hit.gameObject.tag=="Trap")){
                isGrounded=true;
                check=true;
                isJumping=false;
                cant_jump = false;
                jumps=0;
                inertia=0;
                last_velocity=0;
                air_direction_change=false;
                break;
            }
            check = false;
            isGrounded=false;
        }
        if(!isJumping && lastcheck && !check){
            lastcheck=false;
            jumps=1;
        }
        lastcheck=check;
    }

    void DeepCheckGround(){
        Vector2 pos,pos2;
            pos = (Vector2)transform.position + new Vector2(-offset,-height);
            pos2 = (Vector2)transform.position + new Vector2(offset,-height);

            hit1 = Physics2D.Raycast(pos,ray,0.3f,ground);
            hit2 = Physics2D.Raycast(pos2,ray,0.3f,ground);
            if(hit1.collider==null && hit2.collider==null){
                onSlope=false;
                return;
            }
            float diff = Mathf.Abs(hit1.distance-hit2.distance);
            slopeangle[0] = Mathf.Sign(hit1.normal.x) * Vector2.Angle(transform.up, hit1.normal);
            slopeangle[1] = Mathf.Sign(hit2.normal.x) * Vector2.Angle(transform.up, hit2.normal);

            if(diff<0.2f && (Mathf.Abs(slopeangle[0])>3 || Mathf.Abs(slopeangle[1])>3) ){
                onSlope=true;
            }
            else onSlope = false;
    }

    void Flip(){
        Vector3 thisScale = tran.localScale;
        thisScale.x *= -1;
        tran.localScale = thisScale;
    }

    void CustomPhysics(){
        bool directionchanged = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);
        bool needtostop = (rb.velocity.x!=0f && direction.x==0);
        if(isGrounded){
            if(ph.dead){
                rb.drag=8;
                return;
            } 
            rb.gravityScale = gravity;
            rb.drag=1f;
            if(directionchanged || !stickPressed || needtostop){
            }
        } else {
            rb.gravityScale = gravity;
            if(ph.dead){
                rb.drag=4;
                return;
            }
            rb.drag=2f;
            if(direction.x==0){
                rb.velocity +=new Vector2(inertia,0); 
            }
            if(Mathf.Sign(last_velocity) != Mathf.Sign(rb.velocity.x)){
                air_direction_change=true;
            }
            if(air_direction_change) rb.velocity*=new Vector2(0.8f,1);
        }
        //if(inertia!=0 && Mathf.Sign(inertia) != Mathf.Sign(rb.velocity.x)) inertia *= 0.7f;
        //else
        inertia = rb.velocity.x*0.90f;
        last_velocity = rb.velocity.x;
    }

    void PreMove(){
        if(onSlope){
            if(facing==1){
                if(Mathf.Abs(slopeangle[1])>50){
                    
                }
                else rb.sharedMaterial=slope;
            }
            else if(facing==-1){
                if(Mathf.Abs(slopeangle[0])>50){
                    
                }
                else rb.sharedMaterial=slope;
            }
            else {
                if(Mathf.Abs(slopeangle[1])>50 || Mathf.Abs(slopeangle[0])>50){
                    rb.sharedMaterial = normal;
                }
                else rb.sharedMaterial=stop_material;
            }
        }
         else if(!isGrounded){
            rb.sharedMaterial = zero;
        }
        else rb.sharedMaterial = normal;
    }

    void PostMove(){
        if(onSlope){

        } 
    }

    public void SetOtherSource(string name, Vector2 source, int frames){
        source_names.Add(name);
        sources.Add(source);
        source_times.Add(frames);
    }

    public void ResetOtherSource(string name){
        if(sources.Count==0) return;
        int i =  source_names.IndexOf(name);
        source_names.RemoveAt(i);
        sources.RemoveAt(i);
        source_times.RemoveAt(i);
    }

    public void BlockMovement(float time){
        StartCoroutine(Block(time));
    }

    public void unBlock(){
        blocked=false;
        StopAllCoroutines();
    }

    IEnumerator Block(float time){
        blocked = true;
        yield return new WaitForSeconds(time);
        blocked = false;
    }

    public void Speed_Up(float time){
        StartCoroutine(speedup(time));
    }
    IEnumerator speedup(float t){
        maxSpeed = 4;
        yield return new WaitForSeconds(t);
        maxSpeed = 3;
    }

    //private void OnTriggerStay2D(Collider2D collision) {
    //    
    //    if (collision.gameObject.tag == "Ladder"){
    //        rb.bodyType = RigidbodyType2D.Kinematic;
    //        transform.Translate(Vector3.up * Input.GetAxis("Vertical") * speed * 0.5f * Time.deltaTime);
//
    //        
    //    }
    //}
//
    //    private void OnTriggerExit2D(Collider2D collision) 
    //{
    //      if (collision.gameObject.tag == "Ladder")    
    //      {
    //          
    //          rb.bodyType = RigidbodyType2D.Dynamic;
    //      }
    //}

    public void multylow(float low)
    {       
        jump_force= jump_force*low;
        maxSpeed = maxSpeed*low;
        //mass=mass*100;
        
    }
    
    

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "windArea")
        {   
            //other.gameObject.GetComponent<Player_Movement>().GetRb();
            windZone=other.gameObject;
            inWind = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "windArea")
        {   
            inWind = false;
        }   
    }
}
