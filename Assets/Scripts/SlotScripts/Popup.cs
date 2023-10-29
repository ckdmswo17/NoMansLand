using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI itemPrice;

    public void AddItem(ItemSlot itemSlot)
    {
        itemIcon.sprite = itemSlot.itemIcon.sprite;
        itemPrice.text = itemSlot.itemPrice.ToString();
    }

    public void RemoveItem()
    {
        itemIcon.sprite = null;
        itemPrice.text = null;
    }
}
