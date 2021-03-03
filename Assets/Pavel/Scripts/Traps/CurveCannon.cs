using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveCannon : MonoBehaviour
{
    [SerializeField] GameObject SawPatron;
    WithCurve patron;
    // Start is called before the first frame update
    void Start()
    {
        patron = SawPatron.GetComponent<WithCurve>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            GameObject new_patron = Instantiate(SawPatron);
            new_patron.transform.position = transform.position;
            new_patron.GetComponent<WithCurve>().SetDestination(other.transform, transform);
            new_patron.SetActive(true);
        }
    }
}
