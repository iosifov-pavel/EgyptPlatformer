using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public bool active = false;
    SpriteRenderer sprite;
    [SerializeField] int id;
    [SerializeField] Sprite active_sprite;
    [SerializeField] Sprite inactive_sprite;
    Transform background;
    void Start()
    {
        background = transform.GetChild(0);
        sprite = background.gameObject.GetComponent<SpriteRenderer>();
        sprite.sprite = inactive_sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(active) return;
        if(other.gameObject.tag=="Player" && other.gameObject.layer==9 || other.gameObject.layer==10){
            active=true;
            sprite.sprite = active_sprite;
            other.gameObject.GetComponent<Player_Health>().SetCheckPoint(transform,id);
        }
    }

    public int getID(){
        return id;
    }
}
