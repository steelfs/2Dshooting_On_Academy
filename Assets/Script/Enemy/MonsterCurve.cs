using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCurve : EnemyBase
{
    [Header("Curve Enemy data")]
    float curveDir = -1.0f;
    public float rotateSpeed = 10.0f;
 
    public override void OnInitialize()
    {

        if (transform.position.y > 0)
        {
            curveDir = 1.0f; // ������ �����ϸ� �Ʒ��� Ŀ��, ��ȸ��
        }
        else
        {
            curveDir = -1.0f;// �Ʒ����� �����ϸ� ���� Ŀ��
        }
    }
    protected override void OnMoveUpdate()
    {
        base.OnMoveUpdate();
        transform.Rotate(Time.deltaTime * rotateSpeed * curveDir * Vector3.forward);
    }
}
