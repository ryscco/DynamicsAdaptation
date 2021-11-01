using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NonPlayerCharacter : Interactable
{
    public string npcName;
    public Faction faction = Faction.REDTEAM;
    [SerializeField] private TextMeshPro textName;
    void Start()
    {
        textName.text = npcName;
    }
    void Update()
    {
        if (this.isInteractable()) showNameplate();
        else hideNameplate();
    }
    override public void showNameplate()
    {
        if (!textName.gameObject.activeSelf)
        {
            textName.gameObject.SetActive(true);
        }
    }
    public override void hideNameplate()
    {
        if (textName.gameObject.activeSelf)
        {
            textName.gameObject.SetActive(false);
        }
    }
}