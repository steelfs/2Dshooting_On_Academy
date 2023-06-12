using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_Effect : PooledObject
{

    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        Destroy(gameObject, animator.GetCurrentAnimatorClipInfo(0)[0].clip.length ) ; //���� ���ϸ������� ù��° Ŭ���� ���� �Ŀ� �����ض�

    }

}
