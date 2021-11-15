using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
[CreateAssetMenu(menuName = "Faction")]
public class Faction : ScriptableObject
{
    public string factionName;
    public Family[] subordinateFamilies;
    [SerializeField] public FamilyStanding[] familyStandings;
    [SerializeField] public FactionStanding[] factionStandings;
    [System.Serializable]
    public struct FamilyStanding
    {
        public Family family;
        [Range(-1.0f, 1.0f)] public float factionStanding;
    }
    [System.Serializable]
    public struct FactionStanding
    {
        public Faction faction;
        [Range(-1.0f, 1.0f)] public float interFactionStanding;
    }
    public override string ToString()
    {
        return factionName;
    }
}