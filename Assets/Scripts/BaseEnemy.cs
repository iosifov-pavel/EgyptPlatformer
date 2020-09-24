using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    int health = 1;
    int dir = 1;
    float speed = 2.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        RaycastHit2D[] check = CheckRay();
        if(check[0].collider==null) changedir(dir=1);
        else if(check[1].collider==null) changedir(dir=-1);
        Vector3 move = new Vector3((dir*0.5f),0,0);
        transform.Translate(move*Time.deltaTime*speed);
    }

    public void changedir(int frw)
    {       
        Vector3 thisScale = transform.localScale;
        thisScale.x *= -1;
        transform.localScale = thisScale;
    }

    RaycastHit2D[] CheckRay()
    {    
        Vector3 check1 = new Vector3(transform.position.x-0.3f,transform.position.y-0.1f,transform.position.z);
        Vector3 check2 = new Vector3(transform.position.x+0.3f,transform.position.y-0.1f,transform.position.z);
        Vector3 checkTo = Vector3.down * 0.15f;

        Debug.DrawRay(check1,checkTo,Color.yellow);
        Debug.DrawRay(check2,checkTo,Color.red);

        RaycastHit2D[] hit= new RaycastHit2D[2];
        hit[0] =  Physics2D.Raycast(check1,checkTo, 0.15f);
        hit[1] =  Physics2D.Raycast(check2,checkTo, 0.15f);
        return hit;   
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.name=="Player")
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x,0);
            rb.AddForce(Vector3.up*9,ForceMode2D.Impulse);
            StartCoroutine(getdamage());
        }
    }

    private IEnumerator getdamage()
    {
        dir=0;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.green;
        List<BoxCollider2D> bc=new List<BoxCollider2D>(GetComponents<BoxCollider2D>());
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
