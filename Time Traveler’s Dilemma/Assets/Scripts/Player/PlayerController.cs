using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 10f;

    private Rigidbody _rigidbody;
    private bool _isGrounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // To restrict the player movement when in pause or rewinding.
        if (TimeStateController.Instance.IsRecordable())
        {
            if (_rigidbody.isKinematic != false)
            {
                _rigidbody.isKinematic = false;
            }

            MovePlayer();

            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                Jump();
                Physics.gravity = new Vector3(0f, -20f, 0f);
            }
        }
    }

    private void MovePlayer()
    {
        float moveInput = Input.GetAxis("Horizontal"); 
        Vector3 move = new Vector3(moveInput * moveSpeed, _rigidbody.velocity.y, 0f);
        _rigidbody.velocity = move;  
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);  
        _isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Physics.gravity = new Vector3(0f, -9.8f, 0f);
            _isGrounded = true; 
        }
    }
}
