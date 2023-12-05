using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject interactionButton;
    public GameObject escapeButton;
    public GameObject interactionPopupFactory;

    public bool isSuccess;

    

    //public bool isTriggering;
    //public DeadEnemy curDeadEnemy;

    // Start is called before the first frame updatess
    void Awake()
    {
        //playerManagement = GameObject.Find("PlayerManagement");

        instance = this;
        interactionButton.SetActive(false);
        escapeButton.SetActive(false);
        DontDestroyOnLoad(gameObject);

        //for(int i=0;i< playerManagement.GetComponent<PlayerManager>().MyBagItemList.Count; i++)
        //{
        //    Debug.Log(playerManagement.GetComponent<PlayerManager>().MyBagItemList[i].Name);
        //}
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void interactionButtonOnOff(bool flag)
    {
        interactionButton.SetActive(flag);
    }

    public void escapeButtonOnOff(bool flag)
    {
        escapeButton.SetActive(flag);
    }

    public void GoToFailResult()
    {

        isSuccess = false;
        SceneManager.LoadScene("ResultInterface");
        
    }

    public void GoToSuccessResult()
    {
        isSuccess = true;
        SceneManager.LoadScene("ResultInterface");
    }

    public void makeInteractionPopup()
    {
        GameObject interactionPopup = Instantiate(interactionPopupFactory);
        interactionPopup.SetActive(true);
        RectTransform rt = interactionPopup.GetComponent<RectTransform>();
        rt.SetParent(gameObject.transform);
        rt.anchoredPosition = new Vector3(-540, 0, 0);
        rt.SetSiblingIndex(6);
    }
}
