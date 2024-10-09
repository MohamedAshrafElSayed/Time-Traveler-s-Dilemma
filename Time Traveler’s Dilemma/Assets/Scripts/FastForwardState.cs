using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastForwardState : ITimeState
{
    private TimeStateController _controller;

    public FastForwardState(TimeStateController controller)
    {
        _controller = controller;
    }

    public void EnterState()
    {
        Debug.Log("Entered Fast Forward State");
        Time.timeScale = 2.0f;
    }

    public void UpdateState()
    {

    }

    public void ExitState()
    {
        Time.timeScale = 1f;
    }
}
