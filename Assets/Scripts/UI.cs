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
    PlayerHealth plh;
    List<GameObject> hearts= new List<GameObject>();
    void Start()
    {
       plh = player.GetComponent<PlayerHealth>();
       GetScore("0");
       SetHealth();
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
    }


    public void changeHealth(int number)
    {
        if(number<0) return;
        hearts[number].GetComponent<Image>().sprite = h2;
    }
}
