using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WattageCounterPuzzle : MonoBehaviour
{
    public int wattageValue;
    public bool isActive = false;
    public bool isSwitchOn = false;
    public TMP_Text switchValueText;
    private PuzzleManager puzzleManager;
    private Animator animator;

    void Start()
    {
        puzzleManager = GetComponentInParent<PuzzleManager>();
        animator = GetComponent<Animator>();
        UpdateUI();

        isSwitchOn = Random.value > 0.5f;
        animator.SetBool("IsSwitchOn", isSwitchOn);
    }

    public void ToggleSwitch()
    {
        if (isActive)
        {
            isSwitchOn = !isSwitchOn;
            animator.SetBool("IsSwitchOn", isSwitchOn);
            puzzleManager.UpdateCurrentWattage(isSwitchOn ? wattageValue : -wattageValue);
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        switchValueText.text = "Target: " + wattageValue;
    }
}

