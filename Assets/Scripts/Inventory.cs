using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory
{
    private List<Item> itemList;
    public Inventory()
    {
        itemList = new List<Item>();
        AddItem(new Item() { itemName = "Gold", itemType = ItemType.COIN, quantity = 10 });
        AddItem(new Item() { itemName = "Flower", itemType = ItemType.GIFT, quantity = 1 });
    }
    public void AddItem(Item i)
    {
        itemList.Add(i);
    }
    public List<Item> GetItemList()
    {
        return itemList;
    }
}