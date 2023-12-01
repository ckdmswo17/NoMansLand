using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapScrollManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Scrollbar scrollbar;
    public Transform contentTr;

    const int SIZE = 4;
    float[] pos = new float[SIZE];
    float distance, curPos, targetPos;
    bool isDrag;
    int targetIndex;


    void Start()
    {
        distance = 1f / (SIZE - 1);
        for (int i = 0; i < SIZE; i++)
        {
            pos[i] = distance * i;
        }

    }

    float SetPos()
    {
        for (int i = 0; i < SIZE; i++)
        {
            if (scrollbar.value < pos[i] + distance * 0.5f && scrollbar.value > pos[i] - distance * 0.5f)
            {
                targetIndex = i;
                return pos[i];
            }
        }
        return 0f;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        curPos = SetPos();
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDrag = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        targetPos = SetPos();

        //���ݰŸ��� ���� �ʾƵ� ���콺�� ������ �̵��ϸ�
        if (curPos == targetPos)
        {
            //��ũ���� �������� ������ �̵��� ��ǥ�� �ϳ� ����
            if (eventData.delta.x > 18 && curPos - distance >= 0)
            {
                --targetIndex;
                targetPos = curPos - distance;
            }
            //��ũ���� ���������� ������ �̵��� ��ǥ�� �ϳ� ����
            else if (eventData.delta.x < -18 && curPos + distance < 1.01f)
            {
                ++targetIndex;
                targetPos = curPos + distance;
            }
        }

        for (int i = 0; i < SIZE; ++i)
        {
            if (contentTr.GetChild(i).GetComponent<ScrollScript>() && curPos != pos[i] && targetPos == pos[i])
            {
                contentTr.GetChild(i).GetChild(1).GetComponent<Scrollbar>().value = 1;
            }
        }
    }

    void Update()
    {
        if (!isDrag)
        {
            scrollbar.value = Mathf.Lerp(scrollbar.value, targetPos, 0.1f);
        }
    }

    public void TabClick(int n)
    {
        targetIndex = n;
        targetPos = pos[n];
    }
}
