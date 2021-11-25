using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Inventory : MonoBehaviour
{
    private Inventory _inventory;
    private Transform _itemSlot, _itemSlotParent, _button;
    private void Awake()
    {
        _itemSlotParent = transform.Find("ItemContainer");
        _itemSlot = _itemSlotParent.Find("ItemSlot");
        _button = _itemSlot.Find("Button");
    }
    public void SetInventory(Inventory i)
    {
        this._inventory = i;
        Inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }
    public void RemoveItem(Item i)
    {
        Inventory.RemoveItem(i);
    }
    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }
    private void RefreshInventoryItems()
    {
        foreach (Transform child in _itemSlotParent)
        {
            if (child == _itemSlot) continue;
            Destroy(child.gameObject);
        }
        int x = 0;
        int y = 0;
        int slotSize = 90;
        foreach (Item i in _inventory.GetItemList())
        {
            RectTransform itemSlotRT = Instantiate(_itemSlot, _itemSlotParent).GetComponent<RectTransform>();
            RectTransform itemSlotButton = Instantiate(_button, _itemSlot).GetComponent<RectTransform>();
            itemSlotRT.gameObject.SetActive(true);
            itemSlotButton.gameObject.SetActive(true);
            itemSlotButton.GetComponent<Button>().onClick.AddListener(() => { Debug.Log("clicked"); });
            itemSlotRT.anchoredPosition = new Vector2(x * slotSize + 10, y);
            itemSlotRT.transform.Find("ItemText").GetComponent<TextMeshProUGUI>().text = i.itemName;
            itemSlotRT.transform.Find("ItemQuantity").GetComponent<TextMeshProUGUI>().text = i.quantity.ToString();
            x++;
        }
    }
}