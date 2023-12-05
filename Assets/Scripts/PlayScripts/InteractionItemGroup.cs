using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class InteractionItemGroup : MonoBehaviour
{
    public GameObject content;
    public GameObject interactionItemFactory;


    //public UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        //uiManager = GameObject.Find("MainCanvas").GetComponent<UIManager>();
        //Debug.Log("awake uimanager : " + uiManager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void interactionPopupOpen(UIManager uiManager)
    //{
    //    Debug.Log("interaction uimanager:" + uiManager);
    //    Debug.Log("interaction uimanager is Trigger:" + uiManager.isTriggering);
    //    if (uiManager != null && uiManager.isTriggering)
    //    {
    //        int yPos = 0;
    //        Debug.Log(uiManager.curDeadEnemy.enemyInventory + " 길이 입니다");
    //        for(int i = 0; i < uiManager.curDeadEnemy.enemyInventory.itemDatas.Count; i++)
    //        {
    //            Debug.Log(i+"번입니다");
    //            GameObject go = Instantiate(interactionItemFactory);
    //            go.SetActive(true);
    //            RectTransform rt = go.GetComponent<RectTransform>();
    //            rt.SetParent(content.transform);
    //            rt.anchoredPosition = new Vector3(0, yPos, 0);

    //            go.transform.GetChild(0).GetComponent<Text>().text = uiManager.curDeadEnemy.enemyInventory.itemDatas[i].name;
    //            GameObject imageObject = go.transform.GetChild(1).gameObject;
    //            imageObject = ((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/PlayPrefabs/ImageGO/" + name + ".prefab", typeof(GameObject)));
    //            imageObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-150,0,0);
    //            imageObject.GetComponent<RectTransform>().sizeDelta = new Vector3(100, 100);


    //            yPos -= 160;


    //        }

    //    }
    //}
}
