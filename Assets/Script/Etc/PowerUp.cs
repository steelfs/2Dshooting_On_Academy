using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : PooledObject
{
    public float moveSpeed = 3.0f;

    public float dirChangeInterval = 1.0f;

    Vector2 dir;

    public int dirChangeCount;
    public int dirchangeMaxCount = 5;

    public int DirChangeCount
    {
        get => dirChangeCount;
        set 
        {
            dirChangeCount = value;
            if (dirChangeCount < 1)
            {
                StopAllCoroutines();
            }
        }
    }

    Animator anim;

    protected override void OnEnable()
    {
        base.OnEnable();
        StopAllCoroutines();
        StartCoroutine(DirChange());
       anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        transform.Translate(Time.fixedDeltaTime * moveSpeed * dir);
    }
    IEnumerator DirChange()
    {
        while (true)
        {
            //if (dirChangeCount >= dirchangeMaxCount)
            //{
            //    yield break;
            //}
            yield return new WaitForSeconds(dirChangeInterval);
            dir = Random.insideUnitCircle;
            dir.Normalize();
            dirChangeCount--;
            StartCoroutine(PlayAnim());
            Debug.Log(dirChangeCount);
        }     
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dirChangeCount > 0 && collision.gameObject.CompareTag("Boarder"))
        {
            dir = Vector2.Reflect(dir, collision.contacts[0].normal);
            dirChangeCount--;
        }

        if (collision.gameObject.CompareTag("Boarder"))
        {
            dir = Vector2.Reflect(dir, collision.contacts[0].normal);
            dirChangeCount++;
            StartCoroutine(PlayAnim()); 
            Debug.Log(dirChangeCount);
            if(dirChangeCount >= dirchangeMaxCount)
            {
                dir = Vector2.right;
            }
        }
    }
    IEnumerator PlayAnim()
    {
        anim.SetInteger("SetInt", 1);
        yield return new WaitForSeconds(0.5f);
        anim.SetInteger("SetInt", 0);
    }
}
