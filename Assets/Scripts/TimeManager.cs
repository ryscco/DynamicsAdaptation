using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager
{
    static int _increment = 0;
    static float _minute = 0f;
    static float _hour = 0f;
    static float _runningTime = 0;
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
    public static float TimeHour
    {
        get
        {
            return _hour;
        }
    }
    public static float TimeMinute
    {
        get
        {
            return _minute;
        }
    }
    public static float RunningTime
    {
        get
        {
            return _runningTime / 50f;
        }
    }
    public void OnApplicationQuit()
    {
        TimeManager._instance = null;
    }
    public void Update()
    {
        _increment += 1;
        _runningTime += _increment;
        _minute = _increment * 1.2f;
        if (_minute >= 60)
        {
            _increment = 0;
            _minute = 0;
            _hour += 1;
        }
        if (_hour >= 24)
        {
            _hour = 0;
        }
    }
}