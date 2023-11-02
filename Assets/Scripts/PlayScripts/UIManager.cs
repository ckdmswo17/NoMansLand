using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject interactionButton;
    // Start is called before the first frame update
    void Start()
    {
        interactionButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void interactionButtonOnOff(bool flag)
    {
        interactionButton.SetActive(flag);
    }
}
