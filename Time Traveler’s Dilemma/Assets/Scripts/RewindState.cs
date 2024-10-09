using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindState : ITimeState
{
    private TimeStateController _controller;

    public RewindState(TimeStateController controller)
    {
        _controller = controller;
    }

    public void EnterState()
    {
        Debug.Log("Entered Rewind State");
    }

    public void UpdateState()
    {
        foreach (TimeAffectable obj in _controller.timeAffectables)
        {
            obj.Rewind();

            if (obj.IsRewindComplete())
            {
                _controller.SetState(_controller.normalState);
            }
        }
    }

    public void ExitState()
    {
        Debug.Log("Exited Rewind State");
    }
}
