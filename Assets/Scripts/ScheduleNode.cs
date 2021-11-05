using UnityEngine;
[CreateAssetMenu(menuName = "NPC Schedule Node")]
public class ScheduleNode : ScriptableObject
{
    public Vector3 location;
    public float timeToLeave;
}