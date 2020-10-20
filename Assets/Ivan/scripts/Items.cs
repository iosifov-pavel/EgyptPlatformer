using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{

    int coins = 0;
    public float speed;

    bool canHit = true;
    bool isHit = false;

    public GameObject infinity, speeds;
    int buffCount = 0;
    int curHP;
    int maxHP = 3;
    //public virtual int Next (int minValue, int maxValue);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


     private void OnTriggerEnter2D(Collider2D collision)
     {
           // Random rnd = new Random();
          //  int luck = rnd.Next(1,9);


        if (collision.gameObject.tag == "Coin"){
            Destroy(collision.gameObject);
            coins ++;
            print("Kol-vo coin =" + coins);
        }

        if (collision.gameObject.tag == "Heart"){
            Destroy(collision.gameObject);
            RecountHP(1);
        }
      // if (collision.gameObject.tag == "potion")
      // {
      //     Destroy(collision.gameObject);
      //     if( luck >= 1 && luck <= 3)
      //     {
      //     RecountHP(-1);
      //     }
      //     else if (luck >= 4 && luck <= 6)
      //     {
      //     StartCoroutine(noHit());
      //     }
      //     else (luck >= 7 && luck <= 9);
      //     {
      //     StartCoroutine(speedUp());
      //     }
      //     
      // }

        if (collision.gameObject.tag == "infinity"){
            Destroy(collision.gameObject);
            StartCoroutine(noHit());
        }
        
        if (collision.gameObject.tag == "speeds"){
            Destroy(collision.gameObject);
            StartCoroutine(speedUp());
        }
}

    public void RecountHP(int deltaHP)
    {
        
        if(deltaHP < 0 && canHit) {
            curHP = curHP + deltaHP;
            StopCoroutine(OnHit());
            canHit = false;
            isHit = true;
        StartCoroutine(OnHit());

        }
        else if (curHP > maxHP) {
            curHP = curHP + deltaHP;
            curHP = maxHP;

        }

        print (curHP);
        if(curHP <= 0)
        {
        GetComponent<CapsuleCollider2D>().enabled = false;
        Invoke("Lose", 2f);
        }
    }

    IEnumerator noHit (){
        buffCount++;
        infinity.SetActive(true);
        CheckGems(infinity);
        canHit = false;
        infinity.GetComponent<SpriteRenderer>().color = new Color (1f,1f,1f,1f);
        print("DMG taken off");
        yield return new WaitForSeconds(5f);
        canHit = true;
        StartCoroutine(invis(infinity.GetComponent<SpriteRenderer>(), 0.02f));
        yield return new WaitForSeconds(1f);
        print("DMG taken on");

        buffCount--;
        infinity.SetActive(false);
        CheckGems(speeds);
    }

    IEnumerator speedUp()
    {
        buffCount++;
        speeds.SetActive(true);
        CheckGems(speeds);

        speed = speed*2;
        speeds.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
        print("Speed UP");
        yield return new WaitForSeconds(9f);
        StartCoroutine(invis(speeds.GetComponent<SpriteRenderer>(), 0.02f));
        yield return new WaitForSeconds(1f);
        speed = speed /2;
        print("Speed Normal");

        buffCount--;
        speeds.SetActive(false);
        CheckGems(infinity);

    }


    void CheckGems(GameObject obj){
        if(buffCount == 1)
            obj.transform.localPosition = new Vector3(0f,0.6f, transform.localPosition.z);
            else if (buffCount == 2)
            {
                infinity.transform.localPosition = new Vector3(-0.5f,0.5f, transform.localPosition.z);
                speeds.transform.localPosition = new Vector3(0.5f,0.5f, transform.localPosition.z);
            }

    }

    IEnumerator OnHit()
    {
        if(isHit)
        GetComponent<SpriteRenderer>().color = new Color(1f,GetComponent<SpriteRenderer>().color.g - 0.04F ,GetComponent<SpriteRenderer>().color.b - 0.04F);
        else
        GetComponent<SpriteRenderer>().color = new Color(1f,GetComponent<SpriteRenderer>().color.g + 0.04F ,GetComponent<SpriteRenderer>().color.b + 0.04F);
        
        if(GetComponent<SpriteRenderer>().color.g == 0.1f){

        
        StopCoroutine(OnHit());
        canHit = true;
        }
        if(GetComponent<SpriteRenderer>().color.g <= 0.1f)
            isHit = false;
        yield return new WaitForSeconds(0.01F);
        StartCoroutine(OnHit());
    }

    IEnumerator invis(SpriteRenderer spr, float time)
    
    {
        spr.color = new Color(1f,1f,1f, spr.color.a - time*2);
        yield return new WaitForSeconds(time);
        if(spr.color.a > 0){
            StartCoroutine(invis(spr, time));
        }
    }
}
