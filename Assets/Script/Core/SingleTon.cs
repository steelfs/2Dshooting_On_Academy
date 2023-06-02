using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component // where T : Component   T = component이다. 다른것은 넣을 수 없다 
{
    // <> 안에는 반드시 컴포넌트만 넣어줘야한다.
    private static bool isShutDown = false;
    private static T instance;
    public static T Instance
    {
        get
        {
            if (isShutDown) //종료처리에 들어간 상황이면 
            {
                Debug.LogWarning("싱글톤은 이미 삭제중이다.");// 경고메세지 출력
                return null;
            }
            if (Instance == null)
            {
                //instance가 없으면 새로 만든다.
                T singleTon = FindObjectOfType<T>();
                if (singleTon == null) // 이미 씬에 싱글톤이 있는지 확인
                {

                    GameObject gameObj = new GameObject();//빈 오브젝트 생성
                    gameObj.name = $"{typeof(T).Name} : SingleTon";             //이름수정
                    gameObj.AddComponent<T>();// 싱글톤 컴포넌트 추가

                }
                instance = singleTon; //instance에 찾았거나 만들어진 객체 대입
                DontDestroyOnLoad(instance.gameObject); //씬이 바뀌거나 사라져도 객체를 파괴하지 않는다.

            }
            return instance; //instance 리턴 (이미 있거나 새로 만들어졌거나)
        }
    }
    public int testI = 0;

    private void Awake()
    {
        if (instance == null)
        {
            //씬에 배치되어있는 첫번째 싱글톤 게임 오브젝트
            instance = this as T;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            //첫번째 싱글톤 게임보으젝트가 만들어진 이후에 생성된 싱글톤이면 
            if (instance != this)
            {
                Destroy(this.gameObject); // 첫번째 싱글톤과 다른 것이면 나중에 만들어진것(이것을) 삭제해라
            }
        }
    }


}
