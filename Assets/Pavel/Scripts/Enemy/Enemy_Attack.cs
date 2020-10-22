using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private GameObject bullet;
    public bool isTrigered = false;
    private float time = 0.8f;
    private bool canAttack = true;
    
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Trigered",isTrigered);
        if(isTrigered && canAttack){
            StartCoroutine(Attack());
        }
    }
    
    IEnumerator Attack(){
        canAttack = false;
        bullet = Instantiate(bulletPrefab) as GameObject;
        bullet.transform.position = transform.position;
        Transform player = transform.GetChild(0).GetComponent<Enemy_See_You>().GetPlayer();
        bullet.GetComponent<Enemy_Bullet>().GetPlayerPos(player);
        yield return new WaitForSeconds(time);
        canAttack = true;
    }
}
