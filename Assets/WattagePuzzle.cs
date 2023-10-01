using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WattagePuzzle : MonoBehaviour
{
    public TMP_Text[] leverDisplays;
    public GameObject[] levers;
    public TMP_Text currentWattageText;
    public TMP_Text targetWattageText;
    public Text mainDisplayText;
    public float resetTimer = 300f;

    private int wattageGoal;
    private int currentWattage;
    private int mainDisplayValue;
    public bool isActive { get; set; }

    void Start()
    {

        UpdateUI();
    }

    public void RandomizePuzzle()
    {
        wattageGoal = Random.Range(110, 221);
        int divisor = Random.Range(1, 5);
        int[] leverValues = new int[4];
        leverValues[0] = wattageGoal / divisor;

        for (int i = 1; i < 4; i++)
        {
            leverValues[i] = Random.Range(10, 61);
        }

        for (int i = 0; i < 4; i++)
        {
            leverDisplays[i].text = leverValues[i].ToString();
        }

        mainDisplayValue = wattageGoal;
        mainDisplayText.text = "Main Display: " + mainDisplayValue;
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
        targetWattageText.text = wattageGoal.ToString();
    }

    public void CheckPuzzleCompletion()
    {
        if (currentWattage == mainDisplayValue)
        {
            // Mark the puzzle as complete and do other necessary actions
            // ...
        }
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
