using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAsteroidMini : EnemyBase
{
    float baseSpeed = 7.0f;
    float baseRotateSpeed = 0.0f;
    float rotateSpeed = 0.0f;
    Vector3 direction;
    public override void OnInitialize()
    {
        speed = baseSpeed + Random.Range(-1.0f, 1.0f); //이동속도
        rotateSpeed = Random.Range(0, 360); // 회전속도
        direction = -transform.right; // 방향
    }
    protected override void OnMoveUpdate()
    {
        //base.OnMoveUpdate();
        transform.Translate(Time.deltaTime * speed * direction, Space.World);
        transform.Rotate(Time.deltaTime * rotateSpeed * Vector3.forward);
    }
}
