using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretArea : MonoBehaviour
{
    bool wasContact = false;
    // Start is called before the first frame update   
    Tilemap area;
    void Start()
    {
        area = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        if(wasContact){
            Color newalpha = area.color;
            newalpha.a = Mathf.Lerp(newalpha.a,0,0.1f);
            area.color = newalpha;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        string info = other.gameObject.tag;
        if(info == "Player" || info == "Player_Bullet"){
            //Debug.Log("Wall get Hit");
            wasContact = true;
        }
    }
}
