using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class BackpackGroup : MonoBehaviour
{

    public InvenTest invenTest;
    public List<GameObject> invenSlots;
    public GameObject backpackItemPopupFactory;
    public GameObject mainCanvas;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < invenSlots.Count; i++)
        {
            if (invenSlots[i].transform.childCount >= 1)
            {
                Destroy(invenSlots[i].transform.GetChild(0).gameObject);
            }
        }
        for (int i = 0; i < invenTest.backpackItemDatas.Count; i++)
        {
            
            if (invenTest.backpackItemDatas[i].name != null)
            {
                string name = invenTest.backpackItemDatas[i].name;
                
                GameObject go = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/PlayPrefabs/ImageGO/" + name + ".prefab", typeof(GameObject)));
                RectTransform rt = go.GetComponent<RectTransform>();
                if ((0 <= i) && (i < invenSlots.Count))
                {
                    rt.SetParent(invenSlots[i].transform);
                    if(invenTest.backpackItemDatas[i].type == "Weapon")
                    {
                        go.transform.position = invenSlots[i].transform.position - new Vector3(0, 20, 0);
                        rt.sizeDelta = new Vector2(120, 110); // 현재 아이콘 스프라이트는 웨펀 그룹용이라서 인벤토리 슬롯 사이즈에 맞게 조절 
                    } else
                    {

                    }
                    
                }
            }
            
        }
    }

    public void itemPopupOpen(int index)
    {
        if((0 <= index) && (index < invenSlots.Count))
        {
            if(invenSlots[index].transform.GetChild(0) != null)
            {
                GameObject backpackItemPopup = Instantiate(backpackItemPopupFactory);
                backpackItemPopup.SetActive(true);
                backpackItemPopup.GetComponent<BackpackItemPopup>().index = index;
                RectTransform rt = backpackItemPopup.GetComponent<RectTransform>();
                rt.SetParent(mainCanvas.transform);
                backpackItemPopup.transform.position = invenSlots[index].transform.position;
                rt.SetSiblingIndex(7);

                Sprite sp = invenSlots[index].transform.GetChild(0).GetComponent<Image>().sprite;
                string name = invenSlots[index].transform.GetChild(0).name.Replace("(Clone)", "");

                backpackItemPopup.transform.GetChild(2).GetComponent<Image>().sprite = sp;
                backpackItemPopup.transform.GetChild(3).GetComponent<Text>().text = name;
            }
        }
    }

    public void BackpackGroupUIUpdate()
    {
       
    }
}
