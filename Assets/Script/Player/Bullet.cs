using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public GameObject Hit_Explosion;
    public float speed = 9.0f;
    public float lifeTime = 10.0f;

    //delegate void OnEnemyKill(int score); //델리게이트 선언  리턴 타입 void
    //OnEnemyKill onEnemyKill;
    //public Action<int> onEnemyKill; //적을 Kill했을 때 신호를 보내는 delegate

    private void Awake()
    {
        Hit_Explosion = transform.GetChild(0).gameObject;
    }
    private void Start()
    {
        Destroy(gameObject, lifeTime); //lifeTime 초 후 오브젝트 삭제, 모든 투사체는 수명주기를 정해주는 것이 좋다
    }
    void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector2.right);// Space.Self = 자신을 기준으로 방향이 정해진다.  Space.World 월드를 기준으로 방향이 정해진다.
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Hit_Explosion.transform.SetParent(null); //이펙트의 부모 제거
            Hit_Explosion.transform.position = collision.contacts[0].point; //충돌지점으로 이펙트 위치 옮기기
            Hit_Explosion.transform.Rotate(0, 0, UnityEngine.Random.Range(0, 360.0f));
            Hit_Explosion.SetActive(true);

            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>(); // 태그가 Enemy 이기때문에 EnemyBase가 null이 아니다.
           // onEnemyKill?.Invoke(enemy.Score); // onEnemyKill에 연결된 함수를 모두 실행하기 (하나도 없으면 실행)

            Destroy(gameObject);
        }
    }
}
