using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float destroyTime;
    private Rigidbody bulletRigidbody;

    public float gunDamage;
    public string whoShoot;

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        bulletRigidbody.velocity = transform.forward * speed;

        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (whoShoot == "Player")
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                
                Destroy(gameObject);
                collision.gameObject.GetComponent<Enemy>().hp -= gunDamage;
                collision.gameObject.GetComponent<Enemy>().isFollowing = true; // 자신의 시야내에 없어도 맞으면 바로 추적상태로 전환
                Debug.Log("Enemy HP : " + collision.gameObject.GetComponent<Enemy>().hp);
            }

        } else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                
                Destroy(gameObject);
                collision.gameObject.GetComponent<Player>().hp -= gunDamage;
                Debug.Log("Player HP : " + collision.gameObject.GetComponent<Player>().hp);
            }
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
