using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Fading : MonoBehaviour
{

    private float fade_time = 1.5f;
    private float reverse = 1.5f;
    bool canfade = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            if(canfade) StartCoroutine(Fade());
        }
    }

   private IEnumerator Fade(){
       canfade=false;
       var render = GetComponent<MeshRenderer>();
       render.material.color = Color.green;
       yield return new WaitForSeconds(fade_time);
       var colider = gameObject.GetComponent<BoxCollider2D>();
       render.enabled=false;
       colider.enabled = false;
       yield return new WaitForSeconds(reverse);
       render.enabled=true;
       colider.enabled = true;
       render.material.color = Color.red;
       canfade=true;
   } 


}
