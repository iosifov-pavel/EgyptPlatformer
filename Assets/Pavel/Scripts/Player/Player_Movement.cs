using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

   // public bool inWind = false;
   // public GameObject windZone;
    private float speed = 100f;
    float speed_multiplier =1;
    float mass;
    public Vector2 multiplier = Vector2.one;
    float multi_timer=0;
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
    [SerializeField] float jump_force = 8f;
    float jump_force_multiplier=1;
    float jump_time = -111f;
    public float jump_time_max = 0.16f;
    int jump_max = 2;
    public int jumps;
    public bool isJumping=false, jump_block=false, buttonJump=false;
    public float gravity = 2.2f;
    public bool cant_jump=false;
    public Vector2 verical;
    public float hor,ver;
    public float inertia=0;
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
    [SerializeField] ParticleSystem dust, onGround;
    ParticleSystem.EmissionModule dust_e;
    //--------------------------------------
    public bool stickPressed = false;
    public bool blocked = false;
    public bool moveBlock = false;
    [SerializeField] AudioSource steps;
    
   // PhysicsMaterial2D OnSlope;
    // Start is called before the first frame update
    void Start(){
        dust_e = dust.emission;
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

    private void OnDrawGizmos() {
        Vector2 draw= (Vector2)transform.position + direction;
        Gizmos.DrawLine(transform.position,draw);
        //Debug.DrawLine(transform.position,draw,Color.green,0.01f);
    }

    // Update is called once per frame
    void Update(){
        if(stickPressed){
        } 
        else {
             direction = new Vector2(0, 0);
             stick_delta = new Vector2(0,0);
             hor = 0; 
             ver = 0;
             dust_e.enabled = false;
        }
        hor+=stick_delta.x;
        ver+=stick_delta.y;
        if(!blocked){
            anima.setDirection(rb.velocity.x);
            CheckGround();
            DeepCheckGround();
            anima.setBoolAnimation("Ground", isGrounded);
            anima.setFloatAnimation("Velocity",Mathf.Abs(rb.velocity.x));
            if(moveBlock){
                anima.setFloatAnimation("Direction",Mathf.Abs(0));
            }
            else anima.setFloatAnimation("Direction",Mathf.Abs(direction.x));
        }
        if(!moveBlock)if((direction.x > 0 && tran.localScale.x < 0)||(direction.x < 0 && tran.localScale.x > 0)){
            Flip();
        }
        if(Mathf.Abs(direction.x)>0.2f && isGrounded){
            dust_e.enabled=true;
        }
        else{
            dust_e.enabled=false;
        }
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
        Vertical();
        AdditionalMove();
        CustomPhysics();
        PostMove();
        }
    }

    void GetInput(){
        if( Mathf.Abs(direction.x )<0.2) direction.x = 0;
        //if(blocked) direction.x=0;
        if(direction.x==0) facing=0;
        else facing = (int)Mathf.Sign(direction.x);
        move = new Vector2((direction.x)*Time.deltaTime*speed*speed_multiplier, rb.velocity.y);
        if(Mathf.Abs(direction.x)>0.3f && isGrounded) {
            if(steps.isPlaying){}
            else steps.Play();
        }
        else steps.Stop();
    }

    void Horizontal(){
        if(moveBlock) return;
        if (Mathf.Abs(move.x) > maxSpeed) {
            move = new Vector2(Mathf.Sign(move.x) * maxSpeed, rb.velocity.y);
        }
        rb.velocity = move;
    }

    void Vertical(){
        if(jump_block || cant_jump) return;
        anima.setFloatAnimation("vSpeed",rb.velocity.y);
        if(buttonJump && jumps<2){
            Player_Sounds.sounds.PlaySound("jump");
            //onGround.Play();
            dust_e.enabled = false;
            air_direction_change=false;
            isJumping=true;  
            rb.drag=2f;
            jumps++;
            anima.setBoolAnimation("Ground",false);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            if(jumps==2) rb.AddForce(Vector3.up * (jump_force * jump_force_multiplier), ForceMode2D.Impulse);
            else rb.AddForce(Vector3.up * jump_force * jump_force_multiplier, ForceMode2D.Impulse);
            buttonJump = false;
        }
    }

    void AdditionalMove(){
        if(multi_timer>0) multi_timer-=Time.deltaTime;
        if(multi_timer==-111){}
        else if(multi_timer<=0) multiplier=Vector2.one;
        Vector2 new_v = rb.velocity;
        new_v.x*=multiplier.x;
        new_v.y*=multiplier.y;
        rb.velocity=new_v;
        if(source_names.Count<=0) return;
        Vector2 summary=Vector2.zero;
        foreach(string name in source_names){
            int i = source_names.IndexOf(name);
            if(source_times[i]==-1) summary+=sources[i];
            if(source_times[i]>0){
                summary+=sources[i];
                source_times[i]--;
            }
        }
        rb.velocity+=summary;
        foreach(string name in source_names){
            int i = source_names.IndexOf(name);
            if(source_times[i]==0){
                source_names.RemoveAt(i);
                sources.RemoveAt(i);
                source_times.RemoveAt(i); 
            }
        }
    }

    void CheckGround(){  
        bool check=false;
        Collider2D[] hits = new Collider2D[10];
        Physics2D.OverlapCollider(checkground, new ContactFilter2D(),hits);
        foreach(Collider2D hit in hits){
            if(hit!=null && (hit.gameObject.tag=="Ground" || hit.gameObject.tag=="Trap")){
                isGrounded=true;
                if(isJumping || isFalling){
                    buttonJump = false;
                    onGround.Play();
                }
                check=true;
                isJumping=false;
                isFalling = false;
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
            isFalling = true;
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
            if(direction.x==0 && last_velocity!=0){
                rb.velocity +=new Vector2(inertia,0); 
            }
        } 
        else {
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
        inertia = rb.velocity.x*0.921f;
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

    public void SetMultiplier(Vector2 multi, float time){
        multiplier=multi;
        multi_timer = time;
    }
    public void ResetMultiplier(){
        multiplier=Vector2.one;
        multi_timer = 0;
    }

    public void multylow(float low)
    {       
        speed_multiplier = low;
        jump_force_multiplier = low;
    }
    
    public Rigidbody2D GetRb ()
    {   
        return rb;

    }
}
