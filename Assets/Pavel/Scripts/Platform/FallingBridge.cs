using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBridge : MonoBehaviour
{
    // Start is called before the first frame update
    public List<bridgeSec> bridge = new List<bridgeSec>();
    public bool wasContact=false;
    public bool canSetContact=true;
    int contactId=-1;
    void Start()
    {
        Transform[] all =  transform.GetComponentsInChildren<Transform>();
        for(int i=0;i<all.Length;i++){
            if(i==0) continue;
            all[i].GetComponent<FallingBridge_sec>().SetId(i);
            bridgeSec b = new bridgeSec(all[i]);
            bridge.Add(b);
        }
        foreach(bridgeSec b in bridge){
            int i = bridge.IndexOf(b);
            if(i==0) b.leftN=null;
            else b.leftN = bridge[i-1];
            if(i==bridge.Count-1) b.rightN=null;
            else b.rightN = bridge[i+1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(wasContact){
            StartCoroutine(crash(bridge[contactId-1]));
        }
    }

    public void setContactId(int n){
        contactId = n;
    }

    IEnumerator crash(bridgeSec brs){
        if(brs.waAffected){}
        else{
            yield return new WaitForSeconds(0.6f);
            Action(brs);
            if(brs.leftN!=null){
                StartCoroutine(crash(brs.leftN));
            }
            if(brs.rightN!=null){
                StartCoroutine(crash(brs.rightN));
            }
            yield return new WaitForSeconds(1f);
            brs.tr.gameObject.SetActive(false);
        }
    }

    void Action(bridgeSec brs){
        brs.waAffected=true;
        brs.tr.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        brs.rb.bodyType = RigidbodyType2D.Dynamic;
    }
}


public class bridgeSec{
    public Transform tr;
    public Rigidbody2D rb;
    public bridgeSec leftN, rightN;
    public bool waAffected = false;

    public bridgeSec(Transform t){
        tr=t;
        rb = t.gameObject.GetComponent<Rigidbody2D>();
    }

}
