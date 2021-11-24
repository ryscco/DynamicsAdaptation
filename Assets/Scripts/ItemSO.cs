using UnityEngine;
[CreateAssetMenu(menuName = "Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public bool isStackable;
}