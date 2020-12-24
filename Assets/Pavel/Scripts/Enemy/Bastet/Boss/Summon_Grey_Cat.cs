using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon_Grey_Cat : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject grey_cat_prefab;
    int cats_alive = 0;
    [SerializeField] Vector2 c1,c2,c3;
    Boss_Health boss_Health;
    void Start()
    {
        boss_Health = GetComponent<Boss_Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(boss_Health.is_active){
            if(cats_alive==0) Summon();
        }
    }

    void Summon(){
        GameObject cat1 = Instantiate(grey_cat_prefab) as GameObject;
        cat1.transform.position = c1;
        GameObject cat2 = Instantiate(grey_cat_prefab) as GameObject;
        cat2.transform.position = c2;
        GameObject cat3 = Instantiate(grey_cat_prefab) as GameObject;
        cat3.transform.position = c3;
        cats_alive=3;
    }
}
