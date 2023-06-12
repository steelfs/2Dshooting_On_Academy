
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
    public int score = 10; //���� �ִ� ����
    public int Score => score;

    public int maxHp = 1;
    public int hp = 1;

    Player targetPlayer = null;

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
        if (GameManager.Inst?.Player != null)
        {
            onDie -= GameManager.Inst.Player.AddScore;
        }
        base.OnDisable();
    }
    void Update()
    {
        OnMoveUpdate(); //�� Ŭ������ �̵� ������Ʈ �Լ� ����
    }

    protected virtual void OnMoveUpdate() // ������Ʈ���� ����Ǵ� �̵�ó�� �Լ�
    {
        transform.Translate(Time.deltaTime * speed * -transform.right); // �׳� �������� �̵��ϱ�
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
        if (targetPlayer == null)
        {
            targetPlayer = GameManager.Inst.Player;
        }
        if (GameManager.Inst.Player != null)
        {
            onDie += GameManager.Inst.Player.AddScore;
        }
       
    }
    protected virtual void Die()
    {
        explosion.transform.SetParent(null);//�θ� ������Ʈ�� ���� destroy�Ǵ°��� ����
        //explosion.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0.0f, 360));
        explosion.transform.Rotate(0,0,UnityEngine. Random.Range(0, 360.0f));
        explosion.SetActive(true);

        onDie?.Invoke(score);
       
        gameObject.SetActive(false);
    }
}
///CallStack �����â���� �ึ�����κ��� ���� ����� ���̴�.
