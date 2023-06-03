using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster3 : EnemyBase
{
    public GameObject powerUpItem;
    protected override void Die()
    {
        GameObject obj = Instantiate(powerUpItem);
        obj.transform.position = transform.position;
        base.Die();
    }
}
