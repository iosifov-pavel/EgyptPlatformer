using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Player_Health ph;
    private float speed = 72f;
    private float maxSpeed = 3f;
    public Vector2 direction;
    Vector2 move;
    List<string> source_names = new List<string>();
    List<Vector2> sources = new List<Vector2>();
    List<int> source_times = new List<int>();
    bool isJumping;
    private float jump_force = 4.2f;
    public float jump_time;
    public float jump_max = 0.18f;
    public int jump_count;

    private int max_jump_count;
    public bool isGrounded;
    public bool onSlope = false;
    public bool CanJump;
    private float gravity = 2.2f;
    private float slopeangle;
    Rigidbody2D rb;
    Transform tran;
    BoxCollider2D checkground;
    Player_Animation anima;
    PhysicsMaterial2D normal;
    [SerializeField] PhysicsMaterial2D zero;
    public bool buttonJump = false;
    public bool stickPressed = false;

    public GameObject speeds;
    public bool blocked = false;
    bool lastcheck;
   // PhysicsMaterial2D OnSlope;
    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.drag=1f;
        tran = GetComponent<Transform>();
        ph = GetComponent<Player_Health>();
        checkground = tran.GetChild(1).gameObject.GetComponent<BoxCollider2D>();
        anima = GetComponent<Player_Animation>();
        normal = rb.sharedMaterial;
        max_jump_count=3;
        jump_time=-1;
        jump_count=max_jump_count;
        jump_max = 0.18f;
    }

    // Update is called once per frame
    void Update(){
        if(stickPressed){
        } else direction = new Vector2(0, 0);
        anima.setDirection(direction.x);
        CheckGround();

     //   if(Mathf.Abs(direction.x)>0 && isGrounded) GetComponent<Player_Sounds>().PlaySteps(true);
      //  else GetComponent<Player_Sounds>().PlaySteps(false);

        anima.setBoolAnimation("Ground", isGrounded);   
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
        move = new Vector2((direction.x)*Time.deltaTime*speed, rb.velocity.y);
    }

    void Horizontal(){
        rb.velocity = move;
        if (Mathf.Abs(move.x) > maxSpeed) {
            move = new Vector2(Mathf.Sign(move.x) * maxSpeed, rb.velocity.y);
        }

        anima.setFloatAnimation("Velocity",Mathf.Abs(rb.velocity.x));
        anima.setFloatAnimation("Direction",Mathf.Abs(direction.x));

        if((direction.x > 0 && tran.localScale.x < 0)||(direction.x < 0 && tran.localScale.x > 0)){
            Flip();
        }
    }

    void Vertical(){
        anima.setFloatAnimation("vSpeed",rb.velocity.y);
        if(jump_time>=0){    
            rb.drag=2f;
            isJumping=true;
            jump_time-=Time.deltaTime;
            anima.setBoolAnimation("Ground",false);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector3.up * jump_force, ForceMode2D.Impulse);
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
        rb.velocity+=summary;
    }

    void CheckGround(){  
    Collider2D[] hits = new Collider2D[10];
        Physics2D.OverlapCollider(checkground, new ContactFilter2D(),hits);
        foreach(Collider2D hit in hits){
            if(hit!=null && (hit.gameObject.tag=="Ground" || hit.gameObject.tag=="Trap")){
                jump_time=-1;
                isGrounded=true;
                lastcheck=true;
                isJumping=false;
                CanJump=true;
                jump_count = max_jump_count;
                slopeangle = Vector2.Angle(transform.up, hit.transform.up);
                if(slopeangle>=3f) onSlope=true;
                else onSlope = false;
                return;
            }
        }
        onSlope = false;
        isGrounded=false;
        if(!isJumping && !isGrounded && lastcheck){
            lastcheck=false;
            jump_count--;
        }
    }

    void Flip(){
        Vector3 thisScale = tran.localScale;
        thisScale.x *= -1;
        tran.localScale = thisScale;
    }

    void CustomPhysics(){
        bool directionchanged = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);
        bool needtostop = (rb.velocity.x!=0f && direction.x==0);
       // bool needtostop = (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D));
        if(isGrounded){
            if(ph.dead){
                rb.drag=8;
                return;
            } 
            rb.gravityScale = gravity;
            rb.drag=1f;
            if(directionchanged || !stickPressed || needtostop){
                //if(sources.Count==0) return;
                //rb.drag=125f;
            }
        } else {
            rb.gravityScale = gravity;
            if(ph.dead){
                rb.drag=4;
                return;
            }
            rb.drag=2f;
        }
    }

    void PreMove(){
        if(onSlope && direction.x == 0 || transform.parent!=null){
            rb.sharedMaterial = null;
            }
        else if(!isGrounded){
            rb.sharedMaterial = zero;
        }
        else rb.sharedMaterial = normal;
    }

    void PostMove(){
        if(onSlope && direction.x==0){
            //rb.velocity = new Vector2(rb.velocity.x,0);
            //rb.drag=225;
        } 
    }

    public void SetOtherSource(string name, Vector2 source, int seconds){
        source_names.Add(name);
        sources.Add(source);
        source_times.Add(seconds);
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


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "speeds"){
            Destroy(collision.gameObject);
            StartCoroutine(speedUp());
        }
    }
    IEnumerator speedUp()
    {
        speed = speed*2;
        speeds.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
        print("Speed UP");
        yield return new WaitForSeconds(9f);
        StartCoroutine(invis(speeds.GetComponent<SpriteRenderer>(), 0.02f));
        yield return new WaitForSeconds(1f);
        speed = speed /2;
        print("Speed Normal");
    }



    IEnumerator invis(SpriteRenderer spr, float time)
    
    {
        spr.color = new Color(1f,1f,1f, spr.color.a - time*2);
        yield return new WaitForSeconds(time);
        if(spr.color.a > 0){
            StartCoroutine(invis(spr, time));
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        
        if (collision.gameObject.tag == "Ladder"){
            rb.bodyType = RigidbodyType2D.Kinematic;
            transform.Translate(Vector3.up * Input.GetAxis("Vertical") * speed * 0.5f * Time.deltaTime);

            
        }
    }

        private void OnTriggerExit2D(Collider2D collision) 
    {
          if (collision.gameObject.tag == "Ladder")    
          {
              
              rb.bodyType = RigidbodyType2D.Dynamic;
          }
    }
}


