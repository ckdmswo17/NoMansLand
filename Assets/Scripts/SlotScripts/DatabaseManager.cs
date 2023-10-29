using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{

    static public DatabaseManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switches;

    public List<Item> itemList = new List<Item>();

    private void Start()
    {
        itemList.Add(new Item(1, 50000, Item.ItemType.Weapon));
        itemList.Add(new Item(2, 50000, Item.ItemType.Weapon));
        itemList.Add(new Item(3, 50000, Item.ItemType.Weapon));
        itemList.Add(new Item(4, 50000, Item.ItemType.Weapon));
        itemList.Add(new Item(5, 50000, Item.ItemType.Weapon));
        itemList.Add(new Item(6, 50000, Item.ItemType.Weapon));
        itemList.Add(new Item(7, 50000, Item.ItemType.Weapon));
        itemList.Add(new Item(8, 50000, Item.ItemType.Weapon));
        itemList.Add(new Item(9, 50000, Item.ItemType.Weapon));
        itemList.Add(new Item(10, 50000, Item.ItemType.Weapon));

        itemList.Add(new Item(1, 50000, Item.ItemType.Equipment));
        itemList.Add(new Item(2, 50000, Item.ItemType.Equipment));
        itemList.Add(new Item(3, 50000, Item.ItemType.Equipment));
        itemList.Add(new Item(4, 50000, Item.ItemType.Equipment));
        itemList.Add(new Item(5, 50000, Item.ItemType.Equipment));
        itemList.Add(new Item(6, 50000, Item.ItemType.Equipment));
        itemList.Add(new Item(7, 50000, Item.ItemType.Equipment));
        itemList.Add(new Item(8, 50000, Item.ItemType.Equipment));
        itemList.Add(new Item(9, 50000, Item.ItemType.Equipment));
        itemList.Add(new Item(10, 50000, Item.ItemType.Equipment));

        itemList.Add(new Item(1, 50000, Item.ItemType.Consumable));
        itemList.Add(new Item(2, 50000, Item.ItemType.Consumable));
        itemList.Add(new Item(3, 50000, Item.ItemType.Consumable));
        itemList.Add(new Item(4, 50000, Item.ItemType.Consumable));
        itemList.Add(new Item(5, 50000, Item.ItemType.Consumable));
        itemList.Add(new Item(6, 50000, Item.ItemType.Consumable));
        itemList.Add(new Item(7, 50000, Item.ItemType.Consumable));
        itemList.Add(new Item(8, 50000, Item.ItemType.Consumable));
        itemList.Add(new Item(9, 50000, Item.ItemType.Consumable));
        itemList.Add(new Item(10, 50000, Item.ItemType.Consumable));



    }

}
