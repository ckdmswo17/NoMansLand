using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{

    /*  [System.Serializable]
      public class Item
      {
          public string type,name;
          public int battle_type, durability, damage, perUsability;
          public Item(string _type, int _battle_type, string _name, int _durability, int _damage, int _perUsability)
          {
              type = _type; battle_type = _battle_type; name = _name; durability = _durability; damage = _damage; perUsability = _perUsability;
          }
      }*/
    public GameObject itemSource;
    public GameObject popUp_Bag,popUp_Stash,popUp_Shop;
    public GameObject player;
    // public TextAsset ItemDatabase;
    public List<ItemData> AllItemList, MyStashItemList, CurStashItemList;
    public List<ItemData> MyBagItemList;
    public List<ItemData> ShopItemList;
    public List<ItemData> CurShopItemList;
    private string filePath = "/MyItemText.txt";
    public string curType = "All";
    public string curShopType = "UsableItem";
    public GameObject[] Stash_Slot, Bag_Slot,Shop_Slot;
    public GameObject[] MapBag_Slot;
    public Image[] StashTabImage, ShopTabImage, StashItemImage, BagItemImage,ShopItemImage,MapItemImage;
    public Sprite TabIdleSprite, TabSelectSprite;
    public Sprite[] ItemSprite;
    public ItemData CurItem,CurBagItem, CurShopItem;
    public double money;
    // Start is called before the first frame update
    void Start()
    {
        // SourceItem ????, ItemBase?? ??????, ??????.
        itemSource = GameObject.Find("SourceItem");
       
        this.AllItemList = itemSource.GetComponent<ItemDataInitialize>().AllItemList;

        //ItemDataBase????????, ?? ??????, AllList?? ????, 
        // string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n'); 
        /* for (int i = 0; i < line.Length; i++)
         {
             string[] row = line[i].Split('\t');

             AllItemList.Add(new Item(row[0], int.Parse(row[1]), row[2], int.Parse(row[3]), int.Parse(row[4]), int.Parse(row[5])));

         }*/
        

        Save();
        Load();
        //AddStashImage();
        //AddStashSlotListener();

    }

    public void AddStashSlotListener()
    {
        Transform stashContent = GameObject.Find("StashContent").transform;
        int count = stashContent.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform btn = stashContent.GetChild(i);
            int index = i;
            btn.GetComponent<Button>().onClick.AddListener(() => SlotClick(index));
        }
    }
    public void AddStashImage()
    {
        Transform stashContent = GameObject.Find("StashContent").transform;
        int count = stashContent.childCount;
        for (int i = 0; i < count; i++)
        {
            GameObject btn = stashContent.GetChild(i).gameObject;
            int index = i;

        }
    }
    public void SlotClick(int slotNum)
    {
        CurItem = CurStashItemList[slotNum];
        Debug.Log(CurItem.name);

        // popUp.GetComponent<PopUpScirpt>().PopUpInfo(CurItem);
        StashPopUpInfo(CurItem, slotNum);
        // popUp.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "???? :" + CurItem.name;
        //  popUp.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "???? :" + CurItem.price;


        //popUp.transform.GetChild(1).GetComponent<Image>().sprite = Stash_Slot[slotNum].transform.GetChild(0).GetComponent<Image>().sprite;

    }
    public void BagSlotClick(int slotNum)
    {
        CurBagItem = MyBagItemList[slotNum];
        Debug.Log(CurBagItem.name);

        // popUp.GetComponent<PopUpScirpt>().PopUpInfo(CurItem);
        popUp_Bag.transform.GetChild(1).GetComponent<Image>().sprite = Bag_Slot[slotNum].transform.GetChild(0).GetComponent<Image>().sprite;


        popUp_Bag.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Name :" + CurBagItem.name;

        popUp_Bag.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Price : " + CurBagItem.price;

        switch (CurBagItem.type)
        {
            case "UsableItem":

                popUp_Bag.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = "Damage: " + CurBagItem.Damage.ToString(); break;
            case "Equip":

                popUp_Bag.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = "Def: " + CurBagItem.Hp_up.ToString(); break;
            case "Food":

                popUp_Bag.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = "Energy: " + CurBagItem.Energy.ToString(); break;
            case "Unusable":

                popUp_Bag.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = ""; break;

        }

    }

    public void MoveToBag()
    {

        MyBagItemList.Add(CurItem);
        MyStashItemList.Remove(CurItem);

        StashTabClick(curType); //stash ??????
        BagView(); // bag ??????
    }
    public void MoveToStash()
    {
        MyStashItemList.Add(CurBagItem);
        MyBagItemList.Remove(CurBagItem);


        StashTabClick(curType); //stash ??????
        BagView(); // bag ??????
    }
    public void StashPopUpInfo(ItemData _itemData, int slotNum)
    {
        popUp_Stash.transform.GetChild(1).GetComponent<Image>().sprite = Stash_Slot[slotNum].transform.GetChild(0).GetComponent<Image>().sprite;


        popUp_Stash.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Name  :" + _itemData.name;

        popUp_Stash.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Price  : " + _itemData.price;

        switch (_itemData.type)
        {
            case "UsableItem":

                popUp_Stash.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = "Damage: " + _itemData.Damage.ToString(); break;
            case "Equip":

                popUp_Stash.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = "Def: " + _itemData.Hp_up.ToString(); break;
            case "Food":

                popUp_Stash.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = "Energy: " + _itemData.Energy.ToString(); break;
            case "Unusable":

                popUp_Stash.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = ""; break;

        }



    }

    public void BagView() 
    {
        for (int i = 0; i < Bag_Slot.Length; i++)
        {
            bool isExist = i < MyBagItemList.Count;
            Bag_Slot[i].SetActive(isExist);
            Bag_Slot[i].GetComponentInChildren<TextMeshProUGUI>().text = isExist ? MyBagItemList[i].name : "";

            MapBag_Slot[i].GetComponentInChildren<TextMeshProUGUI>().text = isExist ? MyBagItemList[i].name : "";

            if (isExist)
            {
                BagItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.name == MyBagItemList[i].name)];

                MapItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.name == MyBagItemList[i].name)];
            }

        }
      

    }

    public void ShopSlotClick(int slotNum)
    {
        CurShopItem = CurShopItemList[slotNum];
        Debug.Log("Clicked SlotNum: " + CurItem.name);

        popUp_Shop.transform.GetChild(1).GetComponent<Image>().sprite = Shop_Slot[slotNum].transform.GetChild(1).GetComponent<Image>().sprite;
        popUp_Shop.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Name: " + CurShopItem.name;
        popUp_Shop.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Price: " + CurShopItem.price.ToString();

        switch (CurShopItem.type)
        {
            case "UsableItem":

                popUp_Shop.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Damage: " + CurShopItem.Damage.ToString(); break;
            case "Equip":

                popUp_Shop.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Def: " + CurShopItem.Hp_up.ToString(); break;
            case "Food":

                popUp_Shop.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Energy: " + CurShopItem.Energy.ToString(); break;
            case "Unusable":

                popUp_Shop.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = ""; break;

        }
    }

    public void BuyItem(int slotNum)
    {


        MyStashItemList.Add(CurShopItem);
        StashTabClick(curType);
    }

    public void ShopTabClick(string tabName)
    {

        curType = tabName;
       
       
        CurShopItemList = ShopItemList.FindAll(x => x.type == tabName);
        

        for (int i = 0; i < Shop_Slot.Length; i++)
        {
            bool isExist = i < CurShopItemList.Count;
            Shop_Slot[i].SetActive(isExist);
            Shop_Slot[i].GetComponentInChildren<TextMeshProUGUI>().text = isExist ? CurShopItemList[i].price.ToString() : "";

            if (isExist)
            {
                ShopItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.name == CurShopItemList[i].name)];
            }

        }
        // ?? ??????
        int tabNum = 0;
        switch (tabName)
        {
            case "UsableItem": tabNum = 0; break;
            case "Equip": tabNum = 1; break;
            case "Food": tabNum = 2; break;
        }
        for (int i = 0; i < ShopTabImage.Length; i++)
            ShopTabImage[i].sprite = i == tabNum ? TabSelectSprite : TabIdleSprite;


    }

    public void StashTabClick(string tabName)
    {

        curType = tabName;
        if (tabName == "All")
        {
            CurStashItemList = MyStashItemList;
        }
        else
        {
            CurStashItemList = MyStashItemList.FindAll(x => x.type == tabName);
        }


        for (int i = 0; i < Stash_Slot.Length; i++)
        {
            bool isExist = i < CurStashItemList.Count;
            Stash_Slot[i].SetActive(isExist);
            Stash_Slot[i].GetComponentInChildren<TextMeshProUGUI>().text = isExist ? CurStashItemList[i].name : "";

            if (isExist)
            {
                StashItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.name == CurStashItemList[i].name)];
            }

        }
        // ?? ??????????.
        int tabNum = 0;
        switch (tabName)
        {
            case "All": tabNum = 0; break;
            case "UsableItem": tabNum = 1; break;
            case "Equip": tabNum = 2; break;
            case "Food": tabNum = 3; break;
        }
        for (int i = 0; i < StashTabImage.Length; i++)
            StashTabImage[i].sprite = i == tabNum ? TabSelectSprite : TabIdleSprite;


    }




    void Save() // allItemlist(??????, ????????(db)???? ??????.)
    {
        string jdata = ConvertListToJson(AllItemList);
        print(jdata);
        File.WriteAllText(Application.streamingAssetsPath + filePath, jdata); //??????, allItem?? ?????????? ?????? ??. ???? save()?? ??????????. allItem?? ????, 
    }

    // ????????, initialSave(initialItemList) / ??????, Save() {myItemList,} , AllItemList?? ???? ?????????? ????????.. ?????? id?????? ????????.
    void Load() // ?????????????? ??????. 
    {
        string jdata = File.ReadAllText(Application.streamingAssetsPath + filePath);
        MyStashItemList = ConvertJsonToList<ItemData>(jdata);
        ShopItemList = ConvertJsonToList<ItemData>(jdata);
        StashTabClick(curType);
        ShopTabClick(curShopType);
    }









    // List ?????? JSON ???????? ?????????? ????
    public string ConvertListToJson<T>(List<T> myList)
    {
        string json = JsonConvert.SerializeObject(myList);
        return json;
    }

    // JSON ?????? List ?????? ???????????? ????
    public List<T> ConvertJsonToList<T>(string json)
    {
        List<T> myList = JsonConvert.DeserializeObject<List<T>>(json);
        return myList;
    }
}
