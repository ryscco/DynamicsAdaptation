using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_Inventory : MonoBehaviour
{
    private Inventory _inventory;
    private Transform _itemSlot, _itemSlotParent;
    private void Awake()
    {
        _itemSlotParent = transform.Find("ItemContainer");
        _itemSlot = _itemSlotParent.Find("ItemSlot");
    }
    public void SetInventory(Inventory i)
    {
        this._inventory = i;
        RefreshInventoryItems();
    }
    private void RefreshInventoryItems()
    {
        int x = 0;
        int y = 0;
        int slotSize = 90;
        foreach (Item i in _inventory.GetItemList())
        {
            RectTransform itemSlotRT = Instantiate(_itemSlot, _itemSlotParent).GetComponent<RectTransform>();
            itemSlotRT.gameObject.SetActive(true);
            itemSlotRT.anchoredPosition = new Vector2(x * slotSize + 10, y);
            itemSlotRT.transform.Find("ItemText").GetComponent<TextMeshProUGUI>().text = i.itemName;
            itemSlotRT.transform.Find("ItemQuantity").GetComponent<TextMeshProUGUI>().text = i.quantity.ToString();
            x++;
        }
    }
}