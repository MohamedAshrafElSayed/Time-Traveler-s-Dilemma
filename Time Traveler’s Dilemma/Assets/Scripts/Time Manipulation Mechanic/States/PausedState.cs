using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedState : ITimeState
{
    private TimeStateController _controller;

    public PausedState(TimeStateController controller)
    {
        _controller = controller;
    }

    public void EnterState()
    {
        Debug.Log("Entered Paused State");
        Time.timeScale = 0f;
    }

    public void UpdateState()
    {
    
    }

    public void ExitState()
    {
        Time.timeScale = 1f;
    }
}
