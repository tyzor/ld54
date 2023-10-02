using UnityEngine;
using TMPro;

public class HackingMinigame : MonoBehaviour
{
    [SerializeField]
    private float cellWidth = 1f;

    [SerializeField]
    private float cellHeight = 1f;

    [SerializeField]
    private int columns = 5;

    [SerializeField]
    private int rows = 5;

    private Cell[,] grid;  // Declare the grid array

    public GameObject highlightObject;
    public Transform gridTransform; // Parent transform for the grid
    public TextMeshProUGUI gridText; // Reference to the TextMeshProUGUI component
public float timer;
public float timerDuration = 30f;  // Set a default value if desired

    void Start()
    {
        GenerateGrid();
        GenerateSequence();
        DisplaySequence();
        timer = timerDuration;
    }

    public void GenerateGrid()
    {
        // Initialize the grid array with the dimensions of your grid
        grid = new Cell[rows, columns];

        // Destroy any previous grid
        foreach (Transform child in gridTransform)
        {
            Destroy(child.gameObject);
        }

        // Generate new grid
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                // ... rest of your code ...

                // Create new cell GameObject
                GameObject cell = new GameObject($"Cell ({column},{row})");
                cell.transform.SetParent(gridTransform);
                cell.transform.localPosition = new Vector3(x, y, 0f);

                // Set text
                TextMeshProUGUI text = Instantiate(gridText, cell.transform);
                text.text = $"{GetLetter(column)}{GetNumber(row)}";

                // Store a reference to the Cell component in your grid array
                Cell cellComponent = cell.AddComponent<Cell>();
                cellComponent.row = row;
                cellComponent.column = column;
                grid[row, column] = cellComponent;
            }
        }
    }

    private string GetLetter(int index)
    {
        char letter = (char)('A' + index % 17); // Cycle through letters A-Q
        return letter.ToString();
    }

    private int GetNumber(int index)
    {
        return (index % 16) + 1; // Cycle through numbers 1-16
    }



    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        int column = Mathf.FloorToInt(worldPosition.x / cellWidth);
        int row = Mathf.FloorToInt(worldPosition.y / cellHeight);
        
        // Ensure column and row are within bounds
        column = Mathf.Clamp(column, 0, columns - 1);
        row = Mathf.Clamp(row, 0, rows - 1);
        
        HighlightCell(column, row);
    }

    void HighlightCell(int column, int row)
    {
        Vector3 highlightPosition = new Vector3(column * cellWidth, row * cellHeight, 0);
        highlightObject.transform.position = highlightPosition;
    }

    void GenerateSequence()
    {
        // ... generate a new sequence ...
    }

    void DisplaySequence()
    {
        // ... display the sequence in sequenceDisplay ...
    }

void HighlightRowOrColumn()
{
    for (int i = 0; i < rows; i++)  // Adjusted loop to match your grid dimensions
    {
        for (int j = 0; j < columns; j++)  // Adjusted loop to match your grid dimensions
        {
            grid[i, j].SetHighlight(isRowHighlighted ? i == highlightedIndex : j == highlightedIndex);
        }
    }
}


    public void OnCellSelected(Cell selectedCell)
    {
        isRowHighlighted = !isRowHighlighted;
        highlightedIndex = isRowHighlighted ? selectedCell.row : selectedCell.column;
        HighlightRowOrColumn();
    }
}
