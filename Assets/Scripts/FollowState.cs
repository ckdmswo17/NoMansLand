using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : StateMachineBehaviour
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
        if (Vector3.Distance(enemyTransform.position, enemy.player.position) > 8)
        {
            animator.SetBool("isFollow", false);
            animator.SetBool("isBack", true);
        }
        if (Vector3.Distance(enemyTransform.position, enemy.player.position) > 4)
        {
            enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, enemy.player.position, Time.deltaTime * enemy.speed);
        }
        else
        {
            animator.SetBool("isFollow", false);
            animator.SetBool("isBack", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
