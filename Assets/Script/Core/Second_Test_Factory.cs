using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Second_Test_Factory : TestBase
{
    public Pool_Object_Type type;
    List<GameObject> _objects = new List<GameObject>();

    protected override void Test1(InputAction.CallbackContext context)
    {
        GameObject obj = Factory.Inst.GetObject(type);
        obj.transform.position = transform.position;
        _objects.Add(obj);
    }
    protected override void Test2(InputAction.CallbackContext context)
    {
        while(_objects.Count > 0)
        {
            GameObject del = _objects[0];
            _objects.RemoveAt(0);
            Destroy(del);
        }
        _objects.Clear();
    }
}
