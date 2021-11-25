using UnityEngine;
public class Lamp : MonoBehaviour
{
    private TimeManager _timeManager;
    [SerializeField] private GameObject lightGO;
    private void Awake()
    {
        _timeManager = TimeManager.Instance;
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
            lightGO.SetActive(false);
        }
        else if (_timeManager.TimeOfDay == TimeOfDay.NIGHT)
        {
            lightGO.SetActive(true);
        }
    }
}