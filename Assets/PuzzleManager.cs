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
    public WattageCounterPuzzle[] puzzles;
    public Text mainDisplayText;
    public float resetTimer = 300f;
    private int totalWattage;
    private int mainDisplayValue;

    void Start()
    {
        puzzleLayer = LayerMask.GetMask("puzzleLayer");
        //RandomizePuzzle();
        StartCoroutine(PuzzleResetCoroutine());
    }

    public void UpdateCurrentWattage(int newWattage)
    {
        totalWattage += newWattage;
        CheckPuzzleCompletion();
    }

// Commented out the entire RandomizePuzzle method
/*
void RandomizePuzzle()
{
    int[] leverValues = new int[4];
    for (int i = 0; i < 4; i++)
    {
        leverValues[i] = Random.Range(1, 10);
        puzzles[i].wattageValue = leverValues[i];
        puzzles[i].UpdateUI();
    }

    int leversToUse = Random.Range(2, 5);
    mainDisplayValue = 0;
    for (int i = 0; i < leversToUse; i++)
    {
        mainDisplayValue += leverValues[i];
    }
    mainDisplayText.text = "Main Display: " + mainDisplayValue;  // This line should now work with TMP_Text
}
*/

    void CheckPuzzleCompletion()
    {
        if (totalWattage == mainDisplayValue)
        {
            // Mark the puzzle as complete and do other necessary actions
        }
    }

    private IEnumerator PuzzleResetCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(resetTimer);
        // RandomizePuzzle();  // Commented out this line
        }
    }

     public void DetectPuzzle(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, puzzleLayer))
        {
            foreach (WattageCounterPuzzle puzzle in puzzles)
            {
                if (puzzle.transform == hit.transform)
                {
                    // ... do something when a puzzle is detected
                    break;
                }
            }
        }
    }

}
