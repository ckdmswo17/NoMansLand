using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;

    public GameObject interactionButton;
    public GameObject escapeButton;

    public bool isSuccess;
 
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (instance == null)
        {
            Destroy(gameObject);
        }
        interactionButton.SetActive(false);
        escapeButton.SetActive(false);
        DontDestroyOnLoad(gameObject);


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
}
