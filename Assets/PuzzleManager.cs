using System.Collections;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public Camera playerCamera;
    public Camera puzzleCamera;
    public Vector3 puzzleCameraOffset;
    public LayerMask puzzleLayer;
    public PlayerMovement playerMovement;
    public Puzzle[] puzzles;  // Reference to an array of Puzzle script objects
    public float resetTimer = 300f;  // Set the timer for resetting puzzles

    private Puzzle selectedPuzzle;
    private Transform puzzleTransform;
    private bool isPuzzleActive = false;

    void Start()
    {
        puzzleLayer = LayerMask.GetMask("puzzleLayer");
        StartCoroutine(PuzzleResetCoroutine());
    }

public void DetectPuzzle(Ray ray)  // Change this line to make the method public
{
    RaycastHit hit;

    Debug.DrawLine(ray.origin, ray.origin + ray.direction * 10f, Color.red, 2f);  // Visualize the raycast

    if (Physics.Raycast(ray, out hit, Mathf.Infinity, puzzleLayer))
    {
        foreach (Puzzle puzzle in puzzles)
        {
            if (puzzle.puzzleTransform == hit.transform)
            {
                selectedPuzzle = puzzle;
                puzzleTransform = hit.transform;
                ActivatePuzzle();
                break;
            }
        }
    }
}

    void ActivatePuzzle()
    {
        if (selectedPuzzle != null)
        {
            isPuzzleActive = true;

            // Set the position and orientation of the puzzle camera
            Vector3 puzzleCameraPosition = selectedPuzzle.puzzleTransform.position + puzzleCameraOffset;
            puzzleCamera.transform.position = puzzleCameraPosition;
            puzzleCamera.transform.LookAt(selectedPuzzle.puzzleTransform);

            // Enable the puzzle camera and disable the player camera
            puzzleCamera.gameObject.SetActive(true);
            playerCamera.gameObject.SetActive(false);

            // Disable player movement
            playerMovement.enabled = false;

            // Unlock the cursor and make it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Debug.LogWarning("No puzzle selected. Cannot activate puzzle.");
        }
    }

    void Update()
    {
        if (isPuzzleActive)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                DeactivatePuzzle();
            }
        }
    }

    void DeactivatePuzzle()
    {
        isPuzzleActive = false;

        // Enable the player camera and disable the puzzle camera
        playerCamera.gameObject.SetActive(true);
        puzzleCamera.gameObject.SetActive(false);

        // Re-enable player movement
        playerMovement.enabled = true;

        // Lock the cursor and make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private IEnumerator PuzzleResetCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(resetTimer);

            int randomIndex = Random.Range(0, puzzles.Length);
            Puzzle puzzleToReset = puzzles[randomIndex];
            puzzleToReset.isCompleted = false;
            ResetPuzzle(puzzleToReset);
        }
    }

    private void ResetPuzzle(Puzzle puzzle)
    {
        // Implement the logic to reset the physical state of the puzzle
    }
}
