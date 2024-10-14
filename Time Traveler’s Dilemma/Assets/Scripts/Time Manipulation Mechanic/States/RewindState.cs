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
        }
        
        if (AllObjectsFullyRewinded())
        {
            _controller.SetState(_controller.pausedState);
        }  
    }

    public void ExitState()
    {
        Debug.Log("Exited Rewind State");
    }

    // To Check if all objects finished fastforwarding.
    private bool AllObjectsFullyRewinded()
    {
        foreach (TimeAffectable obj in _controller.timeAffectables)
        {
            if (obj.HasMoreRewindStates())
            {
                return false;
            }
        }
        return true;
    }
}
