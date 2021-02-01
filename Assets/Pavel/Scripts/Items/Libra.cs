using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libra : MonoBehaviour
{
    // Start is called before the first frame update
    Quaternion original, childOriginal;
    Transform stick, l,r;
    BoxCollider2D left,right;
    public bool leftContact=false, rightContact=false;
    Transform playerT = null;
    [SerializeField]LayerMask player;
    void Start()
    {
        stick = transform.GetChild(0);
        l = stick.GetChild(0);
        r = stick.GetChild(1);
        original = stick.rotation;
        childOriginal = r.rotation;
        left = l.GetComponent<BoxCollider2D>();
        right = r.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        checkContacts();
        if(!leftContact && !rightContact){
            stick.rotation = Quaternion.Lerp(stick.rotation,original,0.01f);
            l.rotation = childOriginal; 
            r.rotation = childOriginal; 
        }
        else if(leftContact){
            l.rotation = childOriginal; 
            r.rotation = childOriginal;
            
            Quaternion newRotation = Quaternion.Euler(0,0,stick.rotation.z+60);
            stick.rotation = Quaternion.Lerp(stick.rotation,newRotation,0.005f);
        }
        else if(rightContact){
            l.rotation = childOriginal; 
            r.rotation = childOriginal;
            Quaternion newRotation = Quaternion.Euler(0,0,stick.rotation.z-60);
            stick.rotation = Quaternion.Lerp(stick.rotation,newRotation,0.005f);
        }
    }

    void checkContacts(){
        List<ContactPoint2D> left_c = new List<ContactPoint2D>();
        List<ContactPoint2D> right_c = new List<ContactPoint2D>();
        ContactFilter2D cf2d = new ContactFilter2D();
        cf2d.layerMask = player;
        left.GetContacts(cf2d,left_c);
        right.GetContacts(cf2d,right_c);
        if(playerT!=null){
            //playerT.rotation = Quaternion.Euler(0,0,0);
            playerT.parent=null;
        } 
        leftContact = false;
        rightContact = false;
        foreach(ContactPoint2D point in left_c){
            if(point.collider!=null){
                leftContact=true;
                //playerT = point.collider.gameObject.transform;
                //playerT.parent = l;
                break;
            }
        }
        foreach(ContactPoint2D point in right_c){
            if(point.collider!=null){
                rightContact=true;
                //playerT = point.collider.gameObject.transform;
                //playerT.parent = r;
                break;
            }
        }
    }
}
