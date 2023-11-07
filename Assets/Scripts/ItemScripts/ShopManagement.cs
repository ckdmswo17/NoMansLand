using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
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
    public GameObject itemSource,popUp;
  
    public List<ItemData> AllItemList, MyItemList, CurItemList;
    private string filePath = "/Resource/SlotIcon/ShopItem.txt";
    public string curType = "UsableItem";
    public GameObject[] Slot;
    public Image[] TabImage, ItemImage;
    public Sprite TabIdleSprite, TabSelectSprite;
    public Sprite[] ItemSprite;

    // Start is called before the first frame update
    void Start()
    {
        // SourceItem 에서, ItemBase에 접근해, 받아옴.
        itemSource = GameObject.Find("SourceItem");
        this.AllItemList = itemSource.GetComponent<ItemDataInitialize>().AllItemList;
        //ItemDataBase파일에서, 다 불러서, AllList에 넣고, 
        // string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n'); 
        /* for (int i = 0; i < line.Length; i++)
         {
             string[] row = line[i].Split('\t');

             AllItemList.Add(new Item(row[0], int.Parse(row[1]), row[2], int.Parse(row[3]), int.Parse(row[4]), int.Parse(row[5])));

         }*/

        Save();
        Load();
       // AddSlotListener();

    }

    public void AddSlotListener()
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

    public void SlotClick(int slotNum)
    {
        ItemData CurItem = CurItemList[slotNum];
        Debug.Log("Clicked SlotNum: " + CurItem.name);

        popUp.transform.GetChild(1).GetComponent<Image>().sprite = Slot[slotNum].transform.GetChild(1).GetComponent<Image>().sprite;
        popUp.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "이름: " + AllItemList[slotNum].name;
        popUp.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "가격: " + AllItemList[slotNum].price.ToString();
    }
    public void TabClick(string tabName)
    {

        curType = tabName;
        if (tabName == "UsableItem")
        {
            CurItemList = MyItemList;
        }
        else
        {
            CurItemList = MyItemList.FindAll(x => x.type == tabName);
        }


        for (int i = 0; i < Slot.Length; i++)
        {
            bool isExist = i < CurItemList.Count;
            Slot[i].SetActive(isExist);
            Slot[i].GetComponentInChildren<TextMeshProUGUI>().text = isExist ? CurItemList[i].price.ToString() : "";

            if (isExist)
            {
                ItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.name == CurItemList[i].name)];
            }

        }
        // 탭 이미지
        int tabNum = 0;
        switch (tabName)
        {
            case "UsableItem": tabNum = 0; break;
            case "Equip": tabNum = 1; break;
            case "Food": tabNum = 2; break;
        }
        for (int i = 0; i < TabImage.Length; i++)
            TabImage[i].sprite = i == tabNum ? TabSelectSprite : TabIdleSprite;


    }




    void Save()
    {
        string jdata = ConvertListToJson(AllItemList);
        print(jdata);
        File.WriteAllText(Application.dataPath + filePath, jdata);
    }
    void Load()
    {
        string jdata = File.ReadAllText(Application.dataPath + filePath);
        MyItemList = ConvertJsonToList<ItemData>(jdata);
        TabClick(curType);
    }









    // List 변수를 JSON 포맷으로 직렬화하는 함수
    public string ConvertListToJson<T>(List<T> myList)
    {
        string json = JsonConvert.SerializeObject(myList);
        return json;
    }

    // JSON 포맷을 List 변수로 역직렬화하는 함수
    public List<T> ConvertJsonToList<T>(string json)
    {
        List<T> myList = JsonConvert.DeserializeObject<List<T>>(json);
        return myList;
    }
}