using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : PooledObject
{
    public float moveSpeed = 3.0f;

    public float dirChangeInterval = 1.0f;

    Vector2 dir;

    int dirChangeCount;
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
        DirChangeCount = dirchangeMaxCount;
        StopAllCoroutines();
        StartCoroutine(DirChange());
    }
    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * dir);
    }
    IEnumerator DirChange()
    {
        while (true)
        {
            dir = Random.insideUnitCircle;
            dir.Normalize();
      
            yield return new WaitForSeconds(dirChangeInterval);
            DirChangeCount--;     
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
