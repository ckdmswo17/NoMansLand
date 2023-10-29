using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI itemPrice;
    public Popup popup; // 팝업 스크립트 연결

    public void AddItem(Item _item)
    {
        itemIcon.sprite = _item.itemIcon;
        itemPrice.text = _item.itemPrice.ToString();
    }

    public void RemoveItem()
    {
        itemIcon.sprite = null;
        itemPrice.text = null;
    }

}
