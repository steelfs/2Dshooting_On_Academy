using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 오브젝트풀에 들어갈 오브젝트들이 상속받을 클래스
/// </summary>
public class PooledObject : MonoBehaviour
{
    /// <summary>
    /// 게임오브젝트가 비활성화될때 실행되는 델리게이
    /// </summary>
    public Action onDisable;

    protected virtual void OnEnable()
    {
        
    }
  
    protected virtual void OnDisable()
    {
        onDisable?.Invoke(); //비활성화됐다고 알림
    }
    /// <summary>
    /// 일정시간 후 이 게임오ㅡ티젝트를 비활성화시키는 코루
    /// </summary>
    /// <param name="delay">비활성화까지 걸리는 시간</param>
    /// <returns></returns>
    protected IEnumerator LifeOver(float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
