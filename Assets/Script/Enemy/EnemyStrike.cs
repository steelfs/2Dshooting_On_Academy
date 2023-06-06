using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrike : EnemyBase
{
    [Header("Rush Enemy data")]
    public float secondSpeed = 10.0f; //���� ���ǵ�
    public float appearTime = 1f;// ���� �ð�
    public float waitTime = 5.0f;// ���� �� ���ð�


    public override void OnInitialize()
    {
        StopAllCoroutines();
        StartCoroutine(AppearProcess());
    }

    IEnumerator AppearProcess()
    {
        yield return new WaitForSeconds(appearTime);
        speed = 0;
        yield return new WaitForSeconds(waitTime);
        speed = secondSpeed;
    }

}