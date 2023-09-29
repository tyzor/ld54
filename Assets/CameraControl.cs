using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // Clamps the vertical rotation to prevent flipping

        // Apply rotation to player for left and right movement
        player.Rotate(Vector3.up * mouseX);
        // Set the camera's rotation directly for up and down movement
        transform.localRotation = Quaternion.Euler(xRotation, player.eulerAngles.y, 0f);

        transform.position = player.position;  // Keeps the camera at the player's position
    }
}
