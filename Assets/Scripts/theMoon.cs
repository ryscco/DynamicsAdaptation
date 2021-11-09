using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theMoon : MonoBehaviour
{
    private void Update() => transform.rotation = Quaternion.Euler(TimeManager.Instance.TimeToAngle(TimeManager.TimeHour, TimeManager.TimeMinute) - 270f, 0f, 0f);
}