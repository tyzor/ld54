using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    public Camera playerCamera;
    public Camera puzzleCamera;
    public Vector3 puzzleCameraOffset;
    public LayerMask puzzleLayer;
    public PlayerMovement playerMovement;


    private string activePuzzleName;  // Add this line


    void Start()
    {
        puzzleLayer = LayerMask.GetMask("puzzleLayer");
        StartCoroutine(PuzzleResetCoroutine());  // Start the coroutine
    }

    void RandomizePuzzle()
    {

    }

    void CheckPuzzleCompletion()
    {

    }

private IEnumerator PuzzleResetCoroutine()
{
    while (true)
    {
        yield return new WaitForSeconds(1f);  // Add this line or adjust the time as needed
        RandomizePuzzle();
    }
}

public void DetectPuzzle(Ray ray)
{
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, Mathf.Infinity, puzzleLayer))
    {
        PuzzleIdentifier puzzleIdentifier = hit.collider.GetComponent<PuzzleIdentifier>();
        if (puzzleIdentifier != null)
        {
            Debug.Log("Raycast hit on puzzleLayer: " + puzzleIdentifier.puzzleName);
            
            // ... rest of your code ...
            switch(puzzleIdentifier.puzzleType)
            {
                case PuzzleIdentifier.PuzzleType.Wattage:
                    // Handle wattage puzzle
                    ActivatePuzzle(puzzleIdentifier.puzzleName, hit.transform);
                    break;
                case PuzzleIdentifier.PuzzleType.Slider:
                    ActivatePuzzle(puzzleIdentifier.puzzleName, hit.transform);
                    // Handle slider puzzle
                    break;
                // ... other cases for different puzzle types
            }
        }
        else
        {
            Debug.Log("No puzzle detected.");
        }
    }
    else
    {
        Debug.Log("No puzzle detected.");
    }
}

    public void ActivatePuzzle(string puzzleName, Transform puzzleTransform)
{
    activePuzzleName = puzzleName;

    // Disable player movement
    playerMovement.enabled = false;

    // Switch cameras
    playerCamera.gameObject.SetActive(false);  // Updated line
    puzzleCamera.gameObject.SetActive(true);  // Updated line

    // Position the puzzle camera
    puzzleCamera.transform.position = puzzleTransform.position + puzzleCameraOffset;
    puzzleCamera.transform.LookAt(puzzleTransform);
        Debug.Log("ActivatePuzzle called. Player Camera active: " + playerCamera.gameObject.activeSelf + ", Puzzle Camera active: " + puzzleCamera.gameObject.activeSelf);

}


public void DeactivatePuzzle()
{
    // Switch back to the player camera and disable the puzzle camera
    playerCamera.gameObject.SetActive(true);
    puzzleCamera.gameObject.SetActive(false);

    // Re-enable player movement
    playerMovement.enabled = true;

    // Lock the cursor and make it invisible
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;

    // Optionally, you might want to set wattagePuzzle.isActive to false
    //wattagePuzzle.isActive = false;
    Debug.Log("DeactivatePuzzle called. Player Camera active: " + playerCamera.gameObject.activeSelf + ", Puzzle Camera active: " + puzzleCamera.gameObject.activeSelf);

}

void Update()
{
    if (Input.GetKeyDown(KeyCode.Q))
    {
        DeactivatePuzzle();
    }
}
}