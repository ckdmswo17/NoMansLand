using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowState : StateMachineBehaviour
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
        if (Vector3.Distance(enemyTransform.position, playerTransform.position) > enemy.detectDistance+4)
        {
            animator.SetBool("isFollow", false);
            animator.SetBool("isBack", true);
        }

        if (Vector3.Distance(enemyTransform.position, playerTransform.position) > enemy.gun.atkDistance)
        {
            enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, playerTransform.position, Time.deltaTime * enemy.speed);
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
