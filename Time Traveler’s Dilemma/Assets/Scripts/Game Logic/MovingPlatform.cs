using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 moveDirection = new Vector3(1f, 0f, 0f); 
    public float moveDistance = 9f; 
    public float moveSpeed = 9f; 

    private Vector3 startPosition;
    private bool isMoving = false;
    private bool isMoved = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (isMoving && !isMoved)
        {
            MovePlatform();
        }
    }

    private void MovePlatform()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Check if the platform has moved beyond its distance limit
        if (Vector3.Distance(startPosition, transform.position) >= moveDistance)
        {
            isMoving = false; 
            isMoved = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = true;
        }
    }
}
