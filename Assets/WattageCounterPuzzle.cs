using UnityEngine;
using UnityEngine.UI;

public class WattageCounterPuzzle : MonoBehaviour
{
    public int targetWattage;
    public int currentWattage;
    public Switch[] switches;
    public Text targetWattageText;
    public Text currentWattageText;
    public bool isCompleted = false;

    void Start()
    {
        InitializePuzzle();
    }

    void InitializePuzzle()
    {
        // Randomly set target wattage between 100 and 300
        targetWattage = Random.Range(100, 301);

        // Randomly set the number of active switches between 4 and 8
        int activeSwitchCount = Random.Range(4, 9);

        // Divide the target wattage among the active switches
        int wattagePerSwitch = targetWattage / activeSwitchCount;

        for (int i = 0; i < activeSwitchCount; i++)
        {
            switches[i].wattageValue = wattagePerSwitch;
            switches[i].isActive = true;
        }

        UpdateUI();
    }

    public void UpdateCurrentWattage()
    {
        currentWattage = 0;
        foreach (Switch sw in switches)
        {
            if (sw.isSwitchOn)
            {
                currentWattage += sw.wattageValue;
            }
        }

        UpdateUI();

        // Check for puzzle completion
        if (currentWattage == targetWattage)
        {
            isCompleted = true;
            Debug.Log("Puzzle Completed!");
        }
    }

    void UpdateUI()
    {
        targetWattageText.text = "Target Wattage: " + targetWattage;
        currentWattageText.text = "Current Wattage: " + currentWattage;
    }
}
