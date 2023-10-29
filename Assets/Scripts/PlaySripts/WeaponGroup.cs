using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WeaponGroup : MonoBehaviour
{
    public Scrollbar scrollbar;
    const int SIZE = 4;
    float[] pos = new float[SIZE]; // 각자 위치의 벨류값
    float distance; // pos들 간의 간격
    float targetPos;
    bool isDrag;

    // Start is called before the first frame update
    void Start()
    {
        distance = 1f / (SIZE - 1);
        for (int i = 0; i < SIZE; i++) pos[i] = distance * i;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDrag)
        {
            scrollbar.value = Mathf.Lerp(scrollbar.value, targetPos, 0.1f);
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
}
