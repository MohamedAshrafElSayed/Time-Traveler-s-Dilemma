using UnityEngine;

public class StateAtTime
{
    public Vector3 position;
    public Quaternion rotation;

    public StateAtTime(Vector3 _position, Quaternion _rotation) 
    {  
        position = _position; 
        rotation = _rotation;
    }
}
