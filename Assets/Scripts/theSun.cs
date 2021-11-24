using UnityEngine;
using TMPro;
public class theSun : MonoBehaviour
{
    private TimeManager _timeManager;
    public TextMeshProUGUI timeText;
    private void Awake()
    {
        _timeManager = TimeManager.Instance;
    }
    private void Update()
    {
        transform.rotation = Quaternion.Euler(TimeManager.Instance.TimeToAngle(_timeManager.TimeHour, _timeManager.TimeMinute) - 90f, 0f, 0f);
        timeText.text = string.Format("Time: {0:00}:{1:00}", _timeManager.TimeHour, _timeManager.TimeMinute);
    }
    private void FixedUpdate() => TimeManager.Instance.TimeUpdate();
}