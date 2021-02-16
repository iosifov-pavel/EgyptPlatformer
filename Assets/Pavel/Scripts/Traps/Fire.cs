using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ParticleSystem[] fires;
    [SerializeField] BoxCollider2D fire_col;
    public bool on = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    IEnumerator colidAction(bool cond){
        yield return new WaitForSeconds(0.5f);
        fire_col.enabled = cond;
    }
}
