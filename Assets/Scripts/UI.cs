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
    void Start()
    {
       plh = player.GetComponent<PlayerHealth>();
       GetScore("0");
       SetUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetScore(string s) 
    {
        score.text= s;
    }

    

    public void SetUI() 
    {
         for(int i=0;i<plh.GetHealth();i++)
         {
             GameObject child=new GameObject();
             child.transform.position = healthbar.transform.position+new Vector3(i*100-100,0,0);
             child.name = "health"+i;
             RectTransform rt =  child.GetComponent<RectTransform>();
             child.AddComponent<Image>();
             Image childi = child.GetComponent<Image>();
             childi.sprite=h1;
             child.transform.SetParent(healthbar.transform);
             //healthbar.AddComponent<Image>();
         }
    }
}
