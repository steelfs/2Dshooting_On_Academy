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
    Animator anim;
    public int DirChangeCount
    {
        get => dirChangeCount;
        set 
        {
            dirChangeCount = value;
            Debug.Log(dirChangeCount);
            anim.SetInteger("SetInt", dirChangeCount);
            if (dirChangeCount < 1)
            {
                StopAllCoroutines();
            }
        }
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        dirChangeCount = dirchangeMaxCount;
        StopAllCoroutines();
        StartCoroutine(DirChange());
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
            DirChangeCount--;
 
            Debug.Log(dirChangeCount);
        }     
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dirChangeCount > 0 && collision.gameObject.CompareTag("Boarder"))
        {
            dir = Vector2.Reflect(dir, collision.contacts[0].normal);
            DirChangeCount--;
        }
    }
}
