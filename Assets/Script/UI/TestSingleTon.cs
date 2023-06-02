using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestSingleTon : TestBase
{
    protected override void Test1(InputAction.CallbackContext context)
    {
        SingleTon.Instance.testI = 11;
        Debug.Log(SingleTon.Instance.testI);
    }
    protected override void Test2(InputAction.CallbackContext context)
    {
        SingleTon.Instance.testI = 12;
    }
}
