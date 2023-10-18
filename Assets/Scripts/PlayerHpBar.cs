using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    private Slider hpBar;
    public Player player_sc;
    private Text hpText;
    // Start is called before the first frame update
    void Start()
    { 
        hpBar = GetComponentInChildren<Slider>();
        hpText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = player_sc.hp / player_sc.maxHp;
        hpText.text = player_sc.hp.ToString();
    }
}
