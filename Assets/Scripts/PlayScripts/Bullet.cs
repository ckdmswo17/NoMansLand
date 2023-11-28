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

    public GameObject bloodEffectFactory;

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
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();

                GameObject bloodEffect = Instantiate(bloodEffectFactory);
                bloodEffect.transform.position = transform.position;
                Destroy(bloodEffect, 0.35f);

                Destroy(gameObject);
                enemy.hp -= gunDamage;
                enemy.isFollowing = true; // 자신의 시야내에 없어도 맞으면 바로 추적상태로 전환
                enemy.animator.SetTrigger("isAttacked");
                
                Debug.Log("Enemy HP : " + enemy.hp);
            }

        } else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Player player = collision.gameObject.GetComponent<Player>();

                GameObject bloodEffect = Instantiate(bloodEffectFactory);
                bloodEffect.transform.position = transform.position;
                Destroy(bloodEffect, 0.35f);

                Destroy(gameObject);
                player.hp -= gunDamage;
                player.animator.SetTrigger("isAttacked");
                Debug.Log("Player HP : " + player.hp);
            }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
