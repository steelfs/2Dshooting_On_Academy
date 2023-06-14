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
    public int powerBonus = 300;
    private int power = 0;
    private int Power
    {
        get => power;
        set
        {
            if (power != value)
            {
                power = value;

                if (power > 3)
                {
                    AddScore(powerBonus);
                }
                power = Mathf.Clamp(power, 1, 3);
                RefreshFirePositions(power);
                Debug.Log(power);

            }
        }
    }
    public float fireAngle = 30.0f;

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

    Transform[] fireTransforms; //�Ѿ� �߻���ġ

    WaitForSeconds fireWait; //ĳ��
    WaitForSeconds flashWait;

    Rigidbody2D rigid;

  

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        fireCoroutine = FireCoroutine(); //�Լ���ü�� ����
        
        Transform fireRoot = transform.GetChild(0); //�߻���ġ ��Ʈ ã��
        fireTransforms= new Transform[fireRoot.childCount]; //��Ʈ�� �ڽ� �� ��ũ �迭 Ȯ��
        for (int i= 0; i < fireTransforms.Length; i++)
        {
            fireTransforms[i] = fireRoot.GetChild(i); // �Ѿ˹߻� Ʈ������ ã��
        }


        fireWait = new WaitForSeconds(fireInterval); //�ڷ�ƾ���� ����� ���͹� �̸� ��������
        flashWait = new WaitForSeconds(0.1f); //ĳ��

        fireFlash = transform.GetChild(1).gameObject;

    }
    private void Start()
    {
        Power = 1;
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
            for (int i= 0; i < Power; i++)
            {
                Transform firePos = fireTransforms[i];
                Factory.Inst.GetObject(Pool_Object_Type.Player_Bullet, firePos.position, firePos.rotation.eulerAngles.z);
            }
           
     

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
        if (collision.gameObject.CompareTag("PowerUp"))
        {

            Power ++;
            collision.gameObject.SetActive(false);
        }
    }
    void RefreshFirePositions(int power) //
    {
        for (int i = 0; i < fireTransforms.Length; i++)
        {
            fireTransforms[i].gameObject.SetActive(false);//��� ���� �߻���ġ�� ��Ȱ��ȭ
        }

        for (int i = 0; i < power; i++)
        {
            //�Ѿ˰� ���̰� 30�� 
            // power 1�� ��  0�� ȸ��
            // 2�϶� 1���� -15�� 1���� 15�� ȸ��
            // 3�� �� -30, 0, 30�� ȸ�� 

            fireTransforms[i].rotation = Quaternion.Euler(0, 0, (power - 1) * (fireAngle * 0.5f) + (i * -fireAngle));//power�� ���۰� ���ϰ� �߰��� i * �߻簢��ŭ �߰�
            fireTransforms[i].localPosition = Vector3.zero;
            fireTransforms[i].Translate(0.5f, 0, 0);

            fireTransforms[i].gameObject.SetActive(true);
        }
    }
}
