using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HP : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject heart;
    List<GameObject> hearts = new List<GameObject>();
    List<bool> states = new List<bool>();
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
            hearts.Add(h);
            states.Add(true);
        }
        hearts.Reverse();
        foreach(GameObject heart in hearts){
            int i = hearts.IndexOf(heart);
            heart.transform.SetSiblingIndex(i);
        }
        //hearts.Reverse();
    }

    // Update is called once per frame
    void Update()
    {
        if(damaged){
            foreach(GameObject heart in hearts){
                int i = hearts.IndexOf(heart);
                if(states[i]){
                    states[i]=false;
                    heart.GetComponent<Image>().sprite = heart_empty;
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
            hearts.Add(h);
            states.Add(true);
            hearts.Reverse();
        }
        if(healed){
            foreach(GameObject heart in hearts){
                int i = hearts.IndexOf(heart);
                if(!states[i]){
                    states[i]=true;
                    heart.GetComponent<Image>().sprite = heart_full;
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
