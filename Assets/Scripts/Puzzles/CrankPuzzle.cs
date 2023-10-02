using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


// A puzzle that requires the player to constantly keep the indicator above a certain value
public class CrankPuzzles: MonoBehaviour
{

    [SerializeField]
    private GameObject indicator;
    [SerializeField]
    private GameObject crank;

    float fillValue = .5f;
    [SerializeField]
    float fillSpeed = .1f;
    [SerializeField]
    float emptySpeed = .05f;

    [SerializeField]
    float crankSpeed = (float)Math.PI / 10f;

    private Puzzle _puzzle;

    private Animator _crankAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _puzzle = GetComponent<Puzzle>();
        _crankAnimator = crank.GetComponent<Animator>();
        // Initialize puzzle grid
        InitializePuzzle();
    }

    // Update is called once per frame
    void Update()
    {
        if(_puzzle.state == Puzzle.PuzzleState.Hidden)
            return;

        fillValue -= emptySpeed * Time.deltaTime;

        if(_puzzle.IsEngaged && Input.GetMouseButton(0))
        {
            // Fill meter at speed
            crank.transform.Rotate(Vector3.forward, crankSpeed * Time.deltaTime, Space.Self);
            SFXController.PlaySound(SFX.CRANK);
            fillValue += fillSpeed * Time.deltaTime;
        }  else {
            crank.transform.Rotate(Vector3.forward, -crankSpeed/2f * Time.deltaTime, Space.Self);
        }
        fillValue = Mathf.Clamp(fillValue, 0, 1f);
        UpdatePuzzleStatus();        
    }

    void UpdatePuzzleStatus()
    {
        if(fillValue <= 0)
        {
            // FAILED
            this._puzzle.SetPuzzleState(Puzzle.PuzzleState.Fail);
            return;
        }

        if(fillValue < .1f)
        {
            this._puzzle.SetPuzzleState(Puzzle.PuzzleState.Bad);
        } else {
            this._puzzle.SetPuzzleState(Puzzle.PuzzleState.Good);
        }
        indicator.transform.localScale = new Vector3(1f,fillValue,1f);    
    }

    void InitializePuzzle()
    {
        fillValue = .5f;
    }

}
