using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class WeaponGroup : MonoBehaviour
{
    public Scrollbar scrollbar;
    const int SIZE = 4;
    float[] pos = new float[SIZE]; // 각자 위치의 벨류값
    float distance; // pos들 간의 간격
    float targetPos = 1;
    bool isDrag;
    public List<GameObject> weaponSlots;

    public List<ItemData> myBackItemList;

    // Start is called before the first frame update
    void Start()
    {
        distance = 1f / (SIZE - 1);
        for (int i = 0; i < SIZE; i++) pos[i] = distance * i;

        myBackItemList = GameObject.Find("PlayerManagement").GetComponent<PlayerManager>().MyBagItemList;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isDrag)
        {
            scrollbar.value = Mathf.Lerp(scrollbar.value, targetPos, 0.1f);
        }

        for (int i = 0; i < weaponSlots.Count; i++)
        {
            if(weaponSlots[i].transform.childCount >= 1)
            {
                Destroy(weaponSlots[i].transform.GetChild(0).gameObject);
            }   
        }
        int count = 0;
        
        for (int i = 0; i < myBackItemList.Count; i++)
        {
            if(myBackItemList[i].Name != null && myBackItemList[i].Type == "UsableItem")
            {
                string name = myBackItemList[i].Name;
                if ((0 <= count) && (count < weaponSlots.Count))
                {
                    GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/ImageGO/" + name));
                    RectTransform rt = go.GetComponent<RectTransform>();
                    go.transform.position = weaponSlots[count].transform.position;
                    rt.SetParent(weaponSlots[count].transform);
                }
                count++;
            }  

        }

    }

    public void OnDrag()
    {
        isDrag = true;
    }

    public void OnEndDrag()
    {
        isDrag = false;
        for(int i = 0; i < SIZE; i++)
        {
            if (scrollbar.value < pos[i] + distance * 0.5f && scrollbar.value > pos[i] - distance * 0.5f)
            {
                targetPos = pos[i];
            }
        }
    }

    public void weaponGroupUIUpdate()
    {
        
        
    }
}
