using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateByPlayerTouch : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool ObjectNotScript = false;
    [SerializeField] GameObject robject;
    [SerializeField] MonoBehaviour script;
    [SerializeField] bool Reseting = false;
    [SerializeField] float resetTime = 10;

    private void Awake() {
        if(ObjectNotScript) robject.SetActive(false);
        else script.enabled=false;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            if(ObjectNotScript) robject.SetActive(true);
            else script.enabled=true;
            StopAllCoroutines();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(!Reseting) return;
        if(other.gameObject.tag=="Player"){
            StartCoroutine(ResetObject());
        }
    }

    IEnumerator ResetObject(){
        yield return new WaitForSeconds(resetTime);
        if(ObjectNotScript) robject.SetActive(false);
        else script.enabled=false;
    }

}
