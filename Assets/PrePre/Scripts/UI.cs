using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Sprite h1;
    [SerializeField] Sprite h2;
    [SerializeField] Text score;
    [SerializeField] GameObject healthbar;
    [SerializeField] GameObject player;
    [SerializeField] GameObject menu;
    PlayerHealth plh;
    List<GameObject> hearts= new List<GameObject>();
    void Start()
    {
       plh = player.GetComponent<PlayerHealth>();
       GetScore("0");
       SetHealth();
       menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetScore(string s) 
    {
        score.text= s;
    }

    

    public void SetHealth() 
    {
         for(int i=0;i<plh.GetHealth();i++)
         {
             GameObject child=new GameObject();
             int width = 25;
             int height = 25;
             child.transform.position = healthbar.transform.position+new Vector3(i*width-200+width,0+50-height,0);    
             child.name = "health"+(i+1);
             child.AddComponent<Image>();
             Image childi = child.GetComponent<Image>();
             childi.sprite=h1;      
             RectTransform rt =  childi.GetComponent<RectTransform>();
             rt.sizeDelta = new Vector2(width,height); 
             hearts.Add(child);
             child.transform.SetParent(healthbar.transform);
         }
         hearts.Reverse();
    }


    public void changeHealth(int damage)
    {
        if(damage>=3 || damage>plh.GetHealth())
        {
            foreach(GameObject j in hearts)
            {
                j.GetComponent<Image>().sprite = h2;
            }
        }
        else
        {
            int j= damage;
            for(int i =0;i<damage;i++)
            {
                if(hearts[i].GetComponent<Image>().sprite == h2)
                {
                    damage++;
                    continue;
                }
                hearts[i].GetComponent<Image>().sprite = h2;
            }
        }

        //if(number<0) return;
        //hearts[number].GetComponent<Image>().sprite = h2;
    }


    public void deathscreen()
    {
        menu.SetActive(true);
    }
}
