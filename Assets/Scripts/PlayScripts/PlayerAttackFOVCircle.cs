using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackFOVCircle : MonoBehaviour
{
    private int segments = 360; // 원의 세분화
    public float attackRange; // 공격 사거리

    private FieldOfView atkFOV;
    private Player player_sc;

    private LineRenderer line;
    

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        player_sc = GetComponent<Player>();
        if (player_sc.gun.gameObject.name.Contains("AWP"))
        {
            attackRange = player_sc.gun.atkFOV.viewRadius - 6;
        } else
        {
            attackRange = player_sc.gun.atkFOV.viewRadius - 3;
        }
        
        DrawCircle();
    }

    public void awake2()
    {
        line = GetComponent<LineRenderer>();
        player_sc = GetComponent<Player>();
        if (player_sc.gun.gameObject.name.Contains("AWP"))
        {
            Debug.Log("Sniper");
            attackRange = player_sc.gun.atkFOV.viewRadius - 6;
        }
        else
        {
            Debug.Log("else");
            attackRange = player_sc.gun.atkFOV.viewRadius - 3;
        }
        DrawCircle();
        return;
    }

    void DrawCircle()
    {
        line.positionCount = segments;
        line.useWorldSpace = false;

        float deltaTheta = Mathf.Deg2Rad*1;
        float theta = 0f;

        for (int i = 0; i < segments; i++)
        {
            float x = attackRange * Mathf.Cos(theta);
            float z = attackRange * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, -0.5f, z);
            line.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }
}
