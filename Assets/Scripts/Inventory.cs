using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory
{
    public event EventHandler OnItemListChanged;
    private List<Item> itemList;
    public Inventory()
    {
        itemList = new List<Item>();
        //AddItem(new Item() { itemName = "Gold", itemType = ItemType.COIN, quantity = 10 });
        AddItem(new Item() { itemName = "Flower", itemType = ItemType.GIFT, quantity = 1, isStackable = true });
    }
    public void AddItem(Item i)
    {
        if (i.isStackable)
        {
            bool itemInInventory = false;
            foreach (Item item in itemList)
            {
                if (item.itemName == i.itemName)
                {
                    item.quantity += i.quantity;
                    itemInInventory = true;
                }
            }
            if (!itemInInventory)
            {
                itemList.Add(i);
            }
        }
        else itemList.Add(i);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public List<Item> GetItemList()
    {
        return itemList;
    }
}