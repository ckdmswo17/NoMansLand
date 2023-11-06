using System.Collections.Generic;
using UnityEngine;


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
                AllItemList.Add(new UsableItem(row[0], row[1], int.Parse(row[2]), int.Parse(row[3]), int.Parse(row[4]), int.Parse(row[5]), int.Parse(row[6]), float.Parse(row[7]), float.Parse(row[8]), float.Parse(row[9])));
            }
            else if (type == "Equip")
            {
                AllItemList.Add(new Equip(row[0], row[1], int.Parse(row[2]),float.Parse(row[3]), float.Parse(row[4]), float.Parse(row[5])));
            }
            else if (type == "Food")
            {
                AllItemList.Add(new Food(row[0], row[1], int.Parse(row[2]), float.Parse(row[3])));
            }
            else
            {
                AllItemList.Add(new ItemData(row[0], row[1], int.Parse(row[2])));
            }

        }

        
    }


  
}
[System.Serializable]

public class ItemData
{
    public string type, name;
    public int price;

    public ItemData(string type, string name, int price)
    {
        this.type = type;
        this.name = name;
        this.price = price;

    }
}
public class UsableItem : ItemData
{
    private int perUsability;
    private float accuarancy;
    private float attack_speed;
    private float value;
    private int battle_type;
    private int durability;
    private int damage;

    public UsableItem(string type, string name, int price, int battle_type, int durability, int damage, int perUsability, float accuarancy, float attack_speed, float value) : base(type, name, price)
    {
        this.type = type;
        this.name = name;
        this.price = price;
        this.battle_type = battle_type;
        this.durability = durability;
        this.damage = damage;
        this.perUsability = perUsability;
        this.accuarancy = accuarancy;
        this.attack_speed = attack_speed;
        this.value = value;
    }

    public int Battle_type { get => battle_type; set => battle_type = value; }
    public int Durability { get => durability; set => durability = value; }
    public int Damage { get => damage; set => damage = value; }
    public int PerUsability { get => perUsability; set => perUsability = value; }
    public float Accuarancy { get => accuarancy; set => accuarancy = value; }
    public float Attack_speed { get => attack_speed; set => attack_speed = value; }
    public float Value { get => value; set => this.value = value; }
}

public class Equip : ItemData
{
    private float hp_up;
    private float movespeed_up;
    private float fireRate_up;

    public Equip(string type, string name, int price, float hp_up, float movespeed_up, float fireRate_up) : base(type, name, price)
    {
        this.type = type;
        this.name = name;
        this.price = price;
        this.hp_up = hp_up;
        this.movespeed_up = movespeed_up;
        this.fireRate_up = fireRate_up;
    }

    public float Hp_up { get => hp_up; set => hp_up = value; }
    public float Movespeed_up { get => movespeed_up; set => movespeed_up = value; }
    public float FireRate_up { get => fireRate_up; set => fireRate_up = value; }
}
public class Food : ItemData
{
    private float value;



    public Food(string type, string name, int price, float value) : base(type, name, price)
    {
        this.type = type;
        this.name = name;
        this.price = price;
        this.value = value;
    }

    public float Value { get => value; set => this.value = value; }
}