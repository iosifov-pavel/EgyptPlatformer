using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<SpriteRenderer> sections;
    [SerializeField] List<Sprite> sprites;
    [SerializeField] List<Transform> childs;
    Vector2[] original = new Vector2[4];
    [SerializeField] float timeToNextStage = 0.8f;
    [SerializeField] float returnTime = 1f;
    int currentStage = 1;
    bool wasContact = false;
    BoxCollider2D[] boxes;
    void Start()
    {
        int i =0;
        foreach(Transform child in childs){
            original[i] = child.position;
            i++;
        }
        boxes =  GetComponents<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(wasContact){
            foreach(Transform child in childs){
                int i = childs.IndexOf(child);
                child.position = original[i] + (Vector2)Random.insideUnitSphere*0.03f*currentStage;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(wasContact) return;
        if(other.gameObject.tag == "GroundCheck"){
            wasContact = true;
            StartCoroutine(startDestroy());
        }
    }

    IEnumerator startDestroy(){
        if(currentStage<4){
            foreach(SpriteRenderer sr in sections){
                sr.sprite = sprites[currentStage];
            }
            currentStage++;
            yield return new WaitForSeconds(timeToNextStage);
            StartCoroutine(startDestroy());
        }
        else if(currentStage==4){
            currentStage = 5;
            wasContact = false;
            foreach(BoxCollider2D box in boxes){
                box.enabled=false;
            }
            foreach(SpriteRenderer sr in sections){
                Color new_c = sr.color;
                new_c.a = 0;
                sr.color = new_c;
            }
            yield return new WaitForSeconds(returnTime);
            StartCoroutine(startDestroy());
        }
        else{
            currentStage = 0;
            foreach(SpriteRenderer sr in sections){
                sr.sprite = sprites[currentStage];
            }
            foreach(BoxCollider2D box in boxes){
                box.enabled=true;
            }
            foreach(Transform child in childs){
                int i = childs.IndexOf(child);
                child.position = original[i];
            }
            foreach(SpriteRenderer sr in sections){
                Color new_c = sr.color;
                new_c.a = 1;
                sr.color = new_c;
            }
            currentStage = 1;
        }
    }
}
