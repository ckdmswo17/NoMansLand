using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float hp;
    public float speed;

    public Gun gun;

    private Animator animator;

    public JoyStick joystick;
    private bool nowShooting = false; // update문의 연사 함수 다중호출을 막기위해 1개의 연사 함수가 실행중임을 의미하는 플래그

    public float moveRotationSpeed;
    public float atkRotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        

        
    }

    void Update()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }

        if (gun.atkFOV.visibleTargets.Count > 0)
        {
            if (gun.state == "Active" && joystick.atkAble) // 회전이 섞이지 않게 조이스틱을 놓았을때만 적쪽으로 회전
            {
                Vector3 direction = (gun.atkFOV.visibleTargets[0].position - transform.position).normalized;
                Quaternion rotation = Quaternion.LookRotation(direction); // 해당 방향을 바라보는 회전값을 구합니다.
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * atkRotationSpeed); // 부드럽게 회전하도록 Slerp 함수를 사용합니다.
            }

            if (gun.state == "Active" && joystick.atkAble && !nowShooting)
            {
                StartCoroutine(volleyRangedAttack(gun.atkFOV.visibleTargets[0]));
                //Debug.Log("사격시작");
                
            }
        }

        if (gun.currentBulletAmount <= 0 && gun.state == "Active")
        {
            StartCoroutine(DelayedReload());
            
        }
    }

    public IEnumerator volleyRangedAttack(Transform targetTransform)
    {
        nowShooting = true;
        while (gun.currentBulletAmount > 0)
        {
            if (!joystick.atkAble) // 조이스틱이 눌렸을
            {
                break; 
            }
            rangedAttack(targetTransform);          
            yield return new WaitForSeconds(gun.atkDelay);
        }
        nowShooting = false;
        // 이 자리에 다른 무기로 전환하는 코드 필요

    }

    public void rangedAttack(Transform targetTransform)
    {
        GameObject bullet = Instantiate(gun.bulletPrefab, gun.atkPOS.position, gun.transform.rotation);
        bullet.transform.LookAt(targetTransform.position);
        Bullet bullet_sc = bullet.GetComponent<Bullet>();
        bullet_sc.gunDamage = gun.damage;
        bullet_sc.whoShoot = "Player";
        gun.currentBulletAmount -= 1;

       
        //Debug.Log(gun.currentBulletAmount);
    }

    IEnumerator DelayedReload()
    {
        gun.state = "Reload";
        yield return new WaitForSeconds(gun.reloadDelay);
        Reload();
    }

    public void Reload()
    {
        gun.currentBulletAmount = gun.maxBulletAmount;
        gun.state = "Active";
    }

    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.gameObject.CompareTag("Bullet") && collision.gameObject.GetComponent<Bullet>().whoShoot == "Enemy")
    //    {
    //        Debug.Log("player collision");
    //        Destroy(collision.gameObject);
    //        //hp -= collision.gameObject.GetComponent<Bullet>().gunDamage;
    //    }
    //}
}
