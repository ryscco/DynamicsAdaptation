using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theMoon : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Quaternion.Euler((TimeManager.RunningTime * 0.25f) - 180f, 0f, 0f);
    }
}