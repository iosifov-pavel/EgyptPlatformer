using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Health : MonoBehaviour
{
    Vector3 lastCheckPoint;
    int last_id=0;
    public int hp;
    public int MAXhp = 3;
    public bool isDamaged = false;
    public bool superman = false;
    public bool dead = false;
    Player_Animation anima;
    Player_Movement pm;
    GameObject UI;
    GameObject Lives;
    //public int lives =3;
    Text lives_count;
    GameObject LooseScreen;
    GameObject DeathScreen;
    GameObject Playing_UI;
    Manager_Level LM;
    Reset_Playing_UI reset_Playing_UI;

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        UI = GetComponent<Player_InfoHolder>().getUI();
        LM = GetComponent<Player_InfoHolder>().getLM();
        Lives = UI.transform.GetChild(1).GetChild(6).GetChild(0).gameObject;
        lives_count = Lives.GetComponent<Text>();
        lives_count.text=LM.manager_Game.game_info.Lives.ToString();
        LooseScreen = UI.transform.GetChild(4).gameObject;
        DeathScreen = UI.transform.GetChild(5).gameObject;
        Playing_UI = UI.transform.GetChild(1).gameObject;
        reset_Playing_UI = Playing_UI.GetComponent<Reset_Playing_UI>();
        dead=false;
        hp=MAXhp;
        anima = GetComponent<Player_Animation>();
        pm = GetComponent<Player_Movement>();
        lastCheckPoint = transform.position;
    }

    public void SetCheckPoint(Transform newcp, int id){
        if(last_id>id) return;
        lastCheckPoint=newcp.position;
        last_id = id;
    }

    public void Resurrect(){
        pm.BlockMovement(0.4f);
        pm.gameObject.GetComponent<Rigidbody2D>().velocity=Vector2.zero;
        hp=MAXhp;
        UI_HP.Recreate();
        dead=false;
        StopAllCoroutines();
        anima.setBoolAnimation("Dead",false);
        transform.position = lastCheckPoint;
        SpriteRenderer player = transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
        player.color = Color.white;
        Time.timeScale = 1f;
        Playing_UI.SetActive(true);
        reset_Playing_UI.ResetInput();
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
        UI_HP.Damaged();
        Debug.Log("Health " + hp);
        if(hp<=0){
             Death();
             return;
        }
        StartCoroutine(damageIndication());
    }

    public void Heal(){
        hp++;
        UI_HP.Heal();
    }

    public void Death(){
        dead = true;
        StopAllCoroutines();
        anima.setBoolAnimation("Dead",dead);
        SpriteRenderer player = transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
        player.color = Color.red;
        UI_HP.Dead();
        LM.manager_Game.game_info.Lives--;
        lives_count.text = LM.manager_Game.game_info.Lives.ToString();
        Manager_Level.PlayerIsDead();
        LM.Save();
        StartCoroutine(Dekay());
    }

    IEnumerator Dekay(){
        yield return new WaitForSeconds(1);
        if(LM.manager_Game.game_info.Lives==0){
            Time.timeScale = 0f;
            reset_Playing_UI.ResetInput();
            Playing_UI.SetActive(false);
            LM.manager_Game.game_info.Lives=3;
            DeathScreen.SetActive(true);
        }
        else {
            Time.timeScale = 0f;
            reset_Playing_UI.ResetInput();
            Playing_UI.SetActive(false);
            LooseScreen.SetActive(true);
        }
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

    public void MaxHPPlus(){
        MAXhp++;
        hp++;
        UI_HP.MaxHPInc();
    }
}


