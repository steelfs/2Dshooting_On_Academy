
using System;
using System.Collections;
using UnityEngine;

public class EnemyBase : PooledObject
{
    [Header("Base data")]
    public float speed = 3.0f;
    public float waitTimeX = 1.0f;  

    GameObject explosion;

    //[SerializeField]
    public int score = 10; //적이 주는 점수
    public int Score => score;

    public int maxHp = 1;
    public int hp = 1;   

    public int Hp
    {
        get => hp;
        protected set
        {
            if(hp != value)
            {
                hp = value;
                if (hp <= 0)
                {
                    Die();
                }
            }
        }
    }

    public Action<int> onDie;
    protected virtual void Awake()
    {
        hp = maxHp;
        explosion = GetComponentInChildren<Explosion>(true).gameObject;
        
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        OnInitialize();
    }

    protected override void OnDisable()
    {
   
        base.OnDisable();
    }
    void Update()
    {
        OnMoveUpdate(); //각 클래스별 이동 업데이트 함수 실행
    }

    protected virtual void OnMoveUpdate() // 업데이트에서 실행되는 이동처리 함수
    {
        transform.Translate(Time.deltaTime * speed * -transform.right); // 그냥 왼쪽으로 이동하기
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Hp--;            
        }
    }
    protected virtual void OnInitialize()
    {
        if (GameManager.Inst.Player != null)
        {
            onDie += GameManager.Inst.Player.AddScore;
        }
       
    }
    protected virtual void Die()
    {
        explosion.transform.SetParent(null);//부모 오브젝트와 같이 destroy되는것을 방지
        //explosion.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360));
        explosion.transform.Rotate(0,0,UnityEngine. Random.Range(0, 360.0f));
        explosion.SetActive(true);

        onDie?.Invoke(score);
        if (GameManager.Inst?.Player != null)
        {
            onDie -= GameManager.Inst.Player.AddScore;
        }
        gameObject.SetActive(false);


 


    }
}

