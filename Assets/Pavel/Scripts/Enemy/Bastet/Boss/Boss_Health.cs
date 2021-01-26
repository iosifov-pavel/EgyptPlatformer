using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] int boss_health;
    void Start()
    {
        finish = lvl_complete.GetComponent<Finish>();
        enemy_Health = GetComponent<Enemy_Health>();
        boss_health = enemy_Health.health;
    }

    // Update is called once per frame
    void Update()
    {
        if(is_active){
            boss_health = enemy_Health.health;
            if(boss_health<=25) curr_stage = 2;
            if(boss_health<=0) finish.FinishLevel(player);
        }
    }
}
