using UnityEngine;

public class TabletFollow : MonoBehaviour
{
    public Transform player;
    public float yOffset = 1f;  // Distance from the player's position on the Y axis
    public float yFactor = 0.5f;  // Factor by which the tablet's Y position should change relative to the player's Y position

    void Update()
    {
        // Calculate the new position for the tablet
        Vector3 newPosition = new Vector3(
            player.position.x,
            transform.position.y + (player.position.y - transform.position.y) * yFactor,
            player.position.z
        );

        // Add the yOffset to the Y position
        newPosition.y += yOffset;

        // Set the tablet's position to the new position
        transform.position = newPosition;
    }
}
