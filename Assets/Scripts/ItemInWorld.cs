using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemInWorld : Interactable
{
    public ItemSO itemScriptableObject;
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
    private void Start()
    {
        namePlateTMPro.transform.rotation = LookAtCamera();
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
    public TextMeshPro namePlateTMPro;
    public override void HideNameplate()
    {
        if (namePlateTMPro.gameObject.activeSelf)
        {
            namePlateTMPro.gameObject.SetActive(false);
        }
    }
    public override void ShowNameplate()
    {
        if (!namePlateTMPro.gameObject.activeSelf)
        {
            namePlateTMPro.gameObject.SetActive(true);
        }
    }
    protected override void beginPlayerInteraction()
    {
        PlayerController._inventory.AddItem(new Item() { itemName = _itemName, itemType = _itemType, quantity = itemQuantity, isStackable = _isStackable });
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