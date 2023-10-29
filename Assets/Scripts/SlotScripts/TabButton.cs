using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TabButton : MonoBehaviour
{
    public UnityEvent onTabSelected;
    public UnityEvent onTabUnselected;

    public void Select()
    {
        if (onTabSelected != null)
        {
            onTabSelected.Invoke();
        }
    }

    public void Deselect()
    {
        if(onTabUnselected != null)
        {
            onTabUnselected.Invoke();
        }
    }
    
    public void OnSelectedTab(TabButton button)
    {
        TapController.Instance.SelectedButton(button);
    }
}
