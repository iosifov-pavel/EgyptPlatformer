using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Fire : MonoBehaviour
{
    bool letPlay = false;
    //private bool playFire = true;
    //float initialTime = 2f;
    //[SerializeField] ParticleSystem FireParticle ;
    [SerializeField] ParticleSystem FireParticle ;
    // Start is called before the first frame update
  
    // Update is called once per frame
    void Update()
    {       
        
    letPlay = !letPlay;
    if(letPlay)
      {
          if(FireParticle.isPlaying)
          {
             // FireParticle.gameObject.SetActive(false);//
              FireParticle.Stop();
              var emis = FireParticle.emission;
              emis.enabled = false;
              StartCoroutine (Timer ());
              //StartCoroutine (FireOn ());
              //FireOn ();
              letPlay = false;
          }
        //  if(!FireParticle.isPlaying)
        //  {
        //      FireParticle.Play(); 
        //      FireParticle.enableEmission = true; 
        //      //FireParticle.gameObject.SetActive(true);
        //      StartCoroutine (Timer ());
        //      //FireOn ();
        //      letPlay = true;
        // }
      }else{
         // if(FireParticle.isPlaying)
         // {
         //    // FireParticle.gameObject.SetActive(false);//
         //     FireParticle.Stop();
         //     FireParticle.enableEmission = false;
         //     StartCoroutine (Timer ());
         //     //StartCoroutine (FireOn ());
         //     //FireOn ();
         //     letPlay = false;
         // }
          if(!FireParticle.isPlaying)
          {
              FireParticle.Play(); 
              var emis = FireParticle.emission;
              emis.enabled = true;
              //FireParticle.gameObject.SetActive(true);
              StartCoroutine (Timer ());
              //FireOn ();
              letPlay = true;
         }
      }

    }


    private void FireOn ()
    {
     
    if(letPlay == false)
    {
        letPlay = true;
    }
    else 
    {
        letPlay = false;
    }
    //letPlay = true;
    //yield return new WaitForSeconds(5f);
    
    // yield return new WaitForSeconds(5f);
    // FireParticle.Stop();
    }

    IEnumerator Timer ()
    {
        yield return new WaitForSeconds(10f);
    }
   // IEnumerator FireOff ()
   // {
   //    // yield return new WaitForSeconds(2f);
   //    // FireParticle.Play();
   //     yield return new WaitForSeconds(5f);
   //     FireParticle.Stop();
   // }

 // public void play() 
 // { 
 //     FireParticle.Play(); 
 //      FireParticle.enableEmission = true; 
 //      }
 // public void stop() {
 //      FireParticle.enableEmission = false;
 //       }
}
