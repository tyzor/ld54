using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public enum PuzzleState{
        Hidden,
        Bad,
        Good
    }

    public PuzzleState state = PuzzleState.Bad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
