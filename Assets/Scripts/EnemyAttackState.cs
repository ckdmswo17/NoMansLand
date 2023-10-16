using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : StateMachineBehaviour
{
    //Enemy enemy;
    //float currentTime;
    //Transform playerTransform;
    //// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    currentTime = 0;
    //    enemy = animator.GetComponent<Enemy>();
    //    playerTransform = GameObject.Find("Player").transform;
    //    enemy.gun.Attack(playerTransform);
    //    //while (enemy.gun.currentBulletAmount > 0) // update보다 while 반복이 압도적으로 빠르다. 이 코드로 돌리면 총알이 한번에 다 겹쳐서나감 
    //    //{
            
    //    //    if (currentTime >= enemy.gun.atkDelay)
    //    //    {
    //    //        currentTime = 0;
    //    //        enemy.gunAttack();
                
    //    //    }       
    //    //}
    //    //animator.SetBool("isFollow",false);
    //    //animator.SetBool("isBack", false);
    //}

    //// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    if (enemy.gun.currentBulletAmount <= 0)
    //    {
    //        animator.SetBool("isReady", true);
    //    }

    //    currentTime += Time.deltaTime;
    //    if (currentTime >= enemy.gun.atkDelay)
    //    {
    //        currentTime = 0;
    //        enemy.gun.Attack(playerTransform);

    //    }
       
        
    //}

    //// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
    //}

}
