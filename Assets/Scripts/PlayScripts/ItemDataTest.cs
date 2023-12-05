using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataTest : MonoBehaviour
{
    public string type; // "Weapon", "Consumable", "Etc"
    public string name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public ItemDataTest(string type, string name)
    {
        this.type = type;
        this.name = name;
    }
}
