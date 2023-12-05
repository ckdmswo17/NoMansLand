using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    static public PlayerManager instance;
    public double money;
    public GameObject itemManager;
    public List<ItemData> MyBagItemList;
    public ItemData[] MyEquipItemList;

    // Start is called before the first frame update
    void Start()
    {
        
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(itemManager);
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    public void AcceptData() {
        this.money = itemManager.GetComponent<ItemManager>().money;
        this.MyBagItemList = itemManager.GetComponent<ItemManager>().MyBagItemList;
        this.MyEquipItemList = itemManager.GetComponent<ItemManager>().MyEquipItemList;

    }

    public void SceneChange()
    {
        AcceptData();
        Debug.Log(MyBagItemList.Count);
        SceneManager.LoadScene("PlayScene");
    }
}
