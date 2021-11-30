using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory
{
    public static Inventory Instance;
    public static event EventHandler OnItemListChanged;
    private static List<Item> itemList;
    public Inventory()
    {
        Instance = this;
        itemList = new List<Item>();
    }
    public static void AddItem(Item i)
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
        OnItemListChanged?.Invoke(Instance, EventArgs.Empty);
    }
    public static void RemoveItem(Item i)
    {
        if (i.isStackable)
        {
            Item itemInInventory = null;
            foreach (Item item in itemList)
            {
                if (item.itemName == i.itemName)
                {
                    item.quantity -= i.quantity;
                    itemInInventory = item;
                }
            }
            if (itemInInventory != null && itemInInventory.quantity <= 0)
            {
                itemList.Remove(i);
            }
        }
        else
        {
            foreach (Item item in itemList)
            {
                if (item.itemName == i.itemName)
                {
                    itemList.Remove(i);
                    continue;
                }
            }
        }
        OnItemListChanged?.Invoke(Instance, EventArgs.Empty);
    }
    public static void RemoveItem(string s)
    {
        Item temp = itemList.First(i => i.itemName == s);
        RemoveItem(temp);
    }
    public List<Item> GetItemList()
    {
        return itemList;
    }
}