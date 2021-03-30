using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mosquite_Attack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform body;
    [SerializeField] GameObject spikePrefab;
    [SerializeField] float spikeSpeed = 2f;
    [SerializeField] float timeBetweenAttacks = 2f;
    [SerializeField] float spikeLifetime = 4f;
    [SerializeField] EnemyCircleEyes enemyCircleEyes;
    bool canAttack = true;
    float prev_x;
    Transform target=null;
    void Start()
    {
        prev_x = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(prev_x<=transform.position.x){
            Vector3 new_sc = body.transform.localScale;
            new_sc.x =Mathf.Abs(new_sc.x);
            body.transform.localScale = new_sc;
        }
        else{
            Vector3 new_sc = body.transform.localScale;
            new_sc.x =-Mathf.Abs(new_sc.x);
            body.transform.localScale = new_sc;
        }
        prev_x = transform.position.x;
        target = enemyCircleEyes.GetPlayer();
        if(target!=null && canAttack){
            Attack(target);
        }
    }

    void Attack(Transform player){
        GameObject new_spike = Instantiate(spikePrefab);
        new_spike.transform.position = transform.position;
        //new_spike.SetActive(false);
        new_spike.transform.right = player.position - new_spike.transform.position;
        //new_spike.SetActive(true);
        new_spike.GetComponent<Rigidbody2D>().velocity = new_spike.transform.right * spikeSpeed;
        StartCoroutine(delay());
        StartCoroutine(destroySpike(new_spike));
    }

    IEnumerator delay(){
        canAttack = false;
        yield return new WaitForSeconds(timeBetweenAttacks);
        canAttack = true;
    }

    IEnumerator destroySpike(GameObject spike){
        yield return new WaitForSeconds(spikeLifetime);
        try{
            Destroy(spike);
        }
        catch{}
    }

    //private void OnTriggerEnter2D(Collider2D other) {
    //    if(!canAttack) return;
    //    if(other.gameObject.tag=="Player"){
    //        Attack(other.transform);
    //    }
    //}
//
    //private void OnTriggerStay2D(Collider2D other) {
    //    if(!canAttack) return;
    //    if(other.gameObject.tag=="Player"){
    //        Attack(other.transform);
    //    }
    //}
}
