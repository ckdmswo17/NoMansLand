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
    //public float currentReloadDelay; // 코루틴 사용으로 주석 처리
    public float maxBulletAmount;
    public float currentBulletAmount;

    public float maxDurability;
    public float currentDurability;

    public FieldOfView atkFOV;
    public GameObject bulletPrefab;
    public Transform atkPOS;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        currentDurability = maxDurability;
        currentBulletAmount = maxBulletAmount;
        audioSource = GetComponent<AudioSource>();  
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
