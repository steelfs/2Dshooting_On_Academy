using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public GameObject Hit_Explosion;
    public float speed = 9.0f;
    public float lifeTime = 10.0f;

    //delegate void OnEnemyKill(int score); //��������Ʈ ����  ���� Ÿ�� void
    //OnEnemyKill onEnemyKill;
    //public Action<int> onEnemyKill; //���� Kill���� �� ��ȣ�� ������ delegate

    private void Awake()
    {
        Hit_Explosion = transform.GetChild(0).gameObject;
    }
    private void Start()
    {
        Destroy(gameObject, lifeTime); //lifeTime �� �� ������Ʈ ����, ��� ����ü�� �����ֱ⸦ �����ִ� ���� ����
    }
    void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector2.right);// Space.Self = �ڽ��� �������� ������ ��������.  Space.World ���带 �������� ������ ��������.
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Hit_Explosion.transform.SetParent(null); //����Ʈ�� �θ� ����
            Hit_Explosion.transform.position = collision.contacts[0].point; //�浹�������� ����Ʈ ��ġ �ű��
            Hit_Explosion.transform.Rotate(0, 0, UnityEngine.Random.Range(0, 360.0f));
            Hit_Explosion.SetActive(true);

            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>(); // �±װ� Enemy �̱⶧���� EnemyBase�� null�� �ƴϴ�.
           // onEnemyKill?.Invoke(enemy.Score); // onEnemyKill�� ����� �Լ��� ��� �����ϱ� (�ϳ��� ������ ����)

            Destroy(gameObject);
        }
    }
}
