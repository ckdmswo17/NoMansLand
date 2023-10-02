using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyState : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        enemyTransform = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(enemy.atkDelay <= 0)
            animator.SetTrigger("Attack");

        if (Vector3.Distance(enemyTransform.position, enemy.player.position) > 4)
            animator.SetBool("isFollow", true);

    }
}