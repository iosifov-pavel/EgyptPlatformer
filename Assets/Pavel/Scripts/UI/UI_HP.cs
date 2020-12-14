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
    int last_hp,max,curr;
    [SerializeField] Sprite heart_full,heart_empty;
    static bool damaged = false, maxhpinc = false, healed=false;
    // Start is called before the first frame update
    void Start()
    {
        player_Health = player.GetComponent<Player_Health>();
        last_hp=player_Health.hp;
        curr=last_hp;
        max = player_Health.MAXhp;
        offset = 180/max;
        CreateUI();
    }

    void CreateUI(){
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
        hearts.Reverse();
        foreach(Heart h in hearts){
            int i = hearts.IndexOf(h);
            h.heart.transform.SetSiblingIndex(i);
        }
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
            damaged=false;
        }
        if(maxhpinc){
            hearts.Reverse();
            GameObject h = Instantiate(heart) as GameObject;
            h.name = string.Format("Heart{0}",hearts.Count+1);
            h.GetComponent<Image>().sprite = heart_full;
            h.transform.parent = gameObject.transform;
            h.transform.localPosition = orig;
            orig+=new Vector3(offset,0,1);
            Heart hg =new Heart(h);
            hearts.Add(hg);
            hearts.Reverse();
            maxhpinc=false;
        }
        if(healed){
            foreach(Heart h in hearts){
                int i = hearts.IndexOf(h);
                if(!h.full){
                    h.full=true;
                    h.heart.GetComponent<Image>().sprite = heart_full;
                    break;
                }
            }
            healed=false;
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

    
}
public class Heart{
    public bool full;
    public GameObject heart;

    public Heart(GameObject h){
        heart=h;
        full=true;
    }
}
