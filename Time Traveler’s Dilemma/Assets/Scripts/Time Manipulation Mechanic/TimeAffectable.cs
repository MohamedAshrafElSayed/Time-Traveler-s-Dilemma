using System.Collections.Generic;
using UnityEngine;

public class TimeAffectable : MonoBehaviour
{
    protected List<StateAtTime> _stateHistory = new List<StateAtTime>();
    private bool _isInCameraView = true;
    protected int _rewindIndex = 0;

    protected virtual void Start()
    {
        Register();

        // Clears the list after rewind finishes.
        TimeStateController.Instance._onRewind.AddListener(ClearHistory);
    }

    private void Register()
    {
        // Register this object only if it's in the camera view.
        if (_isInCameraView)
        {
            TimeStateController.Instance.RegisterAffectable(this);
        }
    }

    private void Unregister()
    {
        TimeStateController.Instance.UnregisterAffectable(this);
    }

    private void OnBecameVisible()
    {
        _isInCameraView = true;
        Register();
    }

    private void OnBecameInvisible()
    {
        _isInCameraView = false;
        Unregister();
    }

    private void FixedUpdate()
    {
        // To check if it's not visible due to occlusion culling which will make it unnessacary to record it's properties. 
        if (_isInCameraView && TimeStateController.Instance.IsRecordable())
        {
            Record();        
        }
    }

    protected virtual void Record()
    {
        // To check if there is any significant change happened or not before recording
        if (_stateHistory.Count == 0 ||
         Vector3.Distance(transform.position, _stateHistory[0].position) > 0.2f ||
         Quaternion.Angle(transform.rotation, _stateHistory[0].rotation) > 2f || 
         gameObject.activeSelf != _stateHistory[0].active)
        {
            _stateHistory.Insert(0, new StateAtTime(transform.position, transform.rotation, gameObject.activeSelf));
        }

        // To handle the limit of the list threshold according to the rewind ability time.
        if (_stateHistory.Count > Mathf.Round(TimeStateController.Instance.rewindTime / Time.fixedDeltaTime))
        {
            _stateHistory.RemoveAt(_stateHistory.Count - 1);
        }

        _rewindIndex = 0;
    }

    public virtual void Rewind()
    {   
        if (_stateHistory.Count > 0 && _rewindIndex < _stateHistory.Count)
        {
            StateAtTime _stateAtTime = _stateHistory[_rewindIndex];
            transform.position = _stateAtTime.position;
            transform.rotation = _stateAtTime.rotation;
            gameObject.SetActive(_stateAtTime.active);

            _rewindIndex++;
        }
    }

    public virtual void FastForward()
    {
        if (_rewindIndex > 0)
        {
            _rewindIndex--;

            StateAtTime _stateAtTime = _stateHistory[_rewindIndex];
            transform.position = _stateAtTime.position;
            transform.rotation = _stateAtTime.rotation;
            gameObject.SetActive(_stateAtTime.active);
        }
    }

    private void ClearHistory()
    {
        _stateHistory.Clear();
    }

    // To check if the object finished fast forward or not 
    public bool HasMoreFastForwardStates()
    {
        return _rewindIndex > 0;
    }

    // To check if the object finished rewind or not 
    public bool HasMoreRewindStates()
    {
        return _rewindIndex < _stateHistory.Count;
    }
}
