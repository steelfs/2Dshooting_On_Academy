using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public float speed = 3.0f;
    private void FixedUpdate()
    {
        transform.Translate(Time.fixedDeltaTime * speed * -transform.right);
    }
}
