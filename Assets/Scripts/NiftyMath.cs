using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class NiftyMath
{
    public static bool AngleInRange360(Transform v1, Transform v2, Vector3 up, float aMin, float aMax)
    {
        float _angle = Vector3.SignedAngle(v1.forward, v2.forward, up);
        Debug.Log("Angle between: " + _angle);
        return _angle >= aMin && _angle <= aMax;
    }
}