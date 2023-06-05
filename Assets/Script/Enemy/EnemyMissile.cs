using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : EnemyBase
{
    Transform target;

    public override void OnInitialize()
    {

        target = GameManager.Inst.Player.transform;
    }

    protected override void OnMoveUpdate()
    {
        base.OnMoveUpdate();

        Vector3 dir = target.position - transform.position;
  
        transform.right = -Vector3.Lerp(-transform.right, dir, Time.deltaTime * 0.5f);


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Die();
        }
    }
    //미사일이 어느정도 플레이어에게 접근하면 더이상 추적하지 않는다.
}
