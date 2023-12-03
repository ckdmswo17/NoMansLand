using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject user;
    public Player player_sc;
    public Enemy enemy_sc;

    public string state; // Active(플레이 중 발사 가능), Reload(장전 중), Inactive(플레이 중 발사 불가)

    public float damage;
    public float atkDelay; // 한발 한발 간 연사 딜레이 (연사속도) 

    //public float overheatDelay;
    //public float currentOverheatDelay;
    //public float maxOverheatGaze;
    //public float currentOverheatGaze; // 장전 시스템이 좀 더 간단한데 굳이 과열 시스템으로 해야하나 싶어서 일단 주석처리하고 장전으로 구현.

    public float reloadDelay;
    //public float currentReloadDelay; // 코루틴 사용으로 주석 처리
    public float maxBulletAmount;
    public float currentBulletAmount;

    public float maxDurability;
    public float currentDurability;

    public FieldOfView atkFOV;
    public GameObject bulletPrefab;
    public Transform atkPOS;
    public AudioSource audioSource;

    public GameObject reloadText;

    void Awake()
    {
        

    }
    // Start is called before the first frame update
    void Start()
    {
        currentDurability = maxDurability;
        currentBulletAmount = maxBulletAmount;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(user.name == "Player")
        {
            if (atkFOV.visibleTargets.Count > 0)
            {

                if (state == "Active" && player_sc.joystick.atkAble) // 회전이 섞이지 않게 조이스틱을 놓았을때만 적쪽으로 회전
                {
                    
                    Vector3 direction = user.transform.position.normalized;
                    if (atkFOV.visibleTargets[0] != null)
                    {
                        direction = (atkFOV.visibleTargets[0].position - user.transform.position).normalized;
                    }
                    Quaternion rotation = Quaternion.LookRotation(direction); // 해당 방향을 바라보는 회전값을 구합니다.
                    user.transform.rotation = Quaternion.Lerp(user.transform.rotation, rotation, Time.deltaTime * player_sc.atkRotationSpeed); // 부드럽게 회전하도록 Slerp 함수를 사용합니다.
                }

                if (state == "Active" && player_sc.joystick.atkAble && !player_sc.nowShooting)
                {

                    StartCoroutine(volleyRangedAttack(atkFOV.visibleTargets[0]));
                    //Debug.Log("사격시작");

                }
            }

            if (currentBulletAmount <= 0 && state != "Reload")
            {
                StartCoroutine(DelayedReload());

            }
        } else
        {
            
            if (state != "Inactive" && atkFOV.visibleTargets.Count > 0)
            { // 사거리 내에 들어옴
                enemy_sc.animator.SetBool("isRun", false);
                enemy_sc.animator.SetBool("isRangedAttack", false);


                if (atkFOV.visibleTargets.Count == 0)
                {
                    enemy_sc.agent.isStopped = false;
                }
                if (state == "Active" && !enemy_sc.nowShooting)
                {

                    StartCoroutine(volleyRangedAttack(enemy_sc.playerTransform));
                }

            }
            if (currentBulletAmount <= 0 && state != "Reload")
            {
                if(reloadText != null)
                {
                    StartCoroutine(DelayedReload());
                }
                
            }
        }
    }

    public IEnumerator volleyRangedAttack(Transform targetTransform)
    {
        if(user.name == "Player")
        {
            player_sc.nowShooting = true;
            player_sc.animator.SetBool("isRangedAttack", true);
            player_sc.animator.SetBool("isRun", false);
            while (currentBulletAmount > 0)
            {
                if (!player_sc.joystick.atkAble) // 조이스틱이 눌렸을
                {
                    player_sc.animator.SetBool("isRangedAttack", false);
                    player_sc.animator.SetBool("isRun", false);
                    break;
                }
                rangedAttack(targetTransform);
                yield return new WaitForSeconds(atkDelay);
            }
            player_sc.nowShooting = false;
            // 이 자리에 다른 무기로 전환하는 코드 필요
        } else
        {
            enemy_sc.agent.isStopped = true;
            enemy_sc.animator.SetBool("isRun", false);
            enemy_sc.animator.SetBool("isRangedAttack", true);

            enemy_sc.nowShooting = true;
            //agent.isStopped = true;
            while (currentBulletAmount > 0)
            {
                if (enemy_sc.hp <= 0)
                {
                    break;
                }
                rangedAttack(targetTransform);
                yield return new WaitForSeconds(atkDelay);
            }
            enemy_sc.nowShooting = false;
            enemy_sc.agent.isStopped = false;

            enemy_sc.animator.SetBool("isRun", false);
            enemy_sc.animator.SetBool("isRangedAttack", false);
        }

    }

    public void rangedAttack(Transform targetTransform)
    {
        if(user.name == "Player")
        {
            if (targetTransform != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, atkPOS.position, user.transform.rotation);
                bullet.transform.LookAt(targetTransform.position);
                Bullet bullet_sc = bullet.GetComponent<Bullet>();
                bullet_sc.gunDamage = damage;
                bullet_sc.whoShoot = "Player";
                currentBulletAmount -= 1;

                audioSource.Play();

                Debug.Log(currentBulletAmount);
            }
        } else
        {
            GameObject bullet = Instantiate(bulletPrefab, atkPOS.position, user.transform.rotation);
            bullet.transform.LookAt(targetTransform.position);
            Bullet bullet_sc = bullet.GetComponent<Bullet>();
            bullet_sc.gunDamage = damage;
            bullet_sc.whoShoot = "Enemy";
            currentBulletAmount -= 1;

            audioSource.Play();
        }


    }

    IEnumerator DelayedReload() 
    {
        string firstState = state;
        state = "Reload";
        Debug.Log("장전 시작");
        reloadText.SetActive(true);
        yield return new WaitForSeconds(reloadDelay);
        Reload(firstState);

        
    }

    public void Reload(string firstState)
    {
        reloadText.SetActive(false);
        Debug.Log("장전 끝");
        currentBulletAmount = maxBulletAmount;
        if(firstState == "Active")
        {
            state = "Active";
        } else if(firstState == "Inactive")
        {
            state = "Inactive";
        }
        
    }

}
