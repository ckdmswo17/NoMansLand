using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReadyState : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;
    Transform playerTransform;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        enemyTransform = animator.GetComponent<Transform>();
        playerTransform = GameObject.Find("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       

            Vector3 direction = playerTransform.position - enemyTransform.position; // 플레이어와 적 사이의 벡터를 계산합니다.
            Quaternion rotation = Quaternion.LookRotation(direction); // 해당 방향을 바라보는 회전값을 구합니다.
            enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, rotation, Time.deltaTime * 5f); // 부드럽게 회전하도록 Slerp 함수를 사용합니다.

            if (Vector3.Distance(enemyTransform.position, playerTransform.position) > enemy.gun.atkDistance)
                animator.SetBool("isFollow", true);

            


        //if(enemy.gun.state == "Active") // 레이 쏘는중 
        //{
           

            RaycastHit hitInfoNorthWest;
            RaycastHit hitInfoNorth;
            RaycastHit hitInfoNorthEast;
            bool r1Hit = Physics.Raycast(enemyTransform.position, enemyTransform.forward - enemyTransform.right, out hitInfoNorthWest, enemy.gun.atkDistance); // 적 시선 기준 북서 방향 레이 발사
            bool r2Hit = Physics.Raycast(enemyTransform.position, enemyTransform.forward, out hitInfoNorth, enemy.gun.atkDistance); // 적 시선 기준 북 방향 레이 발사
            bool r3Hit = Physics.Raycast(enemyTransform.position, enemyTransform.forward + enemyTransform.right, out hitInfoNorthEast, enemy.gun.atkDistance); // 적 시선 기준 북동 방향 레이 발사

            if (r1Hit)
            {
                if (hitInfoNorthWest.transform.name == "Player")
                {
                    animator.SetTrigger("Attack");
                }
            }

            if (r2Hit)
            {
                if (hitInfoNorth.transform.name == "Player")
                {
                    animator.SetTrigger("Attack");
                }
            }

            if (r3Hit)
            {
                if (hitInfoNorthEast.transform.name == "Player")
                {
                    animator.SetTrigger("Attack");
                }
            }


        //}
        

    }
}