using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BackpackGroup : MonoBehaviour
{

    public InvenTest invenTest;
    public List<GameObject> invenSlots;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log(invenTest.backpackItemNames.Capacity);
        for (int i = 0; i < invenTest.backpackItemNames.Capacity; i++)
        {
            Debug.Log(i);
            string name = invenTest.backpackItemNames[i];
            GameObject go = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/PlayPrefabs/ImageGO/" + name + ".prefab", typeof(GameObject)));
            RectTransform rt = go.GetComponent<RectTransform>();
            if ((0 <= i) && (i < invenSlots.Capacity))
            {
                rt.SetParent(invenSlots[i].transform);
                go.transform.position = invenSlots[i].transform.position - new Vector3(0,20,0); 
                rt.sizeDelta = new Vector2(120,110); // 현재 아이콘 스프라이트는 웨펀 그룹용이라서 인벤토리 슬롯 사이즈에 맞게 조절 
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
