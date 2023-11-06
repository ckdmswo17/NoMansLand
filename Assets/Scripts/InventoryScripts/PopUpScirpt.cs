using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PopUpScirpt : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    public void PopUpInfo(ItemData _itemData)
    {

        GameObject popup = GameObject.Find("PopUp_Inventory");

        Transform _image = popup.transform.GetChild(0);
       


        Transform name = popup.transform.GetChild(1);
         name.GetChild(0).GetComponent<TextMeshProUGUI>().text ="이름 :"+ _itemData.name;
        Transform price = popup.transform.GetChild(2);
        price.GetChild(0).GetComponent<TextMeshProUGUI>().text ="가격 : "+_itemData.price.ToString();
        Transform descript = popup.transform.GetChild(3);

        switch (_itemData.type)
        {
            case "UsableItem": 
                UsableItem usableitem = (UsableItem)_itemData;
                descript.GetChild(0).GetComponent<TextMeshProUGUI>().text = "공격력: " + usableitem.Damage.ToString(); break;
            case "Equip":
                Equip equip = (Equip)_itemData;
                descript.GetChild(0).GetComponent<TextMeshProUGUI>().text = "방어력: " + equip.Hp_up.ToString(); break;
            case "Food":
               Food food = (Food)_itemData;
                descript.GetChild(0).GetComponent<TextMeshProUGUI>().text = "에너지: " + food.Value.ToString(); break;

        }

    }
}
