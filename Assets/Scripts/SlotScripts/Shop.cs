using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject weaponSlot;
    public GameObject equipmentSlot;
    public GameObject consumableSlot;
    public ItemSlot[] weaponSlots;
    public ItemSlot[] equipmentSlots;
    public ItemSlot[] consumableSlots;

    public Transform tfWeapon;
    public Transform tfEquipment;
    public Transform tfConsumable;

    private List<Item> weaponList = new List<Item>();
    private List<Item> equipmentList = new List<Item>();
    private List<Item> consumableList = new List<Item>();



    void Start()
    {
        weaponSlots = tfWeapon.GetComponentsInChildren<ItemSlot>();
        equipmentSlots = tfEquipment.GetComponentsInChildren<ItemSlot>();
        consumableSlots = tfConsumable.GetComponentsInChildren<ItemSlot>();

        weaponList.Add(new Item(1, 50000, Item.ItemType.Weapon));
        weaponList.Add(new Item(2, 50000, Item.ItemType.Weapon));
        weaponList.Add(new Item(3, 50000, Item.ItemType.Weapon));
        weaponList.Add(new Item(4, 50000, Item.ItemType.Weapon));
        weaponList.Add(new Item(5, 50000, Item.ItemType.Weapon));
        weaponList.Add(new Item(6, 50000, Item.ItemType.Weapon));
        weaponList.Add(new Item(7, 50000, Item.ItemType.Weapon));
        weaponList.Add(new Item(8, 50000, Item.ItemType.Weapon));
        weaponList.Add(new Item(9, 50000, Item.ItemType.Weapon));
        weaponList.Add(new Item(10, 50000, Item.ItemType.Weapon));
        weaponList.Add(new Item(1, 50000, Item.ItemType.Weapon));
        weaponList.Add(new Item(2, 50000, Item.ItemType.Weapon));
        weaponList.Add(new Item(3, 50000, Item.ItemType.Weapon));
        weaponList.Add(new Item(4, 50000, Item.ItemType.Weapon));
        weaponList.Add(new Item(5, 50000, Item.ItemType.Weapon));
        weaponList.Add(new Item(6, 50000, Item.ItemType.Weapon));

        equipmentList.Add(new Item(1, 50000, Item.ItemType.Equipment));
        equipmentList.Add(new Item(2, 50000, Item.ItemType.Equipment));
        equipmentList.Add(new Item(3, 50000, Item.ItemType.Equipment));
        equipmentList.Add(new Item(4, 50000, Item.ItemType.Equipment));
        equipmentList.Add(new Item(5, 50000, Item.ItemType.Equipment));
        equipmentList.Add(new Item(6, 50000, Item.ItemType.Equipment));
        equipmentList.Add(new Item(7, 50000, Item.ItemType.Equipment));
        equipmentList.Add(new Item(8, 50000, Item.ItemType.Equipment));
        equipmentList.Add(new Item(9, 50000, Item.ItemType.Equipment));
        equipmentList.Add(new Item(10, 50000, Item.ItemType.Equipment));
        equipmentList.Add(new Item(1, 50000, Item.ItemType.Equipment));
        equipmentList.Add(new Item(2, 50000, Item.ItemType.Equipment));
        equipmentList.Add(new Item(3, 50000, Item.ItemType.Equipment));
        equipmentList.Add(new Item(4, 50000, Item.ItemType.Equipment));
        equipmentList.Add(new Item(5, 50000, Item.ItemType.Equipment));
        equipmentList.Add(new Item(6, 50000, Item.ItemType.Equipment));

        consumableList.Add(new Item(1, 50000, Item.ItemType.Consumable));
        consumableList.Add(new Item(2, 50000, Item.ItemType.Consumable));
        consumableList.Add(new Item(3, 50000, Item.ItemType.Consumable));
        consumableList.Add(new Item(4, 50000, Item.ItemType.Consumable));
        consumableList.Add(new Item(5, 50000, Item.ItemType.Consumable));
        consumableList.Add(new Item(6, 50000, Item.ItemType.Consumable));
        consumableList.Add(new Item(7, 50000, Item.ItemType.Consumable));
        consumableList.Add(new Item(8, 50000, Item.ItemType.Consumable));
        consumableList.Add(new Item(9, 50000, Item.ItemType.Consumable));
        consumableList.Add(new Item(10, 50000, Item.ItemType.Consumable));
        consumableList.Add(new Item(1, 50000, Item.ItemType.Consumable));
        consumableList.Add(new Item(2, 50000, Item.ItemType.Consumable));
        consumableList.Add(new Item(3, 50000, Item.ItemType.Consumable));
        consumableList.Add(new Item(4, 50000, Item.ItemType.Consumable));
        consumableList.Add(new Item(5, 50000, Item.ItemType.Consumable));
        consumableList.Add(new Item(6, 50000, Item.ItemType.Consumable));
        showItem();
    }

    void Update()
    {
        
    }


    public void showItem()
    {
        for(int i = 0; i < weaponList.Count; i++)
        {
            weaponSlots[i].gameObject.SetActive(true);
            weaponSlots[i].AddItem(weaponList[i]);
            equipmentSlots[i].gameObject.SetActive(true);
            equipmentSlots[i].AddItem(equipmentList[i]);
            consumableSlots[i].gameObject.SetActive(true);
            consumableSlots[i].AddItem(consumableList[i]);
        }
    }

    public void removeSlot()
    {
        for(int i = 0; i < weaponSlots.Length; i++) 
        {
            weaponSlots[i].RemoveItem();
            weaponSlots[i].gameObject.SetActive(false);
        }
    }
}
