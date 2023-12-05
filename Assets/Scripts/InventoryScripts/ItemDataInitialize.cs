using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ItemDataInitialize : MonoBehaviour
{

  
    public TextAsset ItemDatabase;
    public List<ItemData> AllItemList;
    // Start is called before the first frame update
    void Awake()
    {
        //ItemDataBase파일에서, 다 불러서, AllList에 넣고, 
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            string type = row[0];

            if (type == "UsableItem")
            {
                AllItemList.Add(new ItemData(row[0], row[1], int.Parse(row[2]), float.Parse(row[3]), float.Parse(row[4]), float.Parse(row[5]), float.Parse(row[6]), float.Parse(row[7])));
            }
            else if (type == "Equip")
            {
                AllItemList.Add(new ItemData(row[0], row[1], int.Parse(row[2]), row[3], float.Parse(row[4]), float.Parse(row[5]), float.Parse(row[6]), float.Parse(row[7])));
            }
            else if (type == "Food")	
            {
                AllItemList.Add(new ItemData(row[0], row[1], int.Parse(row[2]), float.Parse(row[3])));
            }
            else if (type == "Unusable")
            {
                AllItemList.Add(new ItemData(row[0], row[1], int.Parse(row[2])));
            }
            else { 
                                
            }

        }

        
    }


  
}
[System.Serializable]

public  class ItemData
{
    private string type, name;
    private int price;
    //usable
    private float damage;
    private float atkDelay;//공속
    private float reloadDelay;//장전
    private float bulletAmount;//탄창
    private float durability;//내구도
    //equip
    private string equipType;
    private float damage_up;
    private float hp_up;
    private float movespeed_up;
    private float fireRate_up;
    //food
    private float energy;


    public ItemData() { }
    public ItemData(string type, string name, int price)
    {
        this.type = type;
        this.name = name;
        this.price = price;

    }

    public ItemData(string type, string name, int price, float damage, float atkDelay, float reloadDelay, float bulletAmount, float durability) : this(type, name, price)
    {
        this.damage = damage;
        this.atkDelay = atkDelay;
        this.reloadDelay = reloadDelay;
        this.bulletAmount = bulletAmount;
        this.durability = durability;
    }

    public ItemData(string type, string name, int price, string equipType, float damage_up, float hp_up, float movespeed_up, float fireRate_up) : this(type, name, price)
    {
        this.equipType = equipType;
        this.damage_up = damage_up;
        this.hp_up = hp_up;
        this.movespeed_up = movespeed_up;
        this.fireRate_up = fireRate_up;
    }

    public ItemData(string type, string name, int price, float energy) : this(type, name, price)
    {
        this.energy = energy;
    }
    public string Type { get => type; set => type = value; }
    public string Name { get => name; set => name = value; }
    public int Price { get => price; set => price = value; }
    public float Damage { get => damage; set => damage = value; }
    public float AtkDelay { get => atkDelay; set => atkDelay = value; }
    public float ReloadDelay { get => reloadDelay; set => reloadDelay = value; }
    public float BulletAmount { get => bulletAmount; set => bulletAmount = value; }
    public float Durability { get => durability; set => durability = value; }
    public string EquipType { get => equipType; set => equipType = value; }
    public float Damage_up { get => damage_up; set => damage_up = value; }
    public float Hp_up { get => hp_up; set => hp_up = value; }
    public float Movespeed_up { get => movespeed_up; set => movespeed_up = value; }
    public float FireRate_up { get => fireRate_up; set => fireRate_up = value; }
    public float Energy { get => energy; set => energy = value; }

}
