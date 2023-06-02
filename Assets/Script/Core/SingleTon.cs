using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component // where T : Component   T = component�̴�. �ٸ����� ���� �� ���� 
{
    // <> �ȿ��� �ݵ�� ������Ʈ�� �־�����Ѵ�.
    private static bool isShutDown = false;
    private static T instance;
    public static T Instance
    {
        get
        {
            if (isShutDown) //����ó���� �� ��Ȳ�̸� 
            {
                Debug.LogWarning("�̱����� �̹� �������̴�.");// ����޼��� ���
                return null;
            }
            if (Instance == null)
            {
                //instance�� ������ ���� �����.
                T singleTon = FindObjectOfType<T>();
                if (singleTon == null) // �̹� ���� �̱����� �ִ��� Ȯ��
                {

                    GameObject gameObj = new GameObject();//�� ������Ʈ ����
                    gameObj.name = $"{typeof(T).Name} : SingleTon";             //�̸�����
                    gameObj.AddComponent<T>();// �̱��� ������Ʈ �߰�

                }
                instance = singleTon; //instance�� ã�Ұų� ������� ��ü ����
                DontDestroyOnLoad(instance.gameObject); //���� �ٲ�ų� ������� ��ü�� �ı����� �ʴ´�.

            }
            return instance; //instance ���� (�̹� �ְų� ���� ��������ų�)
        }
    }
    public int testI = 0;

    private void Awake()
    {
        if (instance == null)
        {
            //���� ��ġ�Ǿ��ִ� ù��° �̱��� ���� ������Ʈ
            instance = this as T;
            DontDestroyOnLoad(instance.gameObject);
        }
        else
        {
            //ù��° �̱��� ���Ӻ�����Ʈ�� ������� ���Ŀ� ������ �̱����̸� 
            if (instance != this)
            {
                Destroy(this.gameObject); // ù��° �̱���� �ٸ� ���̸� ���߿� ���������(�̰���) �����ض�
            }
        }
    }


}