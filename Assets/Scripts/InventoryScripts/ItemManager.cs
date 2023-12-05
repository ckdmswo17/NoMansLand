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
    public GameObject popUp_Bag,popUp_Stash,popUp_Shop,popUp_Equip;
    public GameObject player; 
    // public TextAsset ItemDatabase;
    public List<ItemData> AllItemList, MyStashItemList, CurStashItemList;
    public List<ItemData> MyBagItemList;
    public List<ItemData> ShopItemList;
    public List<ItemData> CurShopItemList;
    public List<ItemData> MyEquipItemList;
    private string filePath = "/MyItemText.txt";
    public string curType = "All";
    public string curShopType = "UsableItem";
    public GameObject[] Stash_Slot, Bag_Slot,Shop_Slot, Equip_Slot;
    public GameObject[] MapBag_Slot;
    public Image[] StashTabImage, ShopTabImage, StashItemImage, BagItemImage,ShopItemImage,MapItemImage, EquipItemImage;
    public Sprite TabIdleSprite, TabSelectSprite;
    public Sprite[] ItemSprite;
    public ItemData CurItem,CurBagItem, CurShopItem, CurEquipItem;
    public double money = 10000;
    public TextMeshProUGUI moneyDisplay;
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
    private void Update()
    {
        moneyDisplay.transform.GetComponent<TextMeshProUGUI>().text = money.ToString();
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
        Debug.Log(CurItem.Name);

        // popUp.GetComponent<PopUpScirpt>().PopUpInfo(CurItem);
        StashPopUpInfo(slotNum);
        // popUp.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "???? :" + CurItem.name;
        //  popUp.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "???? :" + CurItem.price;


        //popUp.transform.GetChild(1).GetComponent<Image>().sprite = Stash_Slot[slotNum].transform.GetChild(0).GetComponent<Image>().sprite;

    }
    public void BagSlotClick(int slotNum)
    {
        CurBagItem = MyBagItemList[slotNum];
        Debug.Log(CurBagItem.Name);

        if (CurBagItem.Type != "Equip")
        {
            popUp_Bag.transform.GetChild(6).gameObject.SetActive(false);
        }
        else
        {
            popUp_Bag.transform.GetChild(6).gameObject.SetActive(true);
        }

        // popUp.GetComponent<PopUpScirpt>().PopUpInfo(CurItem);
        popUp_Bag.transform.GetChild(1).GetComponent<Image>().sprite = Bag_Slot[slotNum].transform.GetChild(0).GetComponent<Image>().sprite;


        popUp_Bag.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Name :" + CurBagItem.Name;

        popUp_Bag.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Price : " + CurBagItem.Price;

        switch (CurBagItem.Type)
        {
            case "UsableItem":

                popUp_Bag.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Damage: " + CurBagItem.Damage.ToString(); break;
            case "Equip":
                switch (CurBagItem.EquipType)
                {
                    case "Head":
                        popUp_Bag.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Damage Up: " + CurBagItem.Damage_up.ToString(); break;
                    case "Body":
                        popUp_Bag.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "HP Up: " + CurBagItem.Hp_up.ToString(); break;
                    case "Hand":
                        popUp_Bag.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "FireRate Up: " + CurBagItem.FireRate_up.ToString() + "%"; break;
                    case "Foot":
                        popUp_Bag.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "MoveSpeed Up: " + CurBagItem.Movespeed_up.ToString(); break;
                }
                break;
            case "Food":

                popUp_Bag.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Energy: " + CurBagItem.Energy.ToString(); break;
            case "Unusable":

                popUp_Bag.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = ""; break;

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
    public void MoveStashToEquip()
    {

    }

    public void MoveEquipToStash()
    {
        MyStashItemList.Add(CurEquipItem);
        MyEquipItemList.Remove(CurEquipItem);
    }

    public void StashPopUpInfo(int slotNum)
    {
        popUp_Stash.transform.GetChild(1).GetComponent<Image>().sprite = Stash_Slot[slotNum].transform.GetChild(0).GetComponent<Image>().sprite;
        if(CurItem.Type != "Equip")
        {
            popUp_Stash.transform.GetChild(6).gameObject.SetActive(false);
        }
        else
        {
            popUp_Stash.transform.GetChild(6).gameObject.SetActive(true);
        }

        popUp_Stash.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Name  :" + CurItem.Name;

        popUp_Stash.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Price  : " + CurItem.Price;

        switch (CurItem.Type)
        {
            case "UsableItem":

                popUp_Stash.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Damage: " + CurItem.Damage.ToString(); break;
            case "Equip":
                switch (CurItem.EquipType)
                {
                    case "Head":
                        popUp_Stash.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Damage Up: " + CurItem.Damage_up.ToString(); break;
                    case "Body":
                        popUp_Stash.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "HP Up: " + CurItem.Hp_up.ToString(); break;
                    case "Hand":
                        popUp_Stash.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "FireRate Up: " + CurItem.FireRate_up.ToString() + "%"; break;
                    case "Foot":
                        popUp_Stash.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "MoveSpeed Up: " + CurItem.Movespeed_up.ToString(); break;
                }
                break;
            case "Food":

                popUp_Stash.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Energy: " + CurItem.Energy.ToString(); break;
            case "Unusable":

                popUp_Stash.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = ""; break;

        }
    }


    public void BagView() 
    {
        for (int i = 0; i < Bag_Slot.Length; i++)
        {
            bool isExist = i < MyBagItemList.Count;
            Bag_Slot[i].SetActive(isExist);
            Bag_Slot[i].GetComponentInChildren<TextMeshProUGUI>().text = isExist ? MyBagItemList[i].Name : "";

            MapBag_Slot[i].GetComponentInChildren<TextMeshProUGUI>().text = isExist ? MyBagItemList[i].Name : "";

            if (isExist)
            {
                BagItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == MyBagItemList[i].Name)];

                MapItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == MyBagItemList[i].Name)];
            }

        }
      

    }

    public void ShopSlotClick(int slotNum)
    {
        CurShopItem = CurShopItemList[slotNum];
        Debug.Log("Clicked SlotNum: " + CurItem.Name);

        popUp_Shop.transform.GetChild(1).GetComponent<Image>().sprite = Shop_Slot[slotNum].transform.GetChild(1).GetComponent<Image>().sprite;
        popUp_Shop.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Name: " + CurShopItem.Name;
        popUp_Shop.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Price: " + CurShopItem.Price.ToString();

        switch (CurShopItem.Type)
        {
            case "UsableItem":
                popUp_Shop.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Damage: " + CurShopItem.Damage.ToString(); break;
            case "Equip":
                switch(CurShopItem.EquipType)
                {
                    case "Head":
                        popUp_Shop.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Damage Up: " + CurShopItem.Damage_up.ToString(); break;
                    case "Body":
                        popUp_Shop.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "HP Up: " + CurShopItem.Hp_up.ToString(); break;
                    case "Hand":
                        popUp_Shop.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "FireRate Up: " + CurShopItem.FireRate_up.ToString() + "%"; break;
                    case "Foot":
                        popUp_Shop.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "MoveSpeed Up: " + CurShopItem.Movespeed_up.ToString(); break;
                } break;
            case "Food":
                popUp_Shop.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Energy: " + CurShopItem.Energy.ToString(); break;
            case "Unusable":
                popUp_Shop.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = ""; break;

        }
    }

    public void EquipSlotClick(int slotNum)
    {
        if (MyEquipItemList[slotNum] == null)
        {
            popUp_Equip.gameObject.SetActive(false);
        }
        else
        {
            CurEquipItem = MyEquipItemList[slotNum];
            Debug.Log("Clicked SlotNum: " + CurEquipItem.Name);

            popUp_Equip.transform.GetChild(1).GetComponent<Image>().sprite = Shop_Slot[slotNum].transform.GetChild(1).GetComponent<Image>().sprite;
            popUp_Equip.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Name: " + CurEquipItem.Name;
            popUp_Equip.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Price: " + CurEquipItem.Price.ToString();

            switch (CurShopItem.Type)
            {
                case "UsableItem":
                    popUp_Equip.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Damage: " + CurEquipItem.Damage.ToString(); break;
                case "Equip":
                    switch (CurShopItem.EquipType)
                    {
                        case "Head":
                            popUp_Equip.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Damage Up: " + CurEquipItem.Damage_up.ToString(); break;
                        case "Body":
                            popUp_Equip.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "HP Up: " + CurEquipItem.Hp_up.ToString(); break;
                        case "Hand":
                            popUp_Equip.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "FireRate Up: " + CurEquipItem.FireRate_up.ToString() + "%"; break;
                        case "Foot":
                            popUp_Equip.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "MoveSpeed Up: " + CurEquipItem.Movespeed_up.ToString(); break;
                    }
                    break;
                case "Food":
                    popUp_Equip.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Energy: " + CurEquipItem.Energy.ToString(); break;
                case "Unusable":
                    popUp_Equip.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = ""; break;
            }
        }

    }
    public void BuyItem(int slotNum)
    {
        if(money >= CurShopItem.Price)
        {
            MyStashItemList.Add(CurShopItem);
            StashTabClick(curType);
            money -= CurShopItem.Price;
        }
        else
        {
            Debug.Log("µ·¾ø¾î");
        }

    }
    public void SellStashItem()
    {
        MyStashItemList.Remove(CurItem);
        money += CurItem.Price;
        StashTabClick(curType);
        
    }
    public void SellBagItem()
    {
        MyBagItemList.Remove(CurBagItem);
        money += CurBagItem.Price;
        BagView();
    }

    public void ShopTabClick(string tabName)
    {

        curShopType = tabName;
       
       
        CurShopItemList = ShopItemList.FindAll(x => x.Type == tabName);
        

        for (int i = 0; i < Shop_Slot.Length; i++)
        {
            bool isExist = i < CurShopItemList.Count;
            Shop_Slot[i].SetActive(isExist);
            Shop_Slot[i].GetComponentInChildren<TextMeshProUGUI>().text = isExist ? CurShopItemList[i].Price.ToString() : "";

            if (isExist)
            {
                ShopItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == CurShopItemList[i].Name)];
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
            CurStashItemList = MyStashItemList.FindAll(x => x.Type == tabName);
        }


        for (int i = 0; i < Stash_Slot.Length; i++)
        {
            bool isExist = i < CurStashItemList.Count;
            Stash_Slot[i].SetActive(isExist);
            Stash_Slot[i].GetComponentInChildren<TextMeshProUGUI>().text = isExist ? CurStashItemList[i].Name : "";

            if (isExist)
            {
                StashItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == CurStashItemList[i].Name)];
            }

        }
       
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

    public void EquipTabClick(string tabName)
    {
        for (int i = 0; i < Equip_Slot.Length; i++)
        {
            bool isExist = i < MyEquipItemList.Count;
            Equip_Slot[i].SetActive(isExist);
            Equip_Slot[i].GetComponentInChildren<TextMeshProUGUI>().text = isExist ? MyEquipItemList[i].Name : "";

            if (isExist)
            {
                EquipItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.Name == MyEquipItemList[i].Name)];
            }

        }
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
