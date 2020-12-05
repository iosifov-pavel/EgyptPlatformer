using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps_Shoot : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    Trap_Bullet trap_Bullet;
    private GameObject bullet;
    bool can_attack = true;
    float wait = 1.5f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(can_attack){
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack(){
        can_attack = false;
        bullet = Instantiate(prefab) as GameObject;
        trap_Bullet = bullet.GetComponent<Trap_Bullet>();
        bullet.transform.position =transform.position + new Vector3(0,0,1);
        trap_Bullet.GetDirection(transform.right);
        yield return new WaitForSeconds(wait);
        can_attack = true;
    }
}
