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
    public int Score //델리게이트를 사용하면 그냥 set함수에서 호출할 때보다 결합도가 낮아져서 유지보수가 더 수월하다.
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
    public Action<int> OnScoreChange; // 1. 델리게이트를 만들어준다  2. 어디서 호출할지를 지정한다. 호출위치에서 함수이름?.Invoke(매개변수)
                                      // 3. 다른곳에서 델리게이트를 만든 클래스,객체를 찾은 다음 델리게이트 함수에 다른 함수를 연결해준다. 

    public float speed = 2.0f;
    public float fireInterval = 0.2f;
    float boost = 1.0f;
    Vector3 direction;
    PlayerInputAction playerInputAction;
    Animator anim;
    IEnumerator fireCoroutine;//총알 연사용 코루틴
    readonly int inputY_String = Animator.StringToHash("InputY");
    public GameObject bullet;
    public GameObject fireFlash; //총알 발사 이펙트
    GameObject Explosion;

    Transform fireTransform; //총알 발사위치

    WaitForSeconds fireWait; //캐싱
    WaitForSeconds flashWait;

    Rigidbody2D rigid;

  

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        fireCoroutine = FireCoroutine(); //함수자체를 저장
        fireTransform = transform.GetChild(0);
        fireWait = new WaitForSeconds(fireInterval); //코루틴에서 사용할 인터벌 미리 만들어놓기
        flashWait = new WaitForSeconds(0.1f); //캐싱

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
        //GameObject temp = GameObject.Find("FireTransform"); 이름으로 찾기   모든 오브젝트를 검색해야하고 문자열로 찾아야하기 때문에 성느면에서 비효율적이다.
        //GameObject temp2 = GameObject.FindGameObjectWithTag()  태그로 찾기 씬 전부를 찾는다 . 숫자로 변경될수있어서  문자열보다는 빠르긴 하다
        //GameObject temp3 = GameObject.FindObjectOfType<Transform>  //특정 컴포넌트를 가진 오브젝트를 찾는다.
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
            Factory.Inst.GetObject(Pool_Object_Type.Player_Bullet, fireTransform.position);
     

           // Bullet bulletComp = newbullet.GetComponent<Bullet>();
            //bulletComp.onEnemyKill += AddScore; 아래와 같은 코드 OnEnemyKill 에 AddScore함수 등록
            //bulletComp.onEnemyKill += (newScore) => Score += newScore; // 람다식 (newScore 파라미터) 이후는 함수 바디부분
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
        fireFlash.SetActive(true); //활성화
        yield return flashWait;
        fireFlash.SetActive(false); //
    }
    private void OnFire_Stop(InputAction.CallbackContext _)
    {
        StopCoroutine(fireCoroutine);
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //트리거 영역 안에 들어갔다. 겹쳤을 때
    //    //파라미터 Collider2D collision : 상대방의 콜라이더
    //    Debug.Log($"{collision.gameObject.name} 영역에 들어갔다");
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    //트리거 영역안에서 움직일 때.
    //    Debug.Log($"{collision.gameObject.name} 영역에 들어갔다");
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    //트리거 영역에서 나올때 한번 실행 됨
    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //다른 컬라이더랑 충돌한 순간 실행 (겹칠 수 없다)
    //}
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    // 충돌한 상태에서 유지되고있을 때 실행(붙어있을 때)
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    // 떨어졌을 때 실행
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
        anim.SetFloat(inputY_String, direction.y);  //   //anim.SetFloat("InputY", direction.y); 같은 코드

    
        //if (transform.position.x)
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    Explosion.transform.SetParent(null);
        //    Explosion.SetActive(true);
        //    Destroy(gameObject);
        //}
    }
    //delegate = 함수를 변수처럼 , 신호를 보내면 각각 신호를 받은 객체에서 독립된 로직을 실행한다.
}
