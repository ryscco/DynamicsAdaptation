public enum TimeOfDay { MORNING, AFTERNOON, NIGHT }
public class TimeManager
{
    protected TimeManager() { }
    private static TimeManager _instance = null;
    private float _increment = 2.4f, _runningTime = 0f;
    private int _minute = 0, _hour = 0;
    private TimeOfDay _timeOfDay;
    public static TimeManager Instance
    {
        get
        {
            if (TimeManager._instance == null)
            {
                TimeManager._instance = new TimeManager();
            }
            return TimeManager._instance;
        }
    }
    public int TimeHour { get => _hour; }
    public float TimeMinute { get => _minute; }
    public float RunningTime { get => _runningTime / 50f; set => _runningTime = value; }
    public float Increment { get => _increment; }
    public TimeOfDay TimeOfDay { get => _timeOfDay; }
    public void OnApplicationQuit() => TimeManager._instance = null;
    public void TimeUpdate()
    {
        _runningTime += _increment;
        _minute = (int)(_runningTime / 60f) % 60;
        _hour = (int)(_runningTime / 3600) % 24;
        SetTimeOfDay();
    }
    public void SetIncrement(float i) => _increment = i;
    public float TimeToAngle(float h, float m) => ((h * 60) + m) * 0.25f;
    private void SetTimeOfDay()
    {
        if (TimeHour <= 11 && TimeHour >= 5 && _timeOfDay != TimeOfDay.MORNING)
        {
            _timeOfDay = TimeOfDay.MORNING;
        }
        else if (TimeHour <= 19 && TimeHour >= 12 && _timeOfDay != TimeOfDay.AFTERNOON)
        {
            _timeOfDay = TimeOfDay.AFTERNOON;
        }
        else if (((TimeHour <= 24 && TimeHour >= 20) || (TimeHour <= 4 && TimeHour >= 0)) && _timeOfDay != TimeOfDay.NIGHT)
        {
            _timeOfDay = TimeOfDay.NIGHT;
        }
    }
}