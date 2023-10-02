using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed;
    public Vector3 home;

    public float atkCooltime = 3;
    public float atkDelay;
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        home = transform.position;
        atkDelay = atkCooltime;
    }

    // Update is called once per frame
    void Update()
    {
        if (atkDelay >= 0)
            atkDelay -= Time.deltaTime;
    }
    public void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.transform.LookAt(player.position);
    }
}
