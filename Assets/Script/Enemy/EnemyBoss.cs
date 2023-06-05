using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : EnemyBase
{
    [Header("Boss Data")]
    public Vector2 areaMin = new Vector2(2, -3); //Ȱ������  min~ max
    public Vector2 areaMax = new Vector2(7, 3);
    public float secondSpeed = 10.0f; //���� ���ǵ�
    public float appearTime = 1f;// ���� �ð�
    public float waitTime = 5.0f;// ���� �� ���ð�


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
