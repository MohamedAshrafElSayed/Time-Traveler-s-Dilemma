using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorRecorder : TimeAffectable
{
    private Animator _animator;
    private AnimatorStateInfo animState;

    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();

        if (_animator == null)
        {
            Debug.LogError("Animator not found on the player. AnimatorRecorder requires an Animator component.");
        }
    }

    protected override void Record()
    {
        if (_stateHistory.Count == 0 ||
         Vector3.Distance(transform.position, _stateHistory[0].position) > 0.1f ||
         Quaternion.Angle(transform.rotation, _stateHistory[0].rotation) > 1f ||
         gameObject.activeSelf != _stateHistory[0].active)
        {
            animState = _animator.GetCurrentAnimatorStateInfo(0);
            _stateHistory.Insert(0, new StateAtTime(transform.position, transform.rotation, gameObject.activeSelf, animState.shortNameHash, animState.normalizedTime));
        }

        // To handle the limit of the list threshold according to the rewind ability time.
        if (_stateHistory.Count > Mathf.Round(TimeStateController.Instance.rewindTime / Time.fixedDeltaTime))
        {
            _stateHistory.RemoveAt(_stateHistory.Count - 1);
        }

        _rewindIndex = 0;
    }

    public override void Rewind()
    {
        if (_stateHistory.Count > 0 && _rewindIndex < _stateHistory.Count)
        {
            StateAtTime _stateAtTime = _stateHistory[_rewindIndex];
            transform.position = _stateAtTime.position;
            transform.rotation = _stateAtTime.rotation;
            gameObject.SetActive(_stateAtTime.active);
            _animator.Play(_stateAtTime.animationStateHash, 0, _stateAtTime.normalizedTime);

            _rewindIndex++;
        }
    }

    public override void FastForward()
    {
        if (_rewindIndex > 0)
        {
            _rewindIndex--;

            StateAtTime _stateAtTime = _stateHistory[_rewindIndex];
            transform.position = _stateAtTime.position;
            transform.rotation = _stateAtTime.rotation;
            gameObject.SetActive(_stateAtTime.active);
            _animator.Play(_stateAtTime.animationStateHash, 0, _stateAtTime.normalizedTime);
        }
    }
}
