using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WattagePuzzle : MonoBehaviour
{
    public TMP_Text[] leverDisplays;
    public GameObject[] levers;
    public TMP_Text currentWattageText;
    public TMP_Text targetWattageText;
    private int wattageGoal;
    private int currentWattage;
    public bool isActive { get; set; }

    void Start()
    {
        RandomizePuzzle();
        UpdateUI();
    }

    public void RandomizePuzzle()
    {
        wattageGoal = Random.Range(110, 221);
        int correctLeversCount = Random.Range(2, 5);  // Generates 2, 3, or 4
        int fakeLeversCount = 4 - correctLeversCount;
        
        // This assumes that the order of levers in the array doesn't matter
        for (int i = 0; i < correctLeversCount; i++)
        {
            int value = Random.Range(10, 61);
            leverDisplays[i].text = value.ToString();
        }

        for (int i = correctLeversCount; i < 4; i++)
        {
            int value = Random.Range(1, 10);  // Assuming fake levers have values between 1 and 9
            leverDisplays[i].text = value.ToString();
        }

        targetWattageText.text = wattageGoal.ToString();
    }

    public void ToggleLever(int leverIndex)
    {
        int leverValue = int.Parse(leverDisplays[leverIndex].text);
        currentWattage += levers[leverIndex].activeSelf ? leverValue : -leverValue;
        levers[leverIndex].SetActive(!levers[leverIndex].activeSelf);
        UpdateUI();
        CheckPuzzleCompletion();
    }

    public void UpdateUI()
    {
        currentWattageText.text = currentWattage.ToString();
    }

    public void CheckPuzzleCompletion()
    {
        if (currentWattage == wattageGoal)
        {
            Debug.Log("Puzzle Completed!");
            // Mark the puzzle as complete and do other necessary actions
            // ...
            Invoke("DeactivatePuzzle", 2f);  // Deactivate the puzzle after 2 seconds
        }
    }

    private void DeactivatePuzzle()
    {
        isActive = false;
        // ... any other deactivation logic ...
    }

    public int GetWattageGoal()
    {
        return wattageGoal;
    }

    public int GetCurrentWattage()
    {
        return currentWattage;
    }
}
