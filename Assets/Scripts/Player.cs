using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        Transform enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.transform.LookAt(enemy.position);
    }
}
