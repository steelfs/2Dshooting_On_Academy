using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : EnemyBase
{
    [Header("Boss Data")]
    public Vector2 areaMin = new Vector2(2, -3); //활동영역  min~ max
    public Vector2 areaMax = new Vector2(7, 3);

    public GameObject bulletPrefab; // 일반총알
    public GameObject missilePrefab;// 추적미사일
    public float bulletInterval = 1.0f;
    Transform firePosition1;
    Transform firePosition2;
    Transform firePosition3;
    Vector3 targetPos;
    Vector3 moveDirection;

    protected override void Awake()
    {
        base.Awake();
        firePosition1 = transform.GetChild(1);
        firePosition2 = transform.GetChild(2);
        firePosition3 = transform.GetChild(3);
    }
    protected override void OnInitialize()
    {
  
        Vector3 newPos = transform.position;
        newPos.y = 0.0f;
        transform.position = newPos; // y위치 0 

        StopAllCoroutines(); //자신의 코루틴 모두 정지
        StartCoroutine(AppearProcess());
    }

    IEnumerator AppearProcess()
    {
        float oldSpeed = speed; //기존속도 저장
        speed = 0; // 속도를 0으로 안움직이게 만듦
        float remainDistance = 5; //남은 거리 5

        while (remainDistance > 0.01f) // 남아있는 거리가 거의 0이 될 때 까지 반복
        {
            // 0에서 remainDistance까지 가는 중 Time.deltaTime * 0.5f만큼 진행된 위치
            float deltaMove = Mathf.Lerp(0, remainDistance, Time.deltaTime * 1.2f);
            remainDistance -= deltaMove; // deltamove만큼 남은거리 감소 
            transform.Translate(deltaMove * (-Vector3.right)); //deltamove만큼 왼쪽으로 이동
                       
            yield return null;
        }
        StartCoroutine(BulletFire());
        speed = oldSpeed;
        SetNextTargetPos();
    }
    protected override void OnMoveUpdate()
    {
     
        transform.Translate(Time.deltaTime * speed * moveDirection);
        //if (targetPos == transform.position) //최악의 코드 float 이기 대문에 같은걸 판단하기 힘듦

        //if ((targetPos - transform.position).sqrMagnitude < 0.0001f) //아래 if문 코드가 연산량이 훨씬 적다
        //{
        //    //타겟지점에 거의 가까워지면
        //    SetNextTargetPos();
        //}
        if (transform.position.y > areaMax.y) // 위 코드는 vector의 값을 모두 계산해야하지만 아래 if문은 Y값만 계산을 하기 때문에 연산량이 훨씬 적다
        {
            SetNextTargetPos();
        }
        else if (transform.position.y < areaMin.y)
        {
            SetNextTargetPos();
        }
       
    }

    void SetNextTargetPos()//targetPos 리셋
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
        StartCoroutine(MissileFire());
        targetPos = new Vector3(x, y);
        moveDirection = targetPos - transform.position;
        moveDirection.Normalize();// 속도를 일정하게 하기 위해 방향만 남겨놓고 길이를 1로 남겨놓는다. 정규화
    }

    IEnumerator BulletFire()
    {
        while (true)
        {
            //Instantiate(bulletPrefab, firePosition1.position, Quaternion.identity);
            //Instantiate(bulletPrefab, firePosition2.position, Quaternion.identity);
            GameObject bullet1 = Factory.Inst.GetObject(Pool_Object_Type.Enemy_BossBullet);
            bullet1.transform.position = firePosition1.position;
            GameObject bullet2 = Factory.Inst.GetObject(Pool_Object_Type.Enemy_BossBullet);
            bullet2.transform.position = firePosition2.position;


            yield return new WaitForSeconds(bulletInterval);
        }
       
    }
    IEnumerator MissileFire()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject obj = Factory.Inst.GetObject(Pool_Object_Type.Enemy_BossMissile);
            obj.transform.position = firePosition3.position;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
