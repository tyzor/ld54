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
    public WattagePuzzle wattagePuzzle;  // Assuming there's only one WattagePuzzle per PuzzleManager
    public Text mainDisplayText;
    public float resetTimer = 300f;
    private int mainDisplayValue;

    void Start()
    {
        puzzleLayer = LayerMask.GetMask("puzzleLayer");
        //RandomizePuzzle();
        StartCoroutine(PuzzleResetCoroutine());
    }

    void RandomizePuzzle()
    {
        wattagePuzzle.RandomizePuzzle();  // Call RandomizePuzzle method on WattagePuzzle
        mainDisplayValue = wattagePuzzle.GetWattageGoal();  // Assuming you have a method to retrieve the wattageGoal
        mainDisplayText.text = "Main Display: " + mainDisplayValue;
    }

    void CheckPuzzleCompletion()
    {
        int currentWattage = wattagePuzzle.GetCurrentWattage();  // Assuming you have a method to retrieve the currentWattage
        if (currentWattage == mainDisplayValue)
        {
            // Mark the puzzle as complete and do other necessary actions
        }
    }

    private IEnumerator PuzzleResetCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(resetTimer);
            RandomizePuzzle();
        }
    }

    public void DetectPuzzle(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, puzzleLayer))
        {
            if (wattagePuzzle.transform == hit.transform)
            {
                wattagePuzzle.isActive = true;  // Assuming you have an isActive property
                // ... any other logic when a puzzle is detected
            }
        }
    }
}
