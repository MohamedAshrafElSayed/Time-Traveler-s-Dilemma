using UnityEngine;
using UnityEngine.Events;

public class FallingPlatform : MonoBehaviour
{
    public UnityEvent _onHit;
    public Material redMaterial;

    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private bool isFalling = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isFalling)
        {
            isFalling = true;
            DropTrap();
        }
    }

    private void DropTrap()
    {
        _rigidbody.isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // As if spikes appears on the trap when the player touches it.
            _meshRenderer.material = redMaterial;
            _onHit.Invoke();
        }
    }
}
