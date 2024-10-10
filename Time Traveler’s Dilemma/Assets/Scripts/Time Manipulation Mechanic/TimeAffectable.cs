using System.Collections.Generic;
using UnityEngine;

public class TimeAffectable : MonoBehaviour
{
    private List<StateAtTime> _stateHistory = new List<StateAtTime>();

    private void FixedUpdate()
    {
        if (TimeStateController.Instance.IsRecordable())
        {
            Record();        
        }
    }

    private void Record()
    {
        _stateHistory.Insert(0, new StateAtTime(transform.position, transform.rotation, gameObject.activeSelf));

        // To handle the limit of the list threshold.
        if (_stateHistory.Count > Mathf.Round(TimeStateController.Instance.rewindTime / Time.fixedDeltaTime))
        {
            _stateHistory.RemoveAt(_stateHistory.Count - 1);
        }
    }

    public void Rewind()
    {   
        if (_stateHistory.Count > 0)
        {
            StateAtTime _stateAtTime = _stateHistory[0];
            transform.position = _stateAtTime.position;
            transform.rotation = _stateAtTime.rotation;
            gameObject.SetActive(_stateAtTime.active);

            _stateHistory.RemoveAt(0);
        }
    }

    // Checks if the rewind finishied
    public bool IsRewindComplete()
    {
        return _stateHistory.Count == 0;
    }
}
