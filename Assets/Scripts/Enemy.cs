using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform; 

    public float hp;
    public float speed;
    public Vector3 home;
    public FieldOfView detectFOV;

    public Gun gun; // 임시로 ak47 기본 무기로 넣어놓음

    private bool isFollowing = false;
    private bool isBack = false;
    private bool nowShooting = false;

    public float moveRotationSpeed;
    public float atkRotationSpeed;

    private NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        home = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.angularSpeed = moveRotationSpeed;
    }

    void Update()
    {
        
        if (nowShooting)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction); // 해당 방향을 바라보는 회전값을 구합니다.
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * atkRotationSpeed); // 부드럽게 회전하도록 Slerp 함수를 사용합니다.
        }
        
        if(detectFOV.visibleTargets.Count > 0)
        {
            //Debug.Log("적 발견");
            isFollowing = true;
            isBack = false;
            
            
        }
        
        if (Vector3.Distance(transform.position, playerTransform.position) > detectFOV.viewRadius)
        {
            //Debug.Log("추적 중지");
            isFollowing = false;
            isBack = true;
        }
        if (gun.atkFOV.visibleTargets.Count > 0)
        {
            agent.isStopped = true;
            if(gun.atkFOV.visibleTargets.Count == 0)
            {
                agent.isStopped = false;
            }
            if(gun.state == "Active" && !nowShooting)
            {
                StartCoroutine(volleyRangedAttack(playerTransform));
            }
            
        }
        if (isFollowing && !nowShooting)
        {
            //Debug.Log("따라가는중");
            //transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, Time.deltaTime * speed);
            agent.SetDestination(playerTransform.position);

            Vector3 direction = (playerTransform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction); // 해당 방향을 바라보는 회전값을 구합니다.
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * moveRotationSpeed); // 부드럽게 회전하도록 Slerp 함수를 사용합니다.
        }
        if (isBack && !nowShooting)
        {
            //Debug.Log("돌아가는중");
            //transform.position = Vector3.MoveTowards(transform.position, home, Time.deltaTime * speed);
            if(transform.position.x != home.x || transform.position.z != home.z)
            {
                agent.SetDestination(home);
                Vector3 direction = (home - transform.position).normalized;
                Quaternion rotation = Quaternion.LookRotation(direction); // 해당 방향을 바라보는 회전값을 구합니다.
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * moveRotationSpeed); // 부드럽게 회전하도록 Slerp 함수를 사용합니다.


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
        //agent.isStopped = true;
        while (gun.currentBulletAmount > 0)
        {

            rangedAttack(targetTransform);
            yield return new WaitForSeconds(gun.atkDelay);
        }
        nowShooting = false;
        agent.isStopped = false;

    }

    public void rangedAttack(Transform targetTransform)
    {
        GameObject bullet = Instantiate(gun.bulletPrefab, gun.atkPOS.position, transform.rotation);
        bullet.transform.LookAt(targetTransform.position);
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
}
