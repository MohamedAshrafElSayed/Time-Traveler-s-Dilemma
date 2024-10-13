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
       
    }

    public void UpdateState() 
    {
        foreach (TimeAffectable obj in _controller.timeAffectables)
        {
            obj.FastForward();
        }

        if (AllObjectsFullyFastForwarded())
        {
            _controller.SetState(_controller.normalState);
        }
    }

    public void ExitState()
    {
        Time.timeScale = 1f;
    }

    // To Check if all objects finished fastforwarding.
    private bool AllObjectsFullyFastForwarded()
    {
        foreach (TimeAffectable obj in _controller.timeAffectables)
        {
            if (obj.HasMoreFastForwardStates())
            {
                return false;
            }
        }
        return true;
    }
}
