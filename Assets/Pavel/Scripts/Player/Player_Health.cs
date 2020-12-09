using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    Vector3 lastCheckPoint;
    int last_id=0;
    public int hp;
    int MAXhp = 3;
    public bool isDamaged = false;
    public bool superman = false;
    public bool dead = false;
    Player_Animation anima;
    GameObject UI;
    GameObject Lives;
    int lives =3;
    Text lives_count;
    GameObject LooseScreen;
    GameObject DeathScreen;

    // Start is called before the first frame update
    void Start()
    {
        UI = GetComponent<Player_UIHolder>().getUI();
        Lives = UI.transform.GetChild(1).GetChild(6).GetChild(0).gameObject;
        lives_count = Lives.GetComponent<Text>();
        lives_count.text=lives.ToString();
        LooseScreen = UI.transform.GetChild(4).gameObject;
        DeathScreen = UI.transform.GetChild(5).gameObject;
        dead=false;
        hp=MAXhp;
        anima = GetComponent<Player_Animation>();
        lastCheckPoint = transform.position;
    }

    public void SetCheckPoint(Transform newcp, int id){
        if(last_id>id) return;
        lastCheckPoint=newcp.position;
        last_id = id;
    }

    public void Resurrect(){
        hp=MAXhp;
        dead=false;
        anima.setBoolAnimation("Dead",dead);
        transform.position = lastCheckPoint;
        SpriteRenderer player = transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
        player.color = Color.white;
        Time.timeScale = 1f;
        LooseScreen.SetActive(false);
    }

    // Update is called once per frame
    public bool CheckHP(){
        if(hp==MAXhp) return true;
        else return false;
    }

    public void ChangeHP(int source){
        if(superman || dead) return;
        hp+=source;
        Debug.Log("Health " + hp);
        if(hp<=0){
             Death();
             return;
        }
        StartCoroutine(damageIndication());
    }

    public void Death(){
        dead = true;
        //Game_Manager.PlayerDead();
        lives--;
        if(lives==0){
            Time.timeScale = 0f;
            DeathScreen.SetActive(true);
        }
        else {
            Time.timeScale = 0f;
            LooseScreen.SetActive(true);
        }
        lives_count.text = lives.ToString();
        StopAllCoroutines();
        anima.setBoolAnimation("Dead",dead);
        SpriteRenderer player = transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
        player.color = Color.red;
    }

    private IEnumerator damageIndication()
    {
        isDamaged = true;
        superman=true;
        SpriteRenderer player = transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
        player.color = Color.gray;
        anima.setBoolAnimation("Damaged",isDamaged);
        gameObject.layer=10;
        yield return new WaitForSeconds(0.2f);
        isDamaged=false;
        anima.setBoolAnimation("Damaged",isDamaged);
        yield return new WaitForSeconds(1.7f);
        gameObject.layer=9;
        superman=false;
        player.color = Color.white;
    }

    public void BecomeSuperman(float time){
        StartCoroutine(super(time));
    }

    IEnumerator super(float t){
        superman=true;
        yield return new WaitForSeconds(t);
        superman = false;
    }
}
