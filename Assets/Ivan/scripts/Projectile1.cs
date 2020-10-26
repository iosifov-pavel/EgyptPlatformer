using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile1 : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float dir = 1;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile",lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }


    void DestroyProjectile() 
    {
       Destroy(gameObject);
    }


    public void GetPosision (Vector3 pstn)
    {
      Vector3 pos = transform.localScale;
      pos.x *= Mathf.Sign(pstn.x);
      dir = 1*Mathf.Sign(pstn.x);
      transform.localScale = pos;
    }
}
