using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HP : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject heart;
    List<Heart> hearts = new List<Heart>();
    Player_Health player_Health;
    Vector3 orig = Vector3.zero;
    float offset;
    int max,dmg;
    [SerializeField] Sprite heart_full,heart_empty;
    static bool damaged = false, maxhpinc = false, healed=false, dead=false,create=false;
    // Start is called before the first frame update
    void Start()
    {
        dmg=0;
        player_Health = player.GetComponent<Player_Health>();
        create=true;
    }

    void CreateUI(){
        
        //hearts.Reverse();
    }

    // Update is called once per frame
    void Update()
    {
        if(damaged){
            foreach(Heart h in hearts){
                int i = hearts.IndexOf(h);
                if(h.full){
                    h.full=false;
                    h.heart.GetComponent<Image>().sprite = heart_empty;
                    break;
                }
            }
            dmg++;
            damaged=false;
        }
        if(maxhpinc || create){
            if(create) dmg=0;
            max = player_Health.MAXhp;
            offset = 180/max;
            foreach(Heart h in hearts){
                Destroy(h.heart);
            }
            hearts.Clear();
            //---------------------
            for(int i=0;i<max;i++){
                GameObject h = Instantiate(heart) as GameObject;
                h.name = string.Format("Heart{0}",i);
                h.GetComponent<Image>().sprite = heart_full;
                h.transform.parent = gameObject.transform;
                h.transform.localPosition = orig;
                orig+=new Vector3(offset,0,1);
                Heart hg =new Heart(h);
                hearts.Add(hg);
            }
            //----------------------
            hearts.Reverse();
            int curr=0;
            foreach(Heart h in hearts){
                int i = hearts.IndexOf(h);
                h.heart.transform.SetSiblingIndex(i);
                if(curr<dmg){
                    h.full=false;
                    h.heart.GetComponent<Image>().sprite = heart_empty;
                    curr++;
                }
            }
            maxhpinc=false;
            create=false;
            orig=Vector3.zero;
        }
        if(healed){
            hearts.Reverse();
            foreach(Heart h in hearts){
                int i = hearts.IndexOf(h);
                if(!h.full){
                    h.full=true;
                    h.heart.GetComponent<Image>().sprite = heart_full;
                    break;
                }
            }
            dmg--;
            healed=false;
            hearts.Reverse();
        }
        if(dead){
            foreach(Heart h in hearts){
                h.heart.GetComponent<Image>().sprite=heart_empty;
                h.full=false;
            }
            dead = false;
        }
    }

    public static void Damaged(){
        damaged = true;
    }

    public static void MaxHPInc(){
        maxhpinc = true;
    }

    public static void Heal(){
        healed = true;
    }

    public static void Dead(){
        dead = true;
    }

    public static void Recreate(){
        create=true;
    }

    
}
public class Heart{
    public bool full;
    public GameObject heart;

    public Heart(GameObject h){
        heart=h;
        full=true;
    }
}
