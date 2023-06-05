using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public float speed = 2.0f;

    public float rangeX = 7.5f;
    public float rangeY = 3.5f;

    float appearTime = 2.0f;

    private void Awake()
    {
        
    }
    private void Start()
    {
        StartCoroutine(AppearCoroutine());
    }
    private void Update()
    {
        transform.Translate(Time.deltaTime * speed * -Vector3.right);
    }

    IEnumerator AppearCoroutine()
    {
        Debug.Log("코루틴 시작");
        yield return new WaitForSeconds(appearTime);
    
        speed = 0;
        yield return new WaitForSeconds(appearTime);
        speed = 2;
    }
}
