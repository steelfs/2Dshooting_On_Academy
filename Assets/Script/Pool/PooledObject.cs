using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ƮǮ�� �� ������Ʈ���� ��ӹ��� Ŭ����
/// </summary>
public class PooledObject : MonoBehaviour
{
    /// <summary>
    /// ���ӿ�����Ʈ�� ��Ȱ��ȭ�ɶ� ����Ǵ� ��������
    /// </summary>
    public Action onDisable;

    protected virtual void OnEnable()
    {
        
    }
  
    protected virtual void OnDisable()
    {
        onDisable?.Invoke(); //��Ȱ��ȭ�ƴٰ� �˸�
    }
    /// <summary>
    /// �����ð� �� �� ���ӿ���Ƽ��Ʈ�� ��Ȱ��ȭ��Ű�� �ڷ�
    /// </summary>
    /// <param name="delay">��Ȱ��ȭ���� �ɸ��� �ð�</param>
    /// <returns></returns>
    protected IEnumerator LifeOver(float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
