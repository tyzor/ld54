using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public Camera playerCamera;
    public PlayerMovement playerMovement;  // Assume this is your script controlling player movement
    private bool isPuzzleActive = false;
    private float interactionTime = 0f;
    private const float requiredInteractionTime = 2f;
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;
    private GameObject heldObject;

    void Update()
    {
        if (isPuzzleActive)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                DeactivatePuzzle();
            }
            // ... (rest of the puzzle interaction code) ...
        }
        else
        {
            if (Input.GetKey(KeyCode.E))
            {
                interactionTime += Time.deltaTime;
                if (interactionTime >= requiredInteractionTime)
                {
                    ActivatePuzzle();
                }
            }
            else
            {
                interactionTime = 0f;
            }
        }
    }

    void ActivatePuzzle()
    {
        isPuzzleActive = true;
        originalCameraPosition = playerCamera.transform.position;
        originalCameraRotation = playerCamera.transform.rotation;

        // Move camera to focus on the puzzle
        playerCamera.transform.position = transform.position + new Vector3(0, 2, -5);  // Adjust values to focus on puzzle
        playerCamera.transform.LookAt(transform.position);

        // Lock player movement
        playerMovement.enabled = false;
        
        // Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void DeactivatePuzzle()
    {
        isPuzzleActive = false;

        // Restore camera position and rotation
        playerCamera.transform.position = originalCameraPosition;
        playerCamera.transform.rotation = originalCameraRotation;

        // Re-enable player movement
        playerMovement.enabled = true;
        
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // ... (rest of your script) ...
}
