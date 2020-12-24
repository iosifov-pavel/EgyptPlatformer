using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps_Shoot : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] Sprite idle;
    [SerializeField] Sprite u_idle;
    [SerializeField] float speed = 6;
    Transform up_c,r_c;
    [SerializeField] bool up = false;
    SpriteRenderer spriteRenderer;
    Trap_Bullet trap_Bullet;
    private GameObject bullet;
    bool can_attack = true;
    [SerializeField] float wait = 1f;
    // Start is called before the first frame update
    void Start(){
        r_c = transform.GetChild(0);
        up_c = transform.GetChild(1);
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(up){
            spriteRenderer.sprite = u_idle;
            up_c.gameObject.SetActive(true);
            r_c.gameObject.SetActive(false);
        }
        else{
            spriteRenderer.sprite = idle;
            up_c.gameObject.SetActive(false);
            r_c.gameObject.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(can_attack){
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack(){
        //spriteRenderer.sprite = work;
        can_attack = false;
        bullet = Instantiate(prefab) as GameObject;
        trap_Bullet = bullet.GetComponent<Trap_Bullet>();
        bullet.transform.position = transform.position + new Vector3(0,0,1);
        if(up) trap_Bullet.GetDirection(transform.up, speed);
        else trap_Bullet.GetDirection(transform.right, speed);
        yield return new WaitForSeconds(wait);
        can_attack = true;
    }
}
