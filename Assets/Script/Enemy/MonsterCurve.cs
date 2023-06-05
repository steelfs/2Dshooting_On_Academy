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
            curveDir = 1.0f; // 위에서 등장하면 아래로 커브, 우회전
        }
        else
        {
            curveDir = -1.0f;// 아래에서 등장하면 위로 커브
        }
    }
    protected override void OnMoveUpdate()
    {
        base.OnMoveUpdate();
        transform.Rotate(Time.deltaTime * rotateSpeed * curveDir * Vector3.forward);
    }
}
