using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RelationshipManager : MonoBehaviour
{
    public struct Relationship
    {
        public NonPlayerCharacter npc1, npc2;
        public float relationshipValue;
        public Relationship(NonPlayerCharacter npc1, NonPlayerCharacter npc2, float relVal)
        {
            this.npc1 = npc1;
            this.npc2 = npc2;
            this.relationshipValue = relVal;
        }
    }
    public List<Relationship> allRelationships;
    public GameObject[] allNPCs;
    public Family[] allFamilies;
    public Faction[] allFactions;
    private void Start()
    {
        allNPCs = GameObject.FindGameObjectsWithTag("NPC");
        GenerateBaselineRelationships();
        foreach(Relationship r in allRelationships)
        {
            Debug.Log(r.npc1.characterName + "'s relationship with " + r.npc2.characterName + " has a value of: " + r.relationshipValue);
        }
    }
    public void GenerateBaselineRelationships()
    {
        foreach (GameObject n in allNPCs)
        {
            foreach (GameObject m in allNPCs)
            {
                if (m.GetComponent<NonPlayerCharacter>().characterName != n.GetComponent<NonPlayerCharacter>().characterName)
                {
                    NonPlayerCharacter a = m.GetComponent<NonPlayerCharacter>();
                    NonPlayerCharacter b = n.GetComponent<NonPlayerCharacter>();
                    allRelationships.Add(new Relationship(a, b, GetRelationshipValue(a, b)));
                }
            }
        }
    }
    public float GetRelationshipValue(NonPlayerCharacter a, NonPlayerCharacter b)
    {
        float runningTallyA = 0f;
        float runningTallyB = 0f;
        float total = 0f;
        runningTallyA += GetFamilyValues(a);
        runningTallyB += GetFamilyValues(b);
        if (a.family == b.family) return runningTallyA + runningTallyB;
        runningTallyA += GetFactionValues(a);
        runningTallyB += GetFactionValues(b);
        if (a.family.inFaction == b.family.inFaction) return runningTallyA + runningTallyB;
        for (int i = 0; i < allFactions.Length; i++)
        {
            if (a.family.inFaction == allFactions[i])
            {
                for (int j = 0; j < a.family.inFaction.factionStandings.Length; j++)
                {
                    if (a.family.inFaction.factionStandings[j].faction == b.family.inFaction)
                    {
                        total = (runningTallyA + runningTallyB) * a.family.inFaction.factionStandings[j].interFactionStanding;
                    }
                }
            }
        }
        return total;
    }
    private float GetFamilyValues(NonPlayerCharacter a)
    {
        float runningTally = 0f;
        int i = 0;
        foreach (NPCJob j in a.family.job)
        {
            if (j == a.connectionLogic.job) runningTally += a.family.jobValue[i];
            i++;
        }
        i = 0;
        foreach (NPCPersonality p in a.family.personality)
        {
            if (p == a.connectionLogic.personality) runningTally += a.family.personalityValue[i];
            i++;
        }
        i = 0;
        runningTally += a.connectionLogic.wealth;
        return runningTally;
    }
    private float GetFactionValues(NonPlayerCharacter a)
    {
        float runningTally = 0f;
        for (int f = 0; f < a.family.inFaction.familyStandings.Length; f++)
        {
            if (a.family == a.family.inFaction.familyStandings[f].family) runningTally += a.family.inFaction.familyStandings[f].factionStanding;
        }
        return runningTally;
    }
}