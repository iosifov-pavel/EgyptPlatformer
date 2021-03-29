using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    // Start is called before the first frame update
    Enemy_Ray_Eyes eyes;
    EnemyGroundWalking egp2;
    Enemy_Health enemy_Health;
    Enemy_Damage enemy_Damage;
    Animator skelet_anim;
    [SerializeField] bool axe=false,spear=false,shield=false;
    GameObject axe_g, spear_g, shield_g;
    [SerializeField] BoxCollider2D axe_c, spear_c, skeleton_c, shield_c;
    int dir;
    Transform player;
    float distance=100;
    [SerializeField] float melee = 2;
    [SerializeField] float attackDelay = 2f;
    [SerializeField] bool unbreakableShield = true;
    bool canAttack = true;
    bool canWalking = true;
    bool canBeDamaged = true;
    [SerializeField] AudioClip spearSound, axeSound;
    AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
        enemy_Health = GetComponent<Enemy_Health>();
        enemy_Damage = GetComponent<Enemy_Damage>();
        skelet_anim = GetComponent<Animator>();
        eyes = GetComponent<Enemy_Ray_Eyes>();
        egp2 = transform.parent.GetComponent<EnemyGroundWalking>();
        axe_g = transform.GetChild(2).gameObject;
        spear_g = transform.GetChild(3).gameObject;
        shield_g = transform.GetChild(4).gameObject;
        if(axe) axe_g.SetActive(true);
        if(spear) spear_g.SetActive(true);
        if(shield){
            shield_g.SetActive(true);
            if(unbreakableShield) shield_g.GetComponent<Shield_Block>().canBreak = false;
            else shield_g.GetComponent<Shield_Block>().canBreak = true;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy_Health.dead) return;
        if(enemy_Health.is_damaged && canBeDamaged){       
            skelet_anim.SetBool("damage",true);
            canBeDamaged = false;
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
        canBeDamaged = false;
        skelet_anim.SetTrigger("throw");
        StartCoroutine(SpearSoundPlay());
        StartCoroutine(delay());
    }

    IEnumerator SpearSoundPlay(){
        yield return new WaitForSeconds(0.3f);
        source.PlayOneShot(spearSound);
    }

    void PunchAxe(){
        if(distance>1) return;
        canBeDamaged = false;
        axe_c.enabled = true;
        skelet_anim.SetTrigger("attack");
        source.PlayOneShot(axeSound);
        StartCoroutine(delay());
    }

    public void canWalk(){
        if(!enemy_Health.dead)egp2.WalkAgain();
        if(axe) axe_c.enabled = false;
    }
    
    public void cantWalk(){
        egp2.StopWalk();
    }

    public void damageFalse(){
        skelet_anim.SetBool("damage",false);
        StartCoroutine(delayFalseDamage());
    }
    IEnumerator delayFalseDamage(){
        yield return new WaitForSeconds(0.3f);
        canBeDamaged = true;
    }
    
    public void Throw(){
        if(enemy_Health.dead) return;
        GameObject new_spear = Instantiate(spear_g, spear_g.transform.position, spear_g.transform.rotation);
        new_spear.GetComponent<Spear>().Fly(dir);
    }

    IEnumerator delay(){
        canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
        canBeDamaged = true;
    }


    public void DeathS2(){
        egp2.StopWalk();
        axe_c.enabled = false;
        spear_c.enabled = false;
        //skeleton_c.enabled = false;
        //enemy_Damage.enabled = false;
        gameObject.layer =22;
        shield_c.enabled = false;
        //this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>().enabled=false;
    }
    public void DeathS(){
        Destroy(gameObject, 1f);
    }
}
