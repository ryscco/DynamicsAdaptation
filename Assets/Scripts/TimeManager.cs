public class TimeManager
{
    static float _increment = 2.4f, _runningTime = 0f;
    static int _minute = 0, _hour = 0;
    protected TimeManager() { }
    private static TimeManager _instance = null;
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
    public static int TimeHour
    {
        get => _hour;
    }
    public static float TimeMinute
    {
        get => _minute;
    }
    public static float RunningTime
    {
        get => _runningTime / 50f;
    }
    public static float Increment
    {
        get => _increment;
    }
    public void OnApplicationQuit()
    {
        TimeManager._instance = null;
    }
    public void TimeUpdate()
    {
        _runningTime += _increment;
        _minute = (int)(_runningTime / 60f) % 60;
        _hour = (int)(_runningTime / 3600) % 24;
    }
    public void SetIncrement(float i) => _increment = i;
    public float TimeToAngle(float h, float m) => ((h * 60) + m) * 0.25f;
}