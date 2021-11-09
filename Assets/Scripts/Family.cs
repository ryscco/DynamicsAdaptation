using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "Family")]
public class Family : ScriptableObject
{
    public string familyName;
    public Faction inFaction;
    public NPC[] NPCsInFamily;
    public Personality[] personality;
    [Range(-1.0f,1.0f)]
    public float[] personalityValue;
    public Job[] job;
    [Range(-1.0f, 1.0f)]
    public float[] jobValue;
}