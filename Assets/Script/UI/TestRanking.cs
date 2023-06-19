using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestRanking : TestBase
{
    public RankPanel rankPanel;
    public int newScore;
    protected override void Test1(InputAction.CallbackContext context)
    {
        rankPanel.TestSave();
    }
    protected override void Test2(InputAction.CallbackContext context)
    {
        rankPanel.TestLoad();
    }
    protected override void Test3(InputAction.CallbackContext context)
    {
        rankPanel.TestRankUpdate(newScore);
    }
    protected override void Test4(InputAction.CallbackContext context)
    {
        rankPanel.SetDefaultData();
    }
}
