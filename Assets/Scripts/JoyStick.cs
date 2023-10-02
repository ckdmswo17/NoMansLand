using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour //, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
   public static JoyStick Instance
   {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<JoyStick>();
                if(instance == null)
                {
                    var instanceContainer = new GameObject("JoyStick");
                    instance = instanceContainer.AddComponent<JoyStick>();
                }
            }
            return instance;
        }
   }
    private static JoyStick instance;

    public GameObject smallStick;
    public GameObject bGStick;
    Vector3 stickFirstPosition;
    public Vector3 joyVec;
    float stickDiameter;

    [SerializeField] private GameObject go_Player;
    [SerializeField] private float moveSpeed;

    private bool isTouch = false;
    private Vector3 movePosition;

    private Player player_sc;

    void Start()
    {
        stickDiameter = bGStick.gameObject.GetComponent<RectTransform>().sizeDelta.y;

        player_sc = go_Player.GetComponent<Player>();
    }

    void Update()
    {
        if (isTouch)
        {
            go_Player.transform.position += movePosition;
        }
    }

    public void PointDown()
    {
        isTouch = true;
        bGStick.SetActive(true);
        bGStick.transform.position = Input.mousePosition;
        smallStick.transform.position = Input.mousePosition;
        stickFirstPosition = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector3 DragPosition = pointerEventData.position;
        joyVec = (DragPosition - stickFirstPosition).normalized;

        float stickDistance = Vector3.Distance(DragPosition, stickFirstPosition); // 조이스틱 밖 거리까지 포함

        if(stickDistance < stickDiameter)
        {
            //Debug.Log("small");
            smallStick.transform.position = stickFirstPosition + joyVec * stickDistance;
        }
        else
        {
            //Debug.Log("big");
            smallStick.transform.position = stickFirstPosition + joyVec * stickDiameter;
        }
        float innerDistance = Vector2.Distance(bGStick.transform.position, smallStick.transform.position) / (stickDiameter * 0.5f); 
        movePosition = new Vector3(joyVec.x * moveSpeed * innerDistance * Time.deltaTime, 0f, joyVec.y * moveSpeed * innerDistance * Time.deltaTime);
    }

    public void Drop()
    {
        isTouch = false;
        joyVec = Vector3.zero;
        movePosition = Vector3.zero;
        player_sc.Attack();
        bGStick.SetActive(false);
    }

    // 고정 조이스틱 방식
    //[SerializeField] private RectTransform rect_Background;
    //[SerializeField] private RectTransform rect_Joystick;

    //private float radius;

    //[SerializeField] private GameObject go_Player;
    //[SerializeField] private float moveSpeed;

    //private bool isTouch = false;
    //private Vector3 movePosition;
    
    //void Start()
    //{
    //    radius = rect_Background.rect.width * 0.5f;
    //}

    //void Update()
    //{
    //    if (isTouch)
    //    {
    //        go_Player.transform.position += movePosition;
    //    }
    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    isTouch = true;
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    isTouch = false;
    //    rect_Joystick.localPosition = Vector3.zero;
    //    movePosition = Vector3.zero;
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    Vector2 value = eventData.position - (Vector2)rect_Background.position;
    //    value = Vector2.ClampMagnitude(value, radius);

    //    rect_Joystick.localPosition = value;

    //    float distance = Vector2.Distance(rect_Background.position, rect_Joystick.position) / radius;
    //    value = value.normalized;
    //    movePosition = new Vector3(value.x * moveSpeed * distance * Time.deltaTime,0f,value.y * moveSpeed * distance * Time.deltaTime);

    //}
}