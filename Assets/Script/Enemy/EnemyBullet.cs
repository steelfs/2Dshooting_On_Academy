using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBullet : EnemyBase
{
    public GameObject Hit_Explosion;
    public float lifeTime = 10.0f;

    protected override void Awake()
    {
        Hit_Explosion = GetComponentInChildren<Explosion>(true).gameObject;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        LifeOver(lifeTime);
    }
  
   
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Hit_Explosion.transform.SetParent(null); //����Ʈ�� �θ� ����
            Hit_Explosion.transform.position = collision.contacts[0].point; //�浹�������� ����Ʈ ��ġ �ű��
            Hit_Explosion.transform.Rotate(0, 0, UnityEngine.Random.Range(0, 360.0f));
            Hit_Explosion.SetActive(true);

            gameObject.SetActive(false);
            //  Hit_Explosion.SetActive(false);
        }
    }
}
