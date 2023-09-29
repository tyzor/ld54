using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float verticalInput = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;  // Ignore vertical component of camera's forward vector
        forward.Normalize();

        Vector3 right = cameraTransform.right;
        right.y = 0f;  // Ignore vertical component of camera's right vector
        right.Normalize();

        Vector3 moveDirection = (forward * verticalInput) + (right * horizontalInput);
        transform.Translate(moveDirection, Space.World);
    }
}
