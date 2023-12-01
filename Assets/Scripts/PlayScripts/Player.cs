using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHp;
    public float hp;
    public float speed;

    public List<GameObject> guns;
    public Gun gun;

    public Animator animator;

    public JoyStick joystick;
    public bool nowShooting = false; // update문의 연사 함수 다중호출을 막기위해 1개의 연사 함수가 실행중임을 의미하는 플래그

    public float moveRotationSpeed;
    public float atkRotationSpeed;

    public Transform canvasTransform;

    public AudioSource audioSource;
    public AudioClip audioClip;
    public GameObject audio;

    private UIManager uiManager;
    public WeaponGroup weaponGroup;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.Find("MainCanvas").GetComponent<UIManager>();
        hp = maxHp;
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        //Quaternion q_hp = Quaternion.LookRotation(hpSliderTransform.position - cam.transform.position);
        //Vector3 hp_angle = Quaternion.RotateTowards(hpSliderTransform.rotation, q_hp, 300).eulerAngles;
        canvasTransform.rotation = Quaternion.Euler(55, 0, 0);

        if (hp <= 0)
        {
            audio.GetComponent<AudioSource>().PlayOneShot(audioClip);
            Destroy(gameObject);
            uiManager.GoToFailResult();
        }

        if (gun.atkFOV.visibleTargets.Count > 0)
        {
  
            if (gun.state == "Active" && joystick.atkAble) // 회전이 섞이지 않게 조이스틱을 놓았을때만 적쪽으로 회전
            {
                Debug.Log(nowShooting);
                Vector3 direction = transform.position.normalized;
                if (gun.atkFOV.visibleTargets[0] != null)
                {
                    direction = (gun.atkFOV.visibleTargets[0].position - transform.position).normalized;
                }
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
        animator.SetBool("isRangedAttack", true);
        animator.SetBool("isRun", false);
        while (gun.currentBulletAmount > 0)
        {
            if (!joystick.atkAble) // 조이스틱이 눌렸을
            {
                animator.SetBool("isRangedAttack", false);
                animator.SetBool("isRun", false);
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
        if(targetTransform != null)
        {
            GameObject bullet = Instantiate(gun.bulletPrefab, gun.atkPOS.position, gun.transform.rotation);
            bullet.transform.LookAt(targetTransform.position);
            Bullet bullet_sc = bullet.GetComponent<Bullet>();
            bullet_sc.gunDamage = gun.damage;
            bullet_sc.whoShoot = "Player";
            gun.currentBulletAmount -= 1;

            gun.audioSource.Play();

            Debug.Log(gun.currentBulletAmount);
        }
        
    }

    IEnumerator DelayedReload() // 플레이어만 장전 안되는 문제 해결해야할듯
    {
        gun.state = "Reload";
        Debug.Log("장전 시작");
        yield return new WaitForSeconds(gun.reloadDelay);
        Reload();
    }

    public void Reload()
    {
        Debug.Log("장전 끝");
        gun.currentBulletAmount = gun.maxBulletAmount;
        gun.state = "Active";
    }

    public void weaponChange(int index)
    {
        for(int i = 0; i < guns.Capacity; i++)
        {
            if(guns[i] != null)
            {
                if(i == index)
                {
                    guns[i].SetActive(true);
                    gun = guns[i].GetComponent<Gun>();
                } else
                {
                    guns[i].SetActive(false);
                }
            }
        }
    }

}
