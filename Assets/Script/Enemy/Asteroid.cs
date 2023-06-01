using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : EnemyBase
{
    //��� �����Ǹ� destination ������ �����ǰ� �� �������� �̵��Ѵ�.
    // ��� �׻� �ݽð�������� ȸ���Ѵ�.(ȸ�� �ӵ��� ����)
    //������ ���� ������ �׸���
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
