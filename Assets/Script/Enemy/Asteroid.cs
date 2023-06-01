using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : EnemyBase
{
    //운석은 생성되면 destination 방향이 지정되고 그 방향으로 이동한다.
    // 운석은 항상 반시계방향으로 회전한다.(회전 속도는 랜덤)
    //목적지 스팟 기즈모로 그리기
    Vector3 destination;
  
    private void Awake()
    {
        destination = new Vector3(-20.0f, Random.Range(-5.0f, 5.0f), 0);
    }
    protected override void Die()
    {
        base.Die();
    }
    protected override void OnMoveUpdate()
    {
        transform.Rotate(0,1, 0);
        transform.Translate(Time.deltaTime * speed * (destination - transform.position));
    }
     
}
