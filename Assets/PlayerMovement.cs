using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Transform cameraTransform;
    private Rigidbody rb;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;  // Ignore vertical component of camera's forward vector
        forward.Normalize();

        Vector3 right = cameraTransform.right;
        right.y = 0f;  // Ignore vertical component of camera's right vector
        right.Normalize();

        Vector3 moveDirection = (forward * verticalInput) + (right * horizontalInput);
        Vector3 moveVelocity = moveDirection * moveSpeed;

        Vector3 newPosition = rb.position + moveVelocity * Time.deltaTime;
        rb.MovePosition(newPosition);
    }
}
