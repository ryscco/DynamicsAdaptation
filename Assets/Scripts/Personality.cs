using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum NPCPersonality { Friendly, Rude, Reserved }
[CreateAssetMenu(menuName = "NPC Personality")]
public class Personality : ScriptableObject
{
    public NPCPersonality personality;
}