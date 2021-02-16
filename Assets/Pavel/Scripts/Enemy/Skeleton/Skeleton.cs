using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    // Start is called before the first frame update
    Enemy_Ray_Eyes eyes;
    Enemy_Ground_Patroling1 egp;
    Enemy_Health enemy_Health;
    Animator skelet_anim;
    [SerializeField] bool axe=false,spear=false,shield=false;
    GameObject axe_g, spear_g, shield_g;
    [SerializeField] BoxCollider2D axe_c, spear_c, skeleton_c;
    int dir;
    Transform player;
    float distance=100;
    [SerializeField] float melee = 2;
    [SerializeField] float attackDelay = 2f;
    bool canAttack = true;
    bool canWalking = true;
    void Start()
    {
        enemy_Health = GetComponent<Enemy_Health>();
        skelet_anim = GetComponent<Animator>();
        eyes = GetComponent<Enemy_Ray_Eyes>();
        egp = GetComponent<Enemy_Ground_Patroling1>();
        axe_g = transform.GetChild(2).gameObject;
        spear_g = transform.GetChild(3).gameObject;
        shield_g = transform.GetChild(4).gameObject;
        if(axe) axe_g.SetActive(true);
        if(spear) spear_g.SetActive(true);
        if(shield) shield_g.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy_Health.is_damaged){
            //skelet_anim.SetTrigger("damage");
        }
        dir = (int)Mathf.Sign(transform.localScale.x)*1;
        if(eyes.Check()!=null){
            player = eyes.Check();
            distance=Mathf.Abs(transform.position.x-player.position.x);
        }
        else return;
        if(distance>=melee && spear && canAttack) ThrowSpear();
        else if(axe && canAttack) PunchAxe();
        else{}
    }

    void ThrowSpear(){
        skelet_anim.SetTrigger("throw");
        StartCoroutine(delay());
    }

    void PunchAxe(){
        if(distance>1) return;
        skelet_anim.SetTrigger("attack");
        StartCoroutine(delay());
    }

    public void canWalk(){
        //canWalking=!canWalking;
        //egp.enabled = canWalking;
        egp.enabled = true;
    }
    
    public void cantWalk(){
        egp.enabled = false;
    }
    
    public void Throw(){
        GameObject new_spear = Instantiate(spear_g, spear_g.transform.position, spear_g.transform.rotation);
        new_spear.GetComponent<Spear>().Fly(dir);
    }

    IEnumerator delay(){
        canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }

    public void DeathS(){
        axe_c.enabled = false;
        spear_c.enabled = false;
        skeleton_c.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        Destroy(gameObject, 1f);
    }
}
