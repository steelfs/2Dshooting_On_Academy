using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Player : MonoBehaviour
{
    public float moveableRangeX = 8.2f; 
    public float moveableRangeY = 4.3f;

    int score = 0;
    public int Score //��������Ʈ�� ����ϸ� �׳� set�Լ����� ȣ���� ������ ���յ��� �������� ���������� �� �����ϴ�.
    {
        get => score;
        set
        {
            if (score != value)
            {
                score = value;
            }
            //OnScoreChange?.Invoke(score);
            score = value;

            
        }
    }
    public void AddScore(int newscore)
    {
        Score += newscore;
    }
    public Action<int> OnScoreChange; // 1. ��������Ʈ�� ������ش�  2. ��� ȣ�������� �����Ѵ�. ȣ����ġ���� �Լ��̸�?.Invoke(�Ű�����)
                                      // 3. �ٸ������� ��������Ʈ�� ���� Ŭ����,��ü�� ã�� ���� ��������Ʈ �Լ��� �ٸ� �Լ��� �������ش�. 

    public float speed = 2.0f;
    public float fireInterval = 0.2f;
    float boost = 1.0f;
    Vector3 direction;
    PlayerInputAction playerInputAction;
    Animator anim;
    IEnumerator fireCoroutine;//�Ѿ� ����� �ڷ�ƾ
    readonly int inputY_String = Animator.StringToHash("InputY");
    public GameObject bullet;
    public GameObject fireFlash; //�Ѿ� �߻� ����Ʈ
    GameObject Explosion;

    Transform fireTransform; //�Ѿ� �߻���ġ

    WaitForSeconds fireWait; //ĳ��
    WaitForSeconds flashWait;

    Rigidbody2D rigid;

  

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        fireCoroutine = FireCoroutine(); //�Լ���ü�� ����
        fireTransform = transform.GetChild(0);
        fireWait = new WaitForSeconds(fireInterval); //�ڷ�ƾ���� ����� ���͹� �̸� ��������
        flashWait = new WaitForSeconds(0.1f); //ĳ��

        fireFlash = transform.GetChild(1).gameObject;
        Explosion = transform.GetChild(2).gameObject;

    }
    private void OnEnable()
    {
        playerInputAction.Player.Enable();
        playerInputAction.Player.Move.performed += OnMove;
        playerInputAction.Player.Move.canceled += OnMove;
        playerInputAction.Player.Boost.performed += OnBoost;
        playerInputAction.Player.Boost.canceled += OnBoost;
        playerInputAction.Player.Fire.performed += OnFire_Start;
        playerInputAction.Player.Fire.canceled += OnFire_Stop;
    }
    private void OnDisable()
    {
        playerInputAction.Player.Move.performed -= OnMove;
        playerInputAction.Player.Move.canceled -= OnMove;
        playerInputAction.Player.Boost.performed -= OnBoost;
        playerInputAction.Player.Boost.canceled -= OnBoost;
        playerInputAction.Player.Fire.performed -= OnFire_Start;
        playerInputAction.Player.Fire.canceled -= OnFire_Stop;
        playerInputAction.Player.Disable();
    }
    private void OnFire_Start(InputAction.CallbackContext _)
    {
        //GameObject temp = GameObject.Find("FireTransform"); �̸����� ã��   ��� ������Ʈ�� �˻��ؾ��ϰ� ���ڿ��� ã�ƾ��ϱ� ������ �����鿡�� ��ȿ�����̴�.
        //GameObject temp2 = GameObject.FindGameObjectWithTag()  �±׷� ã�� �� ���θ� ã�´� . ���ڷ� ����ɼ��־  ���ڿ����ٴ� ������ �ϴ�
        //GameObject temp3 = GameObject.FindObjectOfType<Transform>  //Ư�� ������Ʈ�� ���� ������Ʈ�� ã�´�.
        //Transform child = transform.GetChild(0);
        //bullet.transform.position = child.position;
        //bullet.transform.rotation = child.rotation;

        //Bullet.transform.position.x = this.transform.position.x + 2;
        StartCoroutine(fireCoroutine);
    }
  
    IEnumerator FireCoroutine()
    {
        while (true)
        {
            GameObject newbullet = Instantiate(bullet);
            newbullet.transform.position = fireTransform.position;

           // Bullet bulletComp = newbullet.GetComponent<Bullet>();
            //bulletComp.onEnemyKill += AddScore; �Ʒ��� ���� �ڵ� OnEnemyKill �� AddScore�Լ� ���
            //bulletComp.onEnemyKill += (newScore) => Score += newScore; // ���ٽ� (newScore �Ķ����) ���Ĵ� �Լ� �ٵ�κ�
            StartCoroutine(FlashEffect());

            yield return fireWait;
        }
    }

    //System.Func<int, float> onTest;
    //void Test()
    //{
    //    onTest += (testScore) => testScore + 3.45f;
    //}

    IEnumerator FlashEffect()
    {
        fireFlash.SetActive(true); //Ȱ��ȭ
        yield return flashWait;
        fireFlash.SetActive(false); //
    }
    private void OnFire_Stop(InputAction.CallbackContext _)
    {
        StopCoroutine(fireCoroutine);
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //Ʈ���� ���� �ȿ� ����. ������ ��
    //    //�Ķ���� Collider2D collision : ������ �ݶ��̴�
    //    Debug.Log($"{collision.gameObject.name} ������ ����");
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    //Ʈ���� �����ȿ��� ������ ��.
    //    Debug.Log($"{collision.gameObject.name} ������ ����");
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    //Ʈ���� �������� ���ö� �ѹ� ���� ��
    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //�ٸ� �ö��̴��� �浹�� ���� ���� (��ĥ �� ����)
    //}
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    // �浹�� ���¿��� �����ǰ����� �� ����(�پ����� ��)
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    // �������� �� ����
    //}


    // Update is called once per frame
    private void FixedUpdate()
    {
        //transform.position += Time.deltaTime * speed * boost * direction;
        rigid.MovePosition(rigid.position + (Vector2)(Time.deltaTime * speed * boost * direction));
    }

 
    private void OnBoost(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            boost = 1.0f;
        }
        else
        {
            boost = 5.0f;
        }
    }



    private void OnMove(InputAction.CallbackContext context)
    {       
        Vector2 value = context.ReadValue<Vector2>();
        direction = value;  
        anim.SetFloat(inputY_String, direction.y);  //   //anim.SetFloat("InputY", direction.y); ���� �ڵ�

    
        //if (transform.position.x)
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Explosion.transform.SetParent(null);
            Explosion.SetActive(true);
            Destroy(gameObject);
        }
    }
    //delegate = �Լ��� ����ó�� , ��ȣ�� ������ ���� ��ȣ�� ���� ��ü���� ������ ������ �����Ѵ�.
}
