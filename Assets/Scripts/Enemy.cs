using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //public Transform player; // 제대로 쏠려면 플레이어 위치를 가지고 있는게 맞지만, 객체지향적으로 맞지않는것 같아서 주석처리, 다른 방식 사용

    public float hp;
    public float speed;
    public Vector3 home;
    public float detectDistance;

    public Gun gun; // 임시로 ak47 기본 무기로 넣어놓음
    // Start is called before the first frame update
    void Start()
    {
        home = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void gunAttack()
    {
        if(gun.state == "Active")
        {
            if (gun.currentBulletAmount > 0)
            {
                GameObject bullet = Instantiate(gun.bulletPrefab, gun.transform.position, gun.transform.rotation);
                bullet.transform.LookAt(GameObject.Find("Player").transform.position);
                gun.currentBulletAmount -= 1;
                Debug.Log(gun.currentBulletAmount);
            }
        }
        
    }
}
