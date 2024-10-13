using UnityEngine;

public class NormalState : ITimeState
{
    private TimeStateController _controller;

    public NormalState(TimeStateController controller)
    {
        _controller = controller;
    }

    public void EnterState()
    {
        Debug.Log("Entered Normal State");
        Time.timeScale = 1f; 
    }

    public void UpdateState() {}

    public void ExitState() {}
}
