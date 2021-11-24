using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BuildingExistence : MonoBehaviour
{
    public RelationshipManager relationshipManager;
    [SerializeField] private GameObject nonPlayerCharacter1, nonPlayerCharacter2;
    private NonPlayerCharacter npc1, npc2;
    [SerializeField] private GameObject buildingA, buildingB;
    void Start()
    {
        npc1 = nonPlayerCharacter1.GetComponent<NonPlayerCharacter>();
        npc2 = nonPlayerCharacter2.GetComponent<NonPlayerCharacter>();
    }
    void Update()
    {
        if (relationshipManager.GetRelationshipValue(npc1, npc2) > 0)
        {
            ActivateBuilding(buildingA);
            DeactivateBuilding(buildingB);
        }
        else
        {
            ActivateBuilding(buildingB);
            DeactivateBuilding(buildingA);
        }
    }
    private void ActivateBuilding(GameObject go)
    {
        go.SetActive(true);
    }
    private void DeactivateBuilding(GameObject go)
    {
        go.SetActive(false);
    }
}