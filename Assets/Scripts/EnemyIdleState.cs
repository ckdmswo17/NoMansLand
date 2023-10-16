using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : StateMachineBehaviour
{
    //Transform enemyTransform;
    //Enemy enemy;
    //// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    enemy = animator.GetComponent<Enemy>();
    //    enemyTransform = animator.GetComponent<Transform>();
        
    //}

    //// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    RaycastHit hitInfoNorthWest;
    //    RaycastHit hitInfoNorth;
    //    RaycastHit hitInfoNorthEast;
    //    bool r1Hit = Physics.Raycast(enemyTransform.position, enemyTransform.forward - enemyTransform.right, out hitInfoNorthWest, enemy.gun.atkDistance); // 적 시선 기준 북서 방향 레이 발사
    //    bool r2Hit = Physics.Raycast(enemyTransform.position, enemyTransform.forward, out hitInfoNorth, enemy.gun.atkDistance); // 적 시선 기준 북 방향 레이 발사
    //    bool r3Hit = Physics.Raycast(enemyTransform.position, enemyTransform.forward + enemyTransform.right, out hitInfoNorthEast, enemy.gun.atkDistance); // 적 시선 기준 북동 방향 레이 발사

    //    if (r1Hit)
    //    {
    //        if (hitInfoNorthWest.transform.name == "Player")
    //        {
    //            animator.SetBool("isFollow", true);
    //        }
    //    }

    //    if (r2Hit)
    //    {
    //        if (hitInfoNorth.transform.name == "Player")
    //        {
    //            animator.SetBool("isFollow", true);
    //        }
    //    }

    //    if (r3Hit)
    //    {
    //        if (hitInfoNorthEast.transform.name == "Player")
    //        {
    //            animator.SetBool("isFollow", true);
    //        }
    //    }

    //}

    //// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

}
