using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public Button mapBtn;
    // Start is called before the first frame update
    void Awake()
    {
        playerManager = GameObject.Find("PlayerManagement").GetComponent<PlayerManager>();
        SceneManager.sceneLoaded += delegate { loadManager(); };
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadManager()
    {

        playerManager.itemManager = GameObject.Find("ItemManagement");
        mapBtn.onClick.AddListener(delegate { playerManager.SceneChange(); });

    }
}
