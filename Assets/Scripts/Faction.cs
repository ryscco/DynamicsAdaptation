using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
[CreateAssetMenu(menuName = "Faction")]
public class Faction : ScriptableObject
{
    public string factionName;
    public Family[] subordinateFamilies;
    [SerializeField] public familyStanding[] familyStandings;
    [SerializeField] public factionStanding[] factionStandings;
    [System.Serializable]
    public struct familyStanding
    {
        public Family family;
        [Range(-1.0f, 1.0f)] public float factionStanding;
    }
    [System.Serializable]
    public struct factionStanding
    {
        public Faction faction;
        [Range(-1.0f, 1.0f)] public float interFactionStanding;
    }
}