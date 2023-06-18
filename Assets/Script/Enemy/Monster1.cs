using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1 : EnemyBase
{
    Vector3 dir;
    Vector3 targetPos;
    float rangeX = 8.0f;
    float rangeY = 4.0f;

    protected override void OnEnable()
    {
        base.OnEnable();

        SetTargetPos();
    }
    protected override void OnMoveUpdate()
    {
        transform.position +=(Time.deltaTime * speed * dir);
        Debug.Log($"dir in OnMoveUpdate: {dir}");
        //if ((targetPos -transform.position).sqrMagnitude < 0.001f)
        //{
        //    ResetTargetPos();
        //}

    }
    void SetTargetPos()
    {
        targetPos = new Vector3(Random.Range(-rangeX, rangeX), Random.Range(-rangeY, rangeY), 0);
        dir = (targetPos - transform.position);
    }
}
