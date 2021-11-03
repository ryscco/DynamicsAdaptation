using UnityEngine;
public class theSun : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Quaternion.Euler(TimeManager.RunningTime * 0.25f, 0f, 0f);
    }
    private void FixedUpdate()
    {
        TimeManager.Instance.Update();
    }
}