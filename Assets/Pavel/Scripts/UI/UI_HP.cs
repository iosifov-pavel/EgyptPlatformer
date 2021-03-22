using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HP : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform[] hearts;
    List<Heart> health = new List<Heart>();
    Player_Health player_Health;
    [SerializeField] Sprite heart_full,heart_empty;
    void Start()
    {
        //dmg=0;
        player_Health = player.GetComponent<Player_Health>();
        for(int i =0;i<hearts.Length;i++){
            health.Add(new Heart(hearts[i]));
        }
    }

    // Update is called once per frame
    void Update()
    {
            int currentHealth =  player_Health.hp;
            for(int i=0;i<health.Count;i++){
                if(currentHealth>0){
                    health[i].hp_image.sprite = heart_full;
                    currentHealth--;
                }
                else{
                    health[i].hp_image.sprite = heart_empty;
                }
            }
    }


    
}
public class Heart{
    public Transform heart;
    public Image hp_image;

    public Heart(Transform h){
        heart=h;
        hp_image = h.GetComponent<Image>();
    }
}
