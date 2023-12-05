using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemy : MonoBehaviour
{

    private UIManager uiManager;
    public Animator animator;
    //public EnemyInventory enemyInventory;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        uiManager = GameObject.Find("MainCanvas").GetComponent<UIManager>();

        StartCoroutine(DelayedDead());
        Destroy(gameObject, 120f); // 아이템 넣기 전에 임시로 자동파괴 넣어놓음

        // 아이템 목록 설정하는 코드
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            uiManager.interactionButtonOnOff(true);
            //if (uiManager != null)
            //{
            //    uiManager.isTriggering = true;
            //    uiManager.curDeadEnemy = this;
            //} 
     
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            uiManager.interactionButtonOnOff(false);
            //if (uiManager != null)
            //{
            //    uiManager.isTriggering = false;
            //    iig.curDeadEnemy = null;
            //}

        }
    }

    IEnumerator DelayedDead() 
    {
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("isIdle");
        transform.position -= new Vector3(0, 1, 0);
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }

}