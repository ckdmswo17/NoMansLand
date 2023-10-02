using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackState : StateMachineBehaviour
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
        if (Vector3.Distance(enemy.home, enemyTransform.position) <= 0.1f || Vector3.Distance(enemyTransform.position,enemy.player.position) <= 8)
        { 
            animator.SetBool("isBack", false);
        }
        else
        {
            enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, enemy.home, Time.deltaTime * enemy.speed);
        }

    }
}