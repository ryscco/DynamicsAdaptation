using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class UI_Inventory : MonoBehaviour
{
    private Inventory _inventory;
    private Transform _itemSlot, _itemSlotParent;
    private int _removeQuantity = 1;
    private void Awake()
    {
        _itemSlotParent = transform.Find("ItemContainer");
        _itemSlot = _itemSlotParent.Find("ItemSlot");
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.RightShift)) _removeQuantity = 5;
        else _removeQuantity = 1;
    }
    public void SetInventory(Inventory i)
    {
        this._inventory = i;
        Inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
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
            itemSlotRT.gameObject.SetActive(true);
            itemSlotRT.anchoredPosition = new Vector2(x * slotSize + 10, y);
            itemSlotRT.gameObject.GetComponentInChildren<Button>().onClick.AddListener(() => Inventory.RemoveItem(i, _removeQuantity));
            itemSlotRT.transform.Find("ItemText").GetComponent<TextMeshProUGUI>().text = i.itemName;
            itemSlotRT.transform.Find("ItemQuantity").GetComponent<TextMeshProUGUI>().text = i.quantity.ToString();
            x++;
        }
    }
}