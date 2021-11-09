using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
[CreateAssetMenu(menuName = "NPC")]
public class NPC : ScriptableObject
{
    public string npcName;
    public Family family;
    public ScheduleNode[] schedule;
    public Relationship[] relationships;
    public RelationshipManager relMan;
    [SerializeField] public ConnectionLogic connectionLogic;
    [System.Serializable]
    public struct ConnectionLogic
    {
        public Job job;
        [Range(0f, 1f)] public float wealth;
        public Personality personality;
    }
    public Queue<Vector4> GetSchedule()
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
    public float getRelationshipValue(NPC n)
    {
        float runningTally = 0f;
        int i = 0;
        foreach (Job j in this.family.job)
        {
            if (j == this.connectionLogic.job)
            {
                runningTally += this.family.jobValue.ElementAt(i);
            }
            i++;
        }
        runningTally += this.connectionLogic.wealth;
        i = 0;
        foreach (Personality p in this.family.personality)
        {
            if (p == this.connectionLogic.personality)
            {
                runningTally += this.family.personalityValue.ElementAt(i);
            }
            i++;
        }
        if (n.family == this.family) return runningTally;
        if (this.family.inFaction == n.family.inFaction)
        {
            i = 0;
            foreach (Family f in this.family.inFaction.subordinateFamilies)
            {
                if (this.family == this.family.inFaction.familyStandings[i].family)
                {
                    runningTally += this.family.inFaction.familyStandings[i].factionStanding;
                }
                i++;
            }
            return runningTally;
        }
        i = 0;
        //foreach (Faction f in GameManager.Instance.allFactions)
        //{
        //    if (n.family.inFaction == f.factionStandings[i].faction)
        //    {
        //        Debug.Log(n.family.inFaction + " " + f.factionStandings[i].faction);
        //        runningTally += f.factionStandings[i].interFactionStanding;
        //    }
        //    i++;
        //}
        return runningTally;
    }
}