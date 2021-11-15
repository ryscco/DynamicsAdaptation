using UnityEngine;
using TMPro;
public class theSun : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private void Update()
    {
        transform.rotation = Quaternion.Euler(TimeManager.Instance.TimeToAngle(TimeManager.TimeHour, TimeManager.TimeMinute) - 90f, 0f, 0f);
        timeText.text = string.Format("Time: {0:00}:{1:00}", TimeManager.TimeHour, TimeManager.TimeMinute);
    }
    private void FixedUpdate() => TimeManager.Instance.TimeUpdate();
}