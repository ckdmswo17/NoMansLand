using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapController : MonoBehaviour
{
    private static TapController instance = null;

    public static TapController Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject.FindObjectOfType<TapController>();
            }
            return instance;
        }
    }

    TabButton tabButton;

    // Start is called before the first frame update
    void Start()
    {
       SelectedButton(transform.GetChild(0).GetComponent<TabButton>());
    }

    // Update is called once per frame
    public void SelectedButton(TabButton button)
    {
        if(tabButton != null)
        {
            tabButton.Deselect();
        }

        tabButton = button;
        tabButton.Select();
        Debug.Log("change");
    }
}
