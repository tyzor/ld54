using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public enum PuzzleState{
        Hidden,
        Bad,
        Good,
        Fail
    }

    public enum PuzzleType {
        Test,
        Wattage,
        Slider
    }

    public PuzzleType type = PuzzleType.Test;

    public PuzzleState state = PuzzleState.Bad;

    [SerializeField]
    private GameObject coverPanelPrefab;
    [SerializeField]
    private Transform coverPanel;

    public Transform CameraLockPoint;

    public SelectionHighlight highlight;

    [SerializeField]
    private GameObject greenStatusLight;
    [SerializeField]
    private GameObject redStatusLight;

    // Is the player interacting with this puzzle?
    public bool IsEngaged = false;

    // Start is called before the first frame update
    void Start()
    {
        var panel = Instantiate(coverPanelPrefab);
        panel.transform.position = transform.position;
        panel.transform.rotation = transform.rotation;
        coverPanel = panel.transform;

        highlight = GetComponentInChildren<SelectionHighlight>();
        highlight.gameObject.SetActive(false);

        SetPuzzleState(PuzzleState.Hidden);

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
        rb.AddForce(coverPanel.forward*1f);

        SetPuzzleState(PuzzleState.Bad);
        

    }

    public void SetPuzzleState(PuzzleState newState)
    {
        PuzzleState oldState = this.state;
        this.state = newState;

        if(newState == PuzzleState.Hidden)
        {
            redStatusLight.SetActive(false);
            greenStatusLight.SetActive(false);
        }

        if(newState == PuzzleState.Bad)
        {
            redStatusLight.SetActive(true);
            greenStatusLight.SetActive(false);
        }

        if(newState == PuzzleState.Good)
        {
            redStatusLight.SetActive(false);
            greenStatusLight.SetActive(true);
        }

    }
}
