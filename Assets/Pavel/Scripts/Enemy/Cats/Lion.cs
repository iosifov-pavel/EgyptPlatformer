using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lion : MonoBehaviour
{
    Enemy_Ray_Eyes eyes;
    [SerializeField] GameObject Fireball_Prefab;
    [SerializeField] float fire_speed = 6f;
    int dir = 1;
    [SerializeField] float fire_lifetime = 5f;
    [SerializeField] float delayt = 1f;
    float distance;
    Transform player;
    bool can_attack = true;
    Vector3 point_of_fire;
    // Start is called before the first frame update
    void Start()
    {
        eyes = GetComponent<Enemy_Ray_Eyes>();
        point_of_fire = transform.GetChild(1).localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        dir = (int)Mathf.Sign(transform.localScale.x) * 1;
        if(eyes.Check()!=null){
            player = eyes.Check();
            distance=Mathf.Abs(transform.position.x-player.position.x);
        }
        else return;
        if(!can_attack) return;
        GameObject fireball = Instantiate(Fireball_Prefab) as GameObject;
        fireball.transform.parent = transform;
        fireball.transform.localPosition = point_of_fire;
        fireball.transform.parent = null;
        fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(dir*fire_speed,0);
        StartCoroutine(destroyFire(fireball));
        StartCoroutine(delay());

    }

    IEnumerator delay(){
        can_attack=false;
        yield return new WaitForSeconds(delayt);
        can_attack=true;
    }

    IEnumerator destroyFire(GameObject f){
        yield return new WaitForSeconds(fire_lifetime);
        Destroy(f);
    }
}
