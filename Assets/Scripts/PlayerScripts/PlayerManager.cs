using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    static public PlayerManager instance;
    public double money;
    public Transform moneyDisplay;
    public GameObject invnentroyManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SendToOtherScene() {

        moneyDisplay.transform.GetComponent<TextMeshProUGUI>().text = money.ToString();
        this.money = invnentroyManager.GetComponent<ItemManager>().money;

        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else { 
        DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        
    }
}
