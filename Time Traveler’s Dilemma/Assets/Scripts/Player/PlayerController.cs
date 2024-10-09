using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    private Rigidbody _rigidbody;
    private bool _isGrounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (TimeStateController.Instance.IsRecordable())
        {
            MovePlayer();
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                Jump();
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
            _isGrounded = true; 
        }
    }
}
