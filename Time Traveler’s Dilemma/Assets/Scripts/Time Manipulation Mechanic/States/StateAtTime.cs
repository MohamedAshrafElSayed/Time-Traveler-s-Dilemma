using UnityEngine;

public class StateAtTime
{
    public Vector3 position;
    public Quaternion rotation;
    public bool active;
    public int animationStateHash = 0;
    public float normalizedTime = 0f;

    public StateAtTime(Vector3 _position, Quaternion _rotation, bool _active)
    {
        position = _position;
        rotation = _rotation;
        active = _active;
    }

    public StateAtTime(Vector3 _position, Quaternion _rotation, bool _active, int _animationStateHash, float _normalizedTime)
    {
        position = _position;
        rotation = _rotation;
        active = _active;
        animationStateHash = _animationStateHash;
        normalizedTime = _normalizedTime;
    }
}
