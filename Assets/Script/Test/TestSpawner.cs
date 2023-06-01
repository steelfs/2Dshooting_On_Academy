using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestSpawner : TestBase
{
    public Spawner spawner;
    protected override void Test1(InputAction.CallbackContext context)
    {
       spawner.TestSpawn();
        Debug.Log("TestSpawner");

    }

    protected override void Test2(InputAction.CallbackContext context)
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
