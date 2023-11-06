using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
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
   // public TextAsset ItemDatabase;
    public List<ItemData> AllItemList, MyItemList, CurItemList;
    private string filePath = "/resource/MyItemText.txt";
    public string curType = "All";
    public GameObject[] Stash_Slot;
    public Image[] StashTabImage, StashItemImage;
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
       //AddStashImage();
        AddStashSlotListener();

    }

    public void AddStashSlotListener() {
        Transform stashContent = GameObject.Find("StashContent").transform;
        int count = stashContent.childCount;
        for (int i = 0; i < count; i++) {
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
    public void SlotClick(int slotNum) {
        // 해당아이템 자료를 팝업  오브젝트에 넘겨준다. 
        ItemData CurItem = CurItemList[slotNum];
        Debug.Log(CurItem.name);
        GameObject.Find("PopUp_Inventory").SetActive(true);
        
       // popup.GetComponent<PopUpScirpt>().PopUpInfo(CurItem);
        
    }
    public void StashTabClick(string tabName)
    {
        
        curType = tabName;
        if (tabName == "All")
        {
            CurItemList = MyItemList;
        }
        else
        {
            CurItemList = MyItemList.FindAll(x => x.type == tabName);
        }


        for (int i = 0; i < Stash_Slot.Length; i++)
         {
           bool isExist = i < CurItemList.Count;
            Stash_Slot[i].SetActive(isExist);
            Stash_Slot[i].GetComponentInChildren<TextMeshProUGUI>().text = isExist ?  CurItemList[i].name : "";

            if (isExist)
            {
                StashItemImage[i].sprite = ItemSprite[AllItemList.FindIndex(x => x.name == CurItemList[i].name)];
            }
       
        }
        // 탭 반전이미지.
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
        StashTabClick(curType);
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
