using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUIManager : MonoBehaviour
{
    public GameObject successText;
    public GameObject failText;
    private GameObject mainCanvas;
    private UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        mainCanvas = GameObject.Find("MainCanvas");
        uiManager = mainCanvas.GetComponent<UIManager>();
        mainCanvas.SetActive(false);
        if (uiManager.isSuccess)
        {
            successText.SetActive(true);
            failText.SetActive(false);
        }
        else
        {
            successText.SetActive(false);
            failText.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
