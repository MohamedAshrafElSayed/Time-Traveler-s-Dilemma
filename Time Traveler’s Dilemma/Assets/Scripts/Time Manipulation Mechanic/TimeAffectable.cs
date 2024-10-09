using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAffectable : MonoBehaviour
{
    private List<StateAtTime> _stateHistory = new List<StateAtTime>();
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (TimeStateController.Instance.IsRecordable())
        {
            if (_rigidbody.isKinematic != false) 
            {
                _rigidbody.isKinematic = false;
            }
            Record();        
        }
    }

    private void Record()
    {
        _stateHistory.Insert(0, new StateAtTime(transform.position, transform.rotation));

        if (_stateHistory.Count > Mathf.Round(TimeStateController.Instance.rewindTime / Time.fixedDeltaTime))
        {
            _stateHistory.RemoveAt(_stateHistory.Count - 1);
        }
    }

    public void Rewind()
    {   
        if (_stateHistory.Count > 0)
        {
            _rigidbody.isKinematic = true;

            StateAtTime _stateAtTime = _stateHistory[0];
            transform.position = _stateAtTime.position;
            transform.rotation = _stateAtTime.rotation;

            _stateHistory.RemoveAt(0);
        }
    }

    public bool IsRewindComplete()
    {
        return _stateHistory.Count == 0;
    }
}
