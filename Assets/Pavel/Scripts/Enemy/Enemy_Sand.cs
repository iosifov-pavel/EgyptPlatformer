using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sand : MonoBehaviour
{    
    BoxCollider2D [] Colliders=new BoxCollider2D[2];

    BoxCollider2D BC2d,trgBC2d;

    Enemy_Damage dmg;

    Player_Health ph;

    Player_Movement pm;


    bool moving = false;

    bool con=false, enough=false, vk = false;

    float y,size;

    Vector2 offs, ornl;

    float moveY;
    // Start is called before the first frame update
    void Start()
    {   
        Colliders = GetComponents<BoxCollider2D>();
        foreach(BoxCollider2D b in Colliders){
            if (b.isTrigger){}
            else BC2d = b;
        }
        //BC2d = GetComponent<BoxCollider2D>();
       // ornl=BC2d.size;
        ornl=BC2d.offset;
        moveY = (BC2d.size.y*transform.localScale.y)*1.5f;
        //trgBC2d = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(enough) return;
        if(con){
            BC2d.offset= Vector2.MoveTowards(BC2d.offset,new Vector2(BC2d.offset.x,-moveY),1*Time.deltaTime);
          // size=BC2d.size.y;
          // size*=0.999f;
          // if(size<=0.4f) enough = true;
          // float div = Mathf.Abs(BC2d.size.y - size);
          // BC2d.size= new Vector2(BC2d.size.x,size);
          // //offs=new Vector2(0,div/2);
          // offs= Vector2.Lerp(ornl,new Vector2(0,div/2),0.3f);
          // BC2d.offset-=offs;
            }
        else {
           if(ornl.y <= BC2d.offset.y) return;
          //  size=BC2d.size.y;
          //  size*=1.009f;
          //  enough = false;
          //  float div = Mathf.Abs(BC2d.size.y - size);
          //  BC2d.size= new Vector2(BC2d.size.x,size);
          //  //offs=new Vector2(0,div/2);
          //  offs= Vector2.Lerp(offs,new Vector2(0,div/2),0.3f);
          //  BC2d.offset+=offs;
        }

        

    
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player" && other.GetType()==typeof(CapsuleCollider2D))
        {
            other.gameObject.GetComponent<Player_Movement>().multylow(0.5f);
            //pm.GetComponent<Player_Movement>().multylow(0.5f);
            Debug.Log("multylow true");
        }


        if(other.gameObject.tag == "GrabCeiling")
        {
            other.gameObject.transform.parent.gameObject.GetComponent<Player_Health>().ChangeHP(-99);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player" && other.GetType()==typeof(CapsuleCollider2D))
        {
            other.gameObject.GetComponent<Player_Movement>().multylow(2f);
            //pm.GetComponent<Player_Movement>().multylow(0.5f);
            Debug.Log("multylow false");
        }
    }


    private void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.tag == "Player" && other.GetType()==typeof(CapsuleCollider2D))
        {

            con = true;
            //other.gameObject.GetComponent<Player_Movement>().multylow(0.5f);
            //pm.GetComponent<Player_Movement>().multylow(0.5f);
           // Debug.Log("multylow true");
        }
        
    }

    private void OnCollisionStay2D(Collision2D other) {

        if(other.gameObject.tag == "Player" )
        {   
            if(vk) return;
            con = true;
           
        }
        
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            if(vk) return;
           //StartCoroutine(zader());
            //other.gameObject.GetComponent<Player_Movement>().multylow(2f);
            //Debug.Log("multylow true");

        }
    }


    IEnumerator zader()
    {   
        vk=true;
        yield return new WaitForSeconds(0.3f);
        con = false;
        vk=false;
    }

}
