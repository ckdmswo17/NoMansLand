using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;

    public float maxHp;
    public float hp;
    public float speed; 
    public Vector3 home;
    public FieldOfView detectFOV;

    public Gun gun; // 임시로 ak47 기본 무기로 넣어놓음

    public bool isFollowing = false;
    private bool isBack = false;
    public bool nowShooting = false;

    public float moveRotationSpeed;
    public float atkRotationSpeed;

    public NavMeshAgent agent;

    public Transform canvasTransform;

    public GameObject deadBodyFactory;

    public Animator animator;

    //public EnemyInventory enemyInventory;

    // Start is called before the first frame update
    void Start()
    {
        home = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.angularSpeed = moveRotationSpeed;

        hp = maxHp;
    }

    void Update()
    {
        canvasTransform.rotation = Quaternion.Euler(55, 0, 0);

        if (hp <= 0)
        {

                GameObject deadBody = Instantiate(deadBodyFactory);
                deadBody.transform.position = transform.position;
            deadBody.transform.localScale = transform.localScale;
                Destroy(gameObject);

        }
        if (nowShooting)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction); // 해당 방향을 바라보는 회전값을 구합니다.
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * atkRotationSpeed); // 부드럽게 회전하도록 Slerp 함수를 사용합니다.
        }


        if (detectFOV.visibleTargets.Count > 0)
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
        
        if (isFollowing && !nowShooting)
        {
            animator.SetBool("isRun", true);
            animator.SetBool("isRangedAttack", false);
            //Debug.Log("따라가는중");
            //transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, Time.deltaTime * speed);
            agent.SetDestination(playerTransform.position);

            Vector3 direction = (playerTransform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction); // 해당 방향을 바라보는 회전값을 구합니다.
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * moveRotationSpeed); // 부드럽게 회전하도록 Slerp 함수를 사용합니다.
        }
        if (isBack && !nowShooting)
        {
            animator.SetBool("isRun", false);
            animator.SetBool("isRangedAttack", false);

            //Debug.Log("돌아가는중");
            //transform.position = Vector3.MoveTowards(transform.position, home, Time.deltaTime * speed);
            if (transform.position.x != home.x || transform.position.z != home.z)
            {
                animator.SetBool("isRun", true);
                animator.SetBool("isRangedAttack", false);
                agent.SetDestination(home);
                Vector3 direction = (home - transform.position).normalized;
                Quaternion rotation = Quaternion.LookRotation(direction); // 해당 방향을 바라보는 회전값을 구합니다.
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * moveRotationSpeed); // 부드럽게 회전하도록 Slerp 함수를 사용합니다.


            } 
            


        }
       
    }




}
