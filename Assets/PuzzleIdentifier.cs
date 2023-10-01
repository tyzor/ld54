using UnityEngine;

public class PuzzleIdentifier : MonoBehaviour
{
    public string puzzleName;  // Unique name for each puzzle
    public PuzzleType puzzleType;  // Enum to differentiate between different types of puzzles

    public enum PuzzleType
    {
        Wattage,
        Slider,
        // ... add other puzzle types as needed
    }
}
