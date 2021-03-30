using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ParticleSystem[] fires;
    [SerializeField] BoxCollider2D fire_col;
    [SerializeField] bool constant = true;
    [SerializeField] float switchStateTime = 3f;
    [SerializeField] bool on = true;
    [SerializeField] float startDelay = 0f;
    [SerializeField] bool warning = false;
    [SerializeField] float warningTime = 0.2f;
    [SerializeField] float afterWarningTime = 1f;
    bool warningDone = false;
    bool startWarning = false;
    bool ready = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startDelay>0){
            startDelay -= Time.deltaTime;
            return;
        }
        if(constant){}
        else{
            if(!ready) return;
            if(warning && !startWarning && !on) StartCoroutine(warningFire());
            if(warning && !warningDone && !on) return;
            StartCoroutine(switchState());
        }

    }

    IEnumerator switchState(){
        on = !on;
        if(on){
            foreach(ParticleSystem ps in fires){
                var em= ps.emission;
                em.enabled = true;
            }
            StartCoroutine(colidAction(true));
        }
        else{
            foreach(ParticleSystem ps in fires){
                var em= ps.emission;
                em.enabled = false;
            }
            StartCoroutine(colidAction(false));
        }
        ready = false;
        yield return new WaitForSeconds(switchStateTime);
        ready = true;
        startWarning = false;
    }

    IEnumerator colidAction(bool cond){
        yield return new WaitForSeconds(0.5f);
        fire_col.enabled = cond;
    }

    IEnumerator warningFire(){
        warningDone = false;
        startWarning = true;
        foreach(ParticleSystem ps in fires){
                var em= ps.emission;
                em.enabled = true;
        }
        yield return new WaitForSeconds(warningTime);
        foreach(ParticleSystem ps in fires){
                var em= ps.emission;
                em.enabled = false;
        }
        yield return new WaitForSeconds(afterWarningTime);
        warningDone = true;
    }
}
