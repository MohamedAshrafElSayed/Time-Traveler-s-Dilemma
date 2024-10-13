using System.Collections.Generic;
using UnityEngine;

public class TimeStateController : MonoBehaviour
{
    public static TimeStateController Instance;
    private ITimeState _currentState;
    private bool _hasRewound = false;

    [HideInInspector]
    public List<TimeAffectable> timeAffectables = new List<TimeAffectable>();
    public float rewindTime = 15f;

    public ITimeState pausedState;
    public ITimeState fastForwardState;
    public ITimeState rewindState;
    public ITimeState normalState;

    private void Awake()
    {
        Instance = this;
        pausedState = new PausedState(this);
        fastForwardState = new FastForwardState(this);
        rewindState = new RewindState(this);
        normalState = new NormalState(this); 
    }

    private void Start()
    {
        SetState(normalState);
    }

    private void Update()
    {
        _currentState.UpdateState();
        HandleTimeInput();
    }

    //To ensure only recording the position when the time is moving forward (Time.timeScale > 0) and we're not in rewind mode
    public bool IsRecordable()
    {
        return _currentState == normalState || _currentState == fastForwardState;
    }

    private void HandleTimeInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetState(pausedState);
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            SetState(fastForwardState);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SetState(rewindState);
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            SetState(normalState); 
        }
    }

    // To only register objects visible to the player.
    public void RegisterAffectable(TimeAffectable obj)
    {
        if (!timeAffectables.Contains(obj))
        {
            timeAffectables.Add(obj);
        }
    }

    public void UnregisterAffectable(TimeAffectable obj)
    {
        if (timeAffectables.Contains(obj))
        {
            timeAffectables.Remove(obj);
        }
    }

    public void SetState(ITimeState newState)
    {
        if (_currentState == rewindState)
        {
            _hasRewound = true;
        }

        if (newState == fastForwardState && !_hasRewound)
        {
            Debug.Log("Cannot Fast Forward without Rewind.");
            return;
        }

        if (_currentState != null)
        {
            _currentState.ExitState();
        }

        _currentState = newState;
        _currentState.EnterState();

        if (_currentState == normalState)
        {
            _hasRewound = false;
        }
    }
}
