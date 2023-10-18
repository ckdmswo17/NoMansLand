using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    private Slider hpBar;
    public Enemy enemy_sc;
    //private Text hpText;
    // Start is called before the first frame update
    void Start()
    {
        hpBar = GetComponentInChildren<Slider>();
        //hpText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = enemy_sc.hp / enemy_sc.maxHp;
        //hpText.text = enemy_sc.hp.ToString();
    }
}
