using UnityEngine;
public class theSun : MonoBehaviour
{
    private void Update() => transform.rotation = Quaternion.Euler(TimeManager.Instance.TimeToAngle(TimeManager.TimeHour, TimeManager.TimeMinute) - 90f, 0f, 0f);
    private void FixedUpdate() => TimeManager.Instance.TimeUpdate();
}