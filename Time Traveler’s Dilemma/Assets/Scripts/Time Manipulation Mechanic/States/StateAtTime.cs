using UnityEngine;

public class StateAtTime
{
    public Vector3 position;
    public Quaternion rotation;
    public bool active;

    public StateAtTime(Vector3 _position, Quaternion _rotation, bool _active)
    {
        position = _position;
        rotation = _rotation;
        active = _active;
    }
}
