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
    public WattagePuzzle wattagePuzzle;
    //public SliderPuzzle sliderPuzzle; //this is another puzzle we need to add like wattage puzzle

    private string activePuzzleName;  // Add this line


    void Start()
    {
        puzzleLayer = LayerMask.GetMask("puzzleLayer");

    }

    void RandomizePuzzle()
    {
      //  wattagePuzzle.RandomizePuzzle();
      //  mainDisplayValue = wattagePuzzle.GetWattageGoal();
      //  mainDisplayText.text = " " + mainDisplayValue;
    }

    void CheckPuzzleCompletion()
    {
       // int currentWattage = wattagePuzzle.GetCurrentWattage();
       // if (currentWattage == mainDisplayValue)
        //{
            // Mark the puzzle as complete and do other necessary actions
        //}
    }

    private IEnumerator PuzzleResetCoroutine()
    {
        while (true)
        {
//            yield return new WaitForSeconds(resetTimer);
            RandomizePuzzle();
        }
    }

public void DetectPuzzle(Ray ray)
{
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, Mathf.Infinity, puzzleLayer))
    {
        Debug.Log("Raycast hit: " + hit.collider.name);
        if (wattagePuzzle.transform == hit.transform)
        {
            wattagePuzzle.isActive = true;

            // Switch to the puzzle camera and disable the player camera
            puzzleCamera.gameObject.SetActive(true);
            playerCamera.gameObject.SetActive(false);

            // Disable player movement
            playerMovement.enabled = false;

            // Unlock the cursor and make it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Debug.Log("Puzzle detected, switching to puzzle camera.");
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
        playerCamera.enabled = false;
        puzzleCamera.enabled = true;

        // Position the puzzle camera
        puzzleCamera.transform.position = puzzleTransform.position + puzzleCameraOffset;
        puzzleCamera.transform.LookAt(puzzleTransform);
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
    wattagePuzzle.isActive = false;
}

void Update()
{
    if (Input.GetKeyDown(KeyCode.Q))
    {
        DeactivatePuzzle();
    }
}


}
