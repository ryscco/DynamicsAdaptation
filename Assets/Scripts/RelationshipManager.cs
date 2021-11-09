using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RelationshipManager : MonoBehaviour
{
    public List<Relationship> allRelationships;
    public NPC[] allNPCs;
    public Family[] allFamilies;
    public Faction[] allFactions;
    public void SetBaselineRelationships()
    {
        foreach (NPC n in allNPCs)
        {
            foreach (NPC m in allNPCs)
            {
                if (m.npcName != n.npcName)
                {
                    allRelationships.Add(new Relationship(n, m, n.getRelationshipValue(m)));
                }
            }
        }
    }
    public Faction[] GetAllFactions()
    {
        return allFactions;
    }
    public void DisplayRelationships()
    {
        foreach (Relationship r in allRelationships)
        {
            Debug.Log(r.npc1.npcName + "'s relationship with " + r.npc2.npcName + " has a value of " + r.relationshipValue);
        }
    }
}