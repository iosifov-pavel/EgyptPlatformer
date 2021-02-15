using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow_Attack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform player;
    [SerializeField] GameObject arrow_p;
    [SerializeField] float timeBetweenShoots = 1f;
    bool canAttack = true;
    Boss_Health boss_Health;
    
    void Start()
    {
        boss_Health = GetComponent<Boss_Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(boss_Health.is_active && canAttack){
            Shoot();
        }
    }

    void Shoot(){
        GameObject arrow = Instantiate(arrow_p) as GameObject;
        arrow.transform.position = transform.position;
        Vector2 pl_pos = new Vector2(player.transform.position.x,player.transform.position.y);
        Vector2 pos = new Vector2(transform.position.x,transform.position.y);
        arrow.transform.right = pl_pos-pos;
        arrow.GetComponent<Rigidbody2D>().velocity = arrow.transform.right * 8;
        StartCoroutine(delayAttack());
    }

    IEnumerator delayAttack(){
        canAttack = false;
        yield return new WaitForSeconds(timeBetweenShoots);
        canAttack = true;
    }
}
