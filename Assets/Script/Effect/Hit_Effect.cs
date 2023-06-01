using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_Effect : MonoBehaviour
{

    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        Destroy(gameObject, animator.GetCurrentAnimatorClipInfo(0)[0].clip.length) ; //현재 에니메이터의 첫번째 클립의 길이 후에 삭제해라

    }

}
