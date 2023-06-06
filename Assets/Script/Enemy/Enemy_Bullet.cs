using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : EnemyBase
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Die();
        }
    }
}
