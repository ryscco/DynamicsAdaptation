using UnityEngine;
public class Lamp : MonoBehaviour
{
    private TimeManager _timeManager;
    [SerializeField] private Light light;
    private void Awake()
    {
        _timeManager = TimeManager.Instance;
        light = GetComponentInChildren<Light>();
    }
    void Start()
    {
        LightSwitch();
    }
    void Update()
    {
        LightSwitch();
    }
    private void LightSwitch()
    {
        if (_timeManager.TimeOfDay == TimeOfDay.MORNING || _timeManager.TimeOfDay == TimeOfDay.AFTERNOON)
        {
            light.gameObject.SetActive(false);
        }
        else if (_timeManager.TimeOfDay == TimeOfDay.NIGHT)
        {
            light.gameObject.SetActive(true);
        }
    }
}