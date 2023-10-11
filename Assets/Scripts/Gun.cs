using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string state; // Active(플레이 중 발사 가능), Reload(장전 중), Item(메인화면에서 쓰거나 플레이화면 오브젝트로 쓰일때)

    public float damage;
    public float atkDelay; // 한발 한발 간 연사 딜레이 (연사속도) 

    //public float overheatDelay;
    //public float currentOverheatDelay;
    //public float maxOverheatGaze;
    //public float currentOverheatGaze; // 장전 시스템이 좀 더 간단한데 굳이 과열 시스템으로 해야하나 싶어서 일단 주석처리하고 장전으로 구현.

    public float reloadDelay;
    public float currentReloadDelay;
    public float maxBulletAmount;
    public float currentBulletAmount;

    public float maxDurability;
    public float currentDurability;
    public float atkDistance;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        currentDurability = maxDurability;
        currentReloadDelay = reloadDelay;
        currentBulletAmount = maxBulletAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBulletAmount <= 0) // 장전중
        {
            //Debug.Log("장전시작");
            state = "Reload";
            currentReloadDelay += Time.deltaTime;

            
            if (reloadDelay <= currentReloadDelay) // 장전 시간 끝
            {
                //Debug.Log("장전완료");
                currentReloadDelay = 0;
                currentBulletAmount = maxBulletAmount;
                state = "Active";
            }
        }
    }

}
