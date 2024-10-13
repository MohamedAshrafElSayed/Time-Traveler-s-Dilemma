using UnityEngine;

public class RewindState : ITimeState
{
    private TimeStateController _controller;
    private float rewindDuration = 15f;
    private float rewindTimer = 0f;

    public RewindState(TimeStateController controller)
    {
        _controller = controller;
        rewindDuration = _controller.rewindTime;
    }

    public void EnterState()
    {
        Debug.Log("Entered Rewind State");
        rewindTimer = rewindDuration;
    }

    public void UpdateState()
    {
        foreach (TimeAffectable obj in _controller.timeAffectables)
        {
            obj.Rewind();
        }
        rewindTimer -= Time.deltaTime;
        if (rewindTimer <= 0)
        {
            _controller.SetState(_controller.pausedState);
        }  
    }

    public void ExitState()
    {
        Debug.Log("Exited Rewind State");
    }
}
