using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HardLines
{
    // Start is called before the first frame update
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t){
        Vector3 p01 = Vector3.Lerp(p0,p1,t);
        Vector3 p12 = Vector3.Lerp(p1,p2,t);
        Vector3 p23 = Vector3.Lerp(p2,p3,t);

        Vector3 p012 = Vector3.Lerp(p01,p12,t);
        Vector3 p123 = Vector3.Lerp(p12,p23,t);

        Vector3 p0123 = Vector3.Lerp(p012,p123,t);

        return p0123;
    }

    public static float GetForce(Transform self, Transform target, float angle){
        Vector3 fromTo = target.position - self.position;

        float distance = new Vector3(fromTo.x,0,0).magnitude;
        float height = fromTo.y;

        float rad_angle = angle * Mathf.PI / 180;
        float v2 = (Physics2D.gravity.y * distance * distance) / (2*(height - Mathf.Tan(rad_angle) * distance) * Mathf.Pow(Mathf.Cos(rad_angle),2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));
        return v;
    }
}
