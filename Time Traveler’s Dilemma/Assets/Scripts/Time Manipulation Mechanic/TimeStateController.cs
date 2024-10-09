using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStateController : MonoBehaviour
{
    public static TimeStateController Instance;
    private ITimeState _currentState;

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

        FindAllTimeAffectableObjects();
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

    private void FindAllTimeAffectableObjects()
    {
        timeAffectables.Clear();
        TimeAffectable[] affectableObjects = FindObjectsOfType<TimeAffectable>();

        foreach (TimeAffectable obj in affectableObjects)
        {
            timeAffectables.Add(obj);
        }
    }

    public void SetState(ITimeState newState)
    {
        if (_currentState != null)
        {
            _currentState.ExitState();
        }

        _currentState = newState;
        _currentState.EnterState();
    }
}
