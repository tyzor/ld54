using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WattagePuzzle : MonoBehaviour
{
    public TMP_Text[] leverDisplays;  // Assign your lever_display Text objects here
    public GameObject[] levers;  // Assign your wattage_lever objects here
    public TMP_Text currentWattageText;  // Assign your current_wattage Text object here
    public TMP_Text targetWattageText;  // Assign your main_display Text object here
    private PuzzleManager puzzleManager;
    private int wattageGoal;
    private int currentWattage;

    void Start()
    {
        puzzleManager = GetComponentInParent<PuzzleManager>();
        RandomizePuzzle();
        UpdateUI();
    }

    public void RandomizePuzzle()
    {
        wattageGoal = Random.Range(110, 221);  // Random target number
        int divisor = Random.Range(1, 5);  // Random divisor
        int[] leverValues = new int[4];
        leverValues[0] = wattageGoal / divisor;

        // Generate random fake numbers
        for (int i = 1; i < 4; i++)
        {
            leverValues[i] = Random.Range(10, 61);  // Random numbers between 10 and 60
        }

        // Update lever display texts
        for (int i = 0; i < 4; i++)
        {
            leverDisplays[i].text = leverValues[i].ToString();
        }
    }

    public void ToggleLever(int leverIndex)
    {
        // Toggle lever to add or subtract value from currentWattage
        int leverValue = int.Parse(leverDisplays[leverIndex].text);
        currentWattage += levers[leverIndex].activeSelf ? leverValue : -leverValue;
        levers[leverIndex].SetActive(!levers[leverIndex].activeSelf);  // Toggle lever active state
        UpdateUI();
        CheckPuzzleCompletion();
    }

    public void UpdateUI()
    {
        currentWattageText.text = currentWattage.ToString();
        targetWattageText.text = wattageGoal.ToString();
    }

    public void CheckPuzzleCompletion()
    {
        if (currentWattage == wattageGoal)
        {
            // Mark the puzzle as complete and do other necessary actions
            // ...
        }
    }
}
