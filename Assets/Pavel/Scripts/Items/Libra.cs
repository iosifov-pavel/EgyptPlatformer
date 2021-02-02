using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libra : MonoBehaviour
{
    // Start is called before the first frame update
    Quaternion original, LOriginal, ROriginal;
    Transform stick, L,R;
    BoxCollider2D leftB,rightB;
    public bool leftContact=false, rightContact=false;
    Transform playerT = null;
    Rigidbody2D stick_rb;
    [SerializeField]LayerMask player;
    void Start()
    {
        stick = transform.GetChild(0);
        stick_rb = stick.gameObject.GetComponent<Rigidbody2D>();
        L = transform.GetChild(1);
        R = transform.GetChild(2);
        original = stick.rotation;
        LOriginal = L.rotation;
        ROriginal = R.rotation;
        leftB = L.GetComponent<BoxCollider2D>();
        rightB = R.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        checkContacts();
        if(!leftContact && !rightContact){
            stick_rb.bodyType = RigidbodyType2D.Kinematic;
            stick_rb.angularVelocity = 0;
            stick.rotation = Quaternion.Lerp(stick.rotation,original,0.033f);
        }
        else if(leftContact || rightContact){
            stick_rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    void checkContacts(){
        List<ContactPoint2D> left_c = new List<ContactPoint2D>();
        List<ContactPoint2D> right_c = new List<ContactPoint2D>();
        ContactFilter2D cf2d = new ContactFilter2D();
        cf2d.useLayerMask=true;
        cf2d.layerMask = player;
        leftB.GetContacts(cf2d,left_c);
        rightB.GetContacts(cf2d,right_c);
        leftContact = false;
        rightContact = false;
        foreach(ContactPoint2D point in left_c){
            if(point.collider!=null){
                leftContact=true;
                playerT = point.collider.gameObject.transform;
                playerT.parent = L;
                break;
            }
        }
        foreach(ContactPoint2D point in right_c){
            if(point.collider!=null){
                rightContact=true;
                playerT = point.collider.gameObject.transform;
                playerT.parent = R;
                break;
            }
        }
        if(!leftContact && !rightContact){
            if(playerT!=null){
                playerT.rotation = Quaternion.Euler(0,0,0);
                playerT.parent=null;
            } 
        }
    }
}
