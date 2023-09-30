using UnityEngine;

public class TabletFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;  // Offset of the tablet relative to the player
    public float rotationSpeed = 700f;  // Speed of rotation

    void Update()
    {
        // Update the position of the tablet based on the player's position and the offset
        transform.position = player.position + offset;

        // Get the player's turning input
        float turnInput = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        // Rotate the tablet along with the player based on input
        transform.RotateAround(player.position, Vector3.up, turnInput);
    }
}
