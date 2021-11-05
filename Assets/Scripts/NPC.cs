using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
[CreateAssetMenu(menuName = "NPC")]
public class NPC : ScriptableObject
{
    public string npcName;
    public Faction faction;
    public Family family;
    public ScheduleNode[] schedule;

    public Queue<Vector4> GetSchedule ()
    {
        Queue<Vector4> q = new Queue<Vector4>();
        foreach (ScheduleNode sn in schedule)
        {
            Vector4 v = new Vector4(sn.location.x, sn.location.y, sn.location.z, sn.timeToLeave);
            q.Enqueue(v);
        }
        return q;
    }
    public void ShowSchedule(Queue<Vector4> q)
    {
        foreach (Vector4 v in q)
        {
            Debug.Log("V4: " + v);
        }
    }
}