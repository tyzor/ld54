using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Cell : MonoBehaviour
{
    public int row;
    public int column;
    public string value;
    public TextMeshProUGUI text;
    public HackingMinigame hackingMinigame;
    public Image highlightImage;  // Image component to highlight the cell

    void Start()
    {
        text.text = value;
    }

    public void SetHighlight(bool isActive)
    {
        highlightImage.enabled = isActive;
    }

    void OnMouseDown()
    {
        hackingMinigame.OnCellSelected(this);
    }
}
