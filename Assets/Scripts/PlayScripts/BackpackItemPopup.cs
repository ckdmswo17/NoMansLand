using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackpackItemPopup : MonoBehaviour
{
    public int index;
    public Button trashBtn;
    public Player player_sc;
    // Start is called before the first frame update
    void Start()
    {
        trashBtn = gameObject.transform.GetChild(5).GetComponent<Button>();
        player_sc = GameObject.Find("Player").GetComponent<Player>();
        //trashBtn.onClick.AddListener(delegate { player_sc.deleteGun(index); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selfDestroy()
    {
        Destroy(gameObject);
    }

    public void trashItem()
    {
        player_sc.deleteGun(index);
    }
}
