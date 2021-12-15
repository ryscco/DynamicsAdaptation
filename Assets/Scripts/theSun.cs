using UnityEngine;
using TMPro;
public class theSun : MonoBehaviour
{
    private TimeManager _timeManager;
    [SerializeField] private float _gameStartTime;
    public TextMeshProUGUI timeText;
    public Material skybox_day, skybox_night;
    private void Awake()
    {
        _timeManager = TimeManager.Instance;
        _timeManager.RunningTime = _gameStartTime;
    }
    private void Update()
    {
        transform.rotation = Quaternion.Euler(TimeManager.Instance.TimeToAngle(_timeManager.TimeHour, _timeManager.TimeMinute) - 90f, 0f, 0f);
        timeText.text = string.Format("Time: {0:00}:{1:00}", _timeManager.TimeHour, _timeManager.TimeMinute);
        SetSkybox();
    }
    private void FixedUpdate() => TimeManager.Instance.TimeUpdate();
    private void SetSkybox()
    {
        if (_timeManager.TimeOfDay == TimeOfDay.MORNING || _timeManager.TimeOfDay == TimeOfDay.AFTERNOON)
        {
            RenderSettings.skybox = skybox_day;
        }
        else RenderSettings.skybox = skybox_night;
    }
}