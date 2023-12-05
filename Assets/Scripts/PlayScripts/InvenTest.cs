using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenTest : MonoBehaviour
{
    public List<ItemDataTest> backpackItemDatas; 
    void Awake()
    {

        backpackItemDatas.Add(new ItemDataTest("Weapon", "Scar"));
        backpackItemDatas.Add(new ItemDataTest("Weapon", "Sniper"));
        backpackItemDatas.Add(new ItemDataTest("Weapon", "Pistol"));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
