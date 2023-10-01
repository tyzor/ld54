using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    public Camera playerCamera;  // Define the camera reference here
    public float mouseSensitivity = 100f;
    public float fixedCameraHeight = 2f;  // Set this to the desired fixed height
    private float xRotation = 0f;

    void Start()
    {
        playerCamera.transform.rotation = Quaternion.Euler(0, 0, 0);  // Reset rotation to look straight ahead
        // ... rest of your code ...
    }

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

        // Keep the camera's Y position static while following the player's X and Z positions
        transform.position = new Vector3(player.position.x, fixedCameraHeight, player.position.z);
    }
}
