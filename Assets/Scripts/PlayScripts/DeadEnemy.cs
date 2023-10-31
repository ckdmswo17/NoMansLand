using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemy : MonoBehaviour
{

    private GameObject interactionButton;
    // Start is called before the first frame update
    void Start()
    {
        interactionButton = GameObject.Find("InteractionButton");
        interactionButton.SetActive(false);

        Destroy(gameObject, 20f); // 아이템 넣기 전에 임시로 자동파괴 넣어놓음

        // 아이템 목록 설정하는 코드
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            interactionButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            interactionButton.SetActive(false);
        }
    }

}