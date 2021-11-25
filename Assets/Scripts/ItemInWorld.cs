using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemInWorld : Interactable
{
    public ItemSO itemScriptableObject;
    public GameObject namePlateTMPro;
    string _itemName;
    ItemType _itemType;
    public int itemQuantity;
    bool _isStackable;
    private void Awake()
    {
        _itemName = itemScriptableObject.itemName;
        _itemType = itemScriptableObject.itemType;
        _isStackable = itemScriptableObject.isStackable;
    }
    private void Update()
    {
        if (this.isInteractable())
        {
            ShowNameplate();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                beginPlayerInteraction();
            }
        }
        else HideNameplate();
    }
    public override void HideNameplate()
    {
        if (namePlateTMPro.activeSelf)
        {
            namePlateTMPro.SetActive(false);
        }
    }
    public override void ShowNameplate()
    {
        if (!namePlateTMPro.activeSelf)
        {
            namePlateTMPro.gameObject.transform.rotation = LookAtCamera;
            namePlateTMPro.SetActive(true);
        }
    }
    protected override void beginPlayerInteraction()
    {
        Inventory.AddItem(new Item() { itemName = _itemName, itemType = _itemType, quantity = itemQuantity, isStackable = _isStackable });
        exitPlayerInteraction();
    }
    protected override void exitPlayerInteraction()
    {
        Destroy(transform.Find("WorldItemIndicator").gameObject);
        Destroy(GetComponent<ItemInWorld>());
    }
    protected override void playerInteraction()
    {
        throw new System.NotImplementedException();
    }
}