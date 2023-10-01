using UnityEngine;
using UnityEngine.UI;

public class Switch : MonoBehaviour
{
    public int wattageValue;
    public bool isActive = false;
    public bool isSwitchOn = false;
    public Text switchValueText;
    private WattageCounterPuzzle puzzleManager;

    void Start()
    {
        puzzleManager = GetComponentInParent<WattageCounterPuzzle>();
        UpdateUI();
    }

    public void ToggleSwitch()
    {
        if (isActive)
        {
            isSwitchOn = !isSwitchOn;
            puzzleManager.UpdateCurrentWattage();
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        switchValueText.text = "Wattage Value: " + wattageValue;
    }
}
