using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Cycle : MonoBehaviour
{
    float swap_time;
    float timer;
    float active_time;
    List<Transform> plats;
    int active = 0;
    // Start is called before the first frame update
    void Start()
    {
        plats = new List<Transform>();
        swap_time = 1f;
        timer=swap_time;
        active_time = 2f;
        int count = transform.childCount;
        for(int i=0;i<count;i++){
            plats.Add(transform.GetChild(i));
        }
       // plats[active].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        timer-=Time.deltaTime;
        if(swap_time<=0){
            StartCoroutine(Swap(plats[active].gameObject));
            active++;
            if(active==5) active=0;
            timer=swap_time;
        }
    }

    IEnumerator Swap(GameObject plat){
        plat.SetActive(true);
        yield return new WaitForSeconds(active_time);
        plat.SetActive(false);
    }
}
