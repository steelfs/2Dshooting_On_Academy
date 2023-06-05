using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : EnemyBase
{
    [Header("Boss Data")]
    public Vector2 areaMin = new Vector2(2, -3); //Ȱ������  min~ max
    public Vector2 areaMax = new Vector2(7, 3);

    Vector3 targetPos;
    Vector3 moveDirection;


    public override void OnInitialize()
    {
  
        Vector3 newPos = transform.position;
        newPos.y = 0.0f;
        transform.position = newPos; // y��ġ 0 

        StopAllCoroutines(); //�ڽ��� �ڷ�ƾ ��� ����
        StartCoroutine(AppearProcess());
    }

    IEnumerator AppearProcess()
    {
        float oldSpeed = speed; //�����ӵ� ����
        speed = 0; // �ӵ��� 0���� �ȿ����̰� ����
        float remainDistance = 5; //���� �Ÿ� 5



        while (remainDistance > 0.01f) // �����ִ� �Ÿ��� ���� 0�� �� �� ���� �ݺ�
        {
            // 0���� remainDistance���� ���� �� Time.deltaTime * 0.5f��ŭ ����� ��ġ
            float deltaMove = Mathf.Lerp(remainDistance, 0, Time.deltaTime * 1.2f);
            remainDistance -= deltaMove; // deltamove��ŭ �����Ÿ� ���� 
            transform.Translate(deltaMove * (-Vector3.right)); //deltamove��ŭ �������� �̵�
                       
            yield return null;
        }

       // yield return new WaitForSeconds(appearTime); 
        speed = oldSpeed;
        SetNextTargetPos();
    }
    protected override void OnMoveUpdate()
    {
     
        transform.Translate(Time.deltaTime * speed * moveDirection);
        //if (targetPos == transform.position) //�־��� �ڵ� float �̱� �빮�� ������ �Ǵ��ϱ� ����

        //if ((targetPos - transform.position).sqrMagnitude < 0.0001f) //�Ʒ� if�� �ڵ尡 ���귮�� �ξ� ����
        //{
        //    //Ÿ�������� ���� ���������
        //    SetNextTargetPos();
        //}
        if (targetPos.y > 0) // �� �ڵ�� vector�� ���� ��� ����ؾ������� �Ʒ� if���� Y���� ����� �ϱ� ������ ���귮�� �ξ� ����
        {
            if (transform.position.y > targetPos.y)// ���� �������� �̵��ؼ� ������ ������ ������ �ʱ�ȭ
            {
                SetNextTargetPos();
            }
        }
        else
        {
            if (transform.position.y < targetPos.y)// ���� �Ʒ������� �̵��ؼ� ������ ������ ������ �ʱ�ȭ
            {
                SetNextTargetPos();
            }
        }
    }

    void SetNextTargetPos()//targetPos ����
    {
        float x;
        float y;

        x = Random.Range(areaMin.x, areaMax.x);
        if (transform.position.y > 0)
        {
            y = areaMin.y;
        }
        else
        {
            y = areaMax.y;
        }
        targetPos = new Vector3(x, y);
        moveDirection = targetPos - transform.position;
        moveDirection.Normalize();// �ӵ��� �����ϰ� �ϱ� ���� ���⸸ ���ܳ��� ���̸� 1�� ���ܳ��´�. ����ȭ
    }
}
