using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public float speed = 5.0f;
    Vector3 dir;
    Vector3 targetPos;
    float rangeX = 8.5f;
    float rangeY = 4.5f;
    private void Awake()
    {
        dir = new Vector3(Random.Range(-rangeX, rangeX), Random.Range(-rangeY, rangeY), 0);
        targetPos = dir - transform.position;
        targetPos.Normalize();
    }
    void Update()
    {
        transform.Translate(Time.deltaTime * speed * targetPos);
        if (transform.position.x > rangeX || transform.position.x < -rangeX || transform.position.y > rangeY || transform.position.y < -rangeY)
        {
            ResetTargetPos();
        }
    }
    void ResetTargetPos()
    {
        dir = new Vector3(Random.Range(-rangeX, rangeX), Random.Range(-rangeY, rangeY), 0);
    }
}
