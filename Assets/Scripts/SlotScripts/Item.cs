using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public int itemID;
    public int itemPrice;
    public Sprite itemIcon;
    public ItemType itemType;

    public enum ItemType
    {
        Weapon, Equipment, Consumable
    }

    public Item(int _itemID, int _itemPrice, ItemType _itemType)
    {
        itemID = _itemID;
        itemPrice = _itemPrice;
        itemIcon = Resources.Load("ItemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite;
        itemType = _itemType;
    }
}
