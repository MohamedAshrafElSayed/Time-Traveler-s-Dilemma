using UnityEngine;

public class PausedState : ITimeState
{
    private TimeStateController _controller;
    private float pausedDuration = 5f; 
    private float pausedTimer = 0f;

    public PausedState(TimeStateController controller)
    {
        _controller = controller;
    }

    public void EnterState()
    {
        Debug.Log("Entered Paused State");
        pausedTimer = pausedDuration;
        Time.timeScale = 0f;
    }

    public void UpdateState() 
    {
        // to give resteriction for the player so that he can only stop time for a certain time, after that it goes to normal state
        pausedTimer -= Time.unscaledDeltaTime;
        if (pausedTimer <= 0)
        {
            _controller.SetState(_controller.normalState);
        }
    }

    public void ExitState()
    {
        Time.timeScale = 1f;
    }
}
