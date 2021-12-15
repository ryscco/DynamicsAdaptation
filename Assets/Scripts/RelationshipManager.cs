using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class RelationshipManager : MonoBehaviour
{
    public class Relationship
    {
        public GameObject npc1, npc2;
        public float relationshipValue;
        public Relationship(GameObject a, GameObject b, float relVal)
        {
            this.npc1 = a;
            this.npc2 = b;
            this.relationshipValue = relVal;
        }
    }
    public List<Relationship> allRelationships;
    public GameObject[] allNPCs;
    public Family[] allFamilies;
    public Faction[] allFactions;
    private void Awake()
    {
        allRelationships = new List<Relationship>();
    }
    private void Start()
    {
        allNPCs = GameObject.FindGameObjectsWithTag("NPC");
        GenerateBaselineRelationships();
    }
    public void GenerateBaselineRelationships()
    {
        foreach (GameObject n in allNPCs)
        {
            foreach (GameObject m in allNPCs)
            {
                if (m.GetComponent<NonPlayerCharacter>().characterName != n.GetComponent<NonPlayerCharacter>().characterName)
                {
                    Relationship rel = new Relationship(m, n, GetRelationshipValue(m, n));
                    allRelationships.Add(rel);
                }
            }
        }
    }
    public float GetRelationshipValue(GameObject a, GameObject b)
    {
        NonPlayerCharacter npc1 = a.GetComponent<NonPlayerCharacter>();
        NonPlayerCharacter npc2 = b.GetComponent<NonPlayerCharacter>();
        float runningTallyA = 0f;
        float runningTallyB = 0f;
        float total = 0f;
        runningTallyA += GetFamilyValues(a);
        runningTallyB += GetFamilyValues(b);
        if (npc1.family == npc2.family)
        {
            //Debug.Log(runningTallyA + runningTallyB);
            total = runningTallyA + runningTallyB;
            return runningTallyA + runningTallyB;
        }
        runningTallyA += GetFactionValues(a);
        runningTallyB += GetFactionValues(b);
        if (npc1.family.inFaction == npc2.family.inFaction)
        {
            //Debug.Log(runningTallyA + runningTallyB);
            total = runningTallyA + runningTallyB;
            return runningTallyA + runningTallyB;
        }
        for (int i = 0; i < allFactions.Length; i++)
        {
            if (npc1.family.inFaction == allFactions[i])
            {
                for (int j = 0; j < npc1.family.inFaction.factionStandings.Length; j++)
                {
                    if (npc1.family.inFaction.factionStandings[j].faction == npc2.family.inFaction)
                    {
                        total = (runningTallyA + runningTallyB) * 
                            npc1.family.inFaction.factionStandings[j].interFactionStanding;
                    }
                }
            }
        }
        return total;
    }
    private float GetFamilyValues(GameObject a)
    {
        NonPlayerCharacter npc1 = a.GetComponent<NonPlayerCharacter>();
        float runningTally = 0f;
        int i = 0;
        foreach (NPCJob j in npc1.family.job)
        {
            if (j == npc1.connectionLogic.job) runningTally += npc1.family.jobValue[i];
            i++;
        }
        i = 0;
        foreach (NPCPersonality p in npc1.family.personality)
        {
            if (p == npc1.connectionLogic.personality) runningTally += npc1.family.personalityValue[i];
            i++;
        }
        i = 0;
        runningTally += npc1.connectionLogic.wealth;
        return runningTally;
    }
    private float GetFactionValues(GameObject a)
    {
        NonPlayerCharacter npc1 = a.GetComponent<NonPlayerCharacter>();
        float runningTally = 0f;
        for (int f = 0; f < npc1.family.inFaction.familyStandings.Length; f++)
        {
            if (npc1.family == npc1.family.inFaction.familyStandings[f].family) runningTally += npc1.family.inFaction.familyStandings[f].factionStanding;
        }
        return runningTally;
    }
    public List<Relationship> GetRelationships(GameObject o)
    {
        List<Relationship> list = new List<Relationship>();
        foreach (Relationship r in allRelationships)
        {
            if (r.npc1 == o) list.Add(r);
        }
        return list;
    }
    public void PrintRelationships(GameObject o)
    {
        foreach (Relationship r in allRelationships)
        {
            if (r.npc1 == o.gameObject)
            {
                Debug.Log("My relationship with " + r.npc2.GetComponent<NonPlayerCharacter>().characterName + " has a value of: " + r.relationshipValue);
            }
        }
    }
    public float GetRelationshipValue(NonPlayerCharacter npc1, NonPlayerCharacter npc2)
    {
        foreach (Relationship r in allRelationships)
        {
            if (r.npc1 == npc1.gameObject)
            {
                foreach (Relationship x in allRelationships)
                {
                    if (x.npc2 == npc2.gameObject) return x.relationshipValue;
                }
            }
        }
        return 0;
    }
    public void ChangeRelationship(NonPlayerCharacter npc1, NonPlayerCharacter npc2, float val)
    {
        foreach (Relationship r in allRelationships)
        {
            if (r.npc1 == npc1.gameObject)
            {
                foreach (Relationship x in allRelationships)
                {
                    if (x.npc2 == npc2.gameObject)
                    {
                        x.relationshipValue += val;
                        continue;
                    }
                }
            }
            continue;
        }
    }
}