using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType { POTION, WEAPON, ARMOR, TRINKET, COIN, GIFT }
public class Item
{
    public string itemName;
    public ItemType itemType;
    public int quantity;
}