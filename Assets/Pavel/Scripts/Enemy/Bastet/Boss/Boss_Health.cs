using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Health : MonoBehaviour
{
    [SerializeField] GameObject lvl_complete;
    [SerializeField] Transform player;
    Finish finish;
    // Start is called before the first frame update
    public bool is_active = false;
    public int stages = 2;
    public int curr_stage=1;
    Enemy_Health enemy_Health;
    int boss_health;
    [SerializeField] GameObject boss_hp;
    Slider health_slider;
    void Start()
    {
        finish = lvl_complete.GetComponent<Finish>();
        enemy_Health = GetComponent<Enemy_Health>();
        boss_health = enemy_Health.health;
        health_slider = boss_hp.transform.GetChild(0).GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(is_active){
            boss_hp.SetActive(true);
            int health = enemy_Health.health;
            float percent = health*100/boss_health;
            health_slider.value = percent;
            if(health<=25) curr_stage = 2;
            if(health<=0) {
                FinishLevel();
            }
        }
    }

    public void FinishLevel(){
        finish.FinishLevel(player);
        is_active=false;
    }
}
