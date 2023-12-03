using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Player : MonoBehaviour
{
    public float maxHp;
    public float hp;
    public float speed;

    public List<GameObject> guns;
    public Gun gun;

    public Animator animator;

    public JoyStick joystick;
    public bool nowShooting = false; // update문의 연사 함수 다중호출을 막기위해 1개의 연사 함수가 실행중임을 의미하는 플래그

    public float moveRotationSpeed;
    public float atkRotationSpeed;

    public Transform canvasTransform;

    public AudioSource audioSource;
    public AudioClip audioClip;
    public GameObject audio;

    private UIManager uiManager;
    public InvenTest invenTest;
    public GameObject weaponsObject;
    public GameObject reloadText;
    //public WeaponGroup weaponGroup;

    // Start is called before the first frame update
    void Awake()
    {
        uiManager = GameObject.Find("MainCanvas").GetComponent<UIManager>();
        hp = maxHp;
        audioSource = GetComponent<AudioSource>();

        for(int i = 0; i < invenTest.weaponNames.Capacity; i++)
        {
            string name = invenTest.weaponNames[i];
            GameObject go = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/PlayPrefabs/" + name + ".prefab", typeof(GameObject)));
            
            guns.Add(go);
            Gun gun_sc = go.GetComponent<Gun>();
            if (i == 0)
            {
                gun = gun_sc;
                gun.user = gameObject;
                gun.player_sc = this;
                gun.reloadText = reloadText;
            } else
            {
                gun_sc.state = "Inactive";

                Transform gunPrefab = go.transform.GetChild(0);
                gunPrefab.GetComponent<MeshRenderer>().enabled = false;
                for (int j = 0; j < gunPrefab.childCount; j++)
                {
                    gunPrefab.GetChild(j).GetComponent<MeshRenderer>().enabled = false;
                }

                gun_sc.user = gameObject;
                gun_sc.player_sc = this;
                gun_sc.reloadText = reloadText;

            }
            
            if ((0 <= i) && (i < guns.Capacity))
            {
                
                go.transform.SetParent(weaponsObject.transform);
                go.transform.position = weaponsObject.transform.position;
                go.transform.localScale = new Vector3(1, 1, 1);

            }
            
        }

    }

    void Update()
    {
        //Quaternion q_hp = Quaternion.LookRotation(hpSliderTransform.position - cam.transform.position);
        //Vector3 hp_angle = Quaternion.RotateTowards(hpSliderTransform.rotation, q_hp, 300).eulerAngles;
        canvasTransform.rotation = Quaternion.Euler(55, 0, 0);

        if (hp <= 0)
        {
            audio.GetComponent<AudioSource>().PlayOneShot(audioClip);
            Destroy(gameObject);
            uiManager.GoToFailResult();
        }

    }

    public void weaponChange(int index)
    {
        if((0<=index && index<guns.Capacity) && guns[index] == null)
        {
            return;
        }
        //Debug.Log("index : " + index+", cap : "+guns.Capacity);
        for(int i = 0; i < guns.Capacity; i++)
        {
            
            if(guns[i] != null)
            {
                //Debug.Log(i);
                Transform gunPrefab = guns[i].transform.GetChild(0);
                if (i == index)
                {
                    guns[i].GetComponent<Gun>().state = "Active";
                    gunPrefab.GetComponent<MeshRenderer>().enabled = true;
                    for(int j = 0; j < gunPrefab.childCount; j++)
                    {
                        gunPrefab.GetChild(j).GetComponent<MeshRenderer>().enabled = true;
                    }
                    gun = guns[i].GetComponent<Gun>();
                    
                    gameObject.GetComponent<PlayerAttackFOVCircle>().awake2();
                } else
                {
                    guns[i].GetComponent<Gun>().state = "Inactive";
                    gunPrefab.GetComponent<MeshRenderer>().enabled = false;
                    for (int j = 0; j < gunPrefab.childCount; j++)
                    {
                        gunPrefab.GetChild(j).GetComponent<MeshRenderer>().enabled = false;
                    }
                }
            }
        }
    }

}
