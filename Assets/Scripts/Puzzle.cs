using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public enum PuzzleState{
        Hidden,
        Bad,
        Good
    }

    public enum PuzzleType {
        Test,
        Wattage,
        Slider
    }

    public PuzzleType type = PuzzleType.Test;

    public PuzzleState state = PuzzleState.Bad;

    [SerializeField]
    private Transform coverPanel;

    public Transform CameraLockPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePuzzle()
    {
        Debug.Log($"Activating puzzle {gameObject.name}");

        var rb = coverPanel.GetComponent<Rigidbody>();
        rb.isKinematic = false;       

        this.state = PuzzleState.Bad;

    }
}
