using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanApplyForce : MonoBehaviour
{
    [SerializeField] float forceStrength = 1f;
    [SerializeField] float forceTime = 0.5f;
    [SerializeField] bool isImpulse = false;
    [SerializeField]string id = "force name";
    [SerializeField] Vector2 forceDirection = Vector2.zero;
    [SerializeField] bool isLegApply = false;
    Vector2 resultForce;
    Movement player;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update() {
        resultForce = forceDirection * forceStrength;
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other) {
        if(isLegApply && other.gameObject.tag=="GroundCheck" || !isLegApply && other.gameObject.tag=="Player"){
            if(isLegApply) player = other.GetComponentInParent<Movement>();
            else player = other.GetComponent<Movement>();
            if(!isImpulse) player.SetConstantForce(id,resultForce);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (isLegApply && other.gameObject.tag == "GroundCheck" || !isLegApply && other.gameObject.tag == "Player")
        {
            if (isLegApply) player = other.GetComponentInParent<Movement>();
            else player = other.GetComponent<Movement>();
            if (isImpulse) player.SetImpulseForce(resultForce, forceTime);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (isLegApply && other.gameObject.tag == "GroundCheck" || !isLegApply && other.gameObject.tag == "Player")
        {
            if (isLegApply) player = other.GetComponentInParent<Movement>();
            else player = other.GetComponent<Movement>();
            if (!isImpulse) player.RemoveForce(id);
        }
    }
}
