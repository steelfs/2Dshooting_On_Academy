using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : EnemyBase
{
    Transform target;

    public override void OnInitialize()
    {
        target = GameManager.Instance.Player.transform;
    }

    protected override void OnMoveUpdate()
    {
        base.OnMoveUpdate();

        Vector3 dir = target.position - transform.position;
        transform.right = -Vector3.Lerp(-transform.right, dir, Time.deltaTime * 0.1f);

        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Die();
        }
    }
    //�̻����� ������� �÷��̾�� �����ϸ� ���̻� �������� �ʴ´�.
}
