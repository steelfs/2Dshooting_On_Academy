using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : EnemyBase
{
    [Header("Boss Data")]
    public Vector2 areaMin = new Vector2(2, -3); //활동영역  min~ max
    public Vector2 areaMax = new Vector2(7, 3);
    public float secondSpeed = 10.0f; //돌진 스피드
    public float appearTime = 1f;// 등장 시간
    public float waitTime = 5.0f;// 등장 후 대기시간


    public override void OnInitialize()
    {
        Vector3 newPos = transform.position;
        newPos.y = 0.0f;
        transform.position = newPos;
        StopAllCoroutines();
        StartCoroutine(AppearProcess());
    }

    IEnumerator AppearProcess()
    {
        yield return new WaitForSeconds(appearTime);
        float lodSpeed = speed;
        speed = 0;
        yield return new WaitForSeconds(waitTime);
        speed = lodSpeed;
    }
}
