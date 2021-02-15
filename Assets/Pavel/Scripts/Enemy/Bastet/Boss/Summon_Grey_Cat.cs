using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon_Grey_Cat : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject grey_cat_prefab;
    int cats_alive = 0;
    bool cats_was_summoned = false;
    [SerializeField] float time_before_new_summon = 2f;
    bool canSummon = true;
    [SerializeField] Vector2 c1,c2,c3;
    Boss_Health boss_Health;
    List<gCat> cats;
    void Start()
    {
        boss_Health = GetComponent<Boss_Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(boss_Health.is_active){
            if(cats_was_summoned) CheckCats();
            if(cats_alive<=0 && canSummon) Summon();
        }
    }

    void CheckCats(){
        int live_cats =3;
        foreach(gCat cat in cats){
            if(cat.tr==null){
                live_cats--;
            } 
        }
        cats_alive = live_cats;
        if(cats_alive<=0){
            cats_was_summoned=false;
            StartCoroutine(delay());
        } 
    }

    IEnumerator delay(){
        canSummon = false;
        yield return new WaitForSeconds(time_before_new_summon);
        canSummon = true;
    }

    void Summon(){
        cats = new List<gCat>();
        GameObject cat1 = Instantiate(grey_cat_prefab) as GameObject;
        cat1.transform.position = c1;
        cats.Add(new gCat(cat1));
        GameObject cat2 = Instantiate(grey_cat_prefab) as GameObject;
        cat2.transform.position = c2;
        cats.Add(new gCat(cat2));
        GameObject cat3 = Instantiate(grey_cat_prefab) as GameObject;
        cat3.transform.position = c3;
        cats.Add(new gCat(cat3));
        cats_alive=3;
        cats_was_summoned = true;
    }

    class gCat{
        public bool alive = false;
        public Transform tr;

        public gCat(GameObject g){
            alive = true;
            tr = g.transform;
        }
    }
}
