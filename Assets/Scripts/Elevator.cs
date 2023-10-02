using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ELEVATOR_CONDITION {
    Good = 0,
    
    Errors,
    Failed
}

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private DoorManager DoorManager;

    public int floorNumber = 35;
    private float subFloorTracker;
    [SerializeField]
    private float elevatorMoveSpeed = 1f; // Number of floors per second

    [SerializeField]
    private TextMeshProUGUI floorIndicatorText;

    // LIGHTS
    [SerializeField]
    private Light mainLight;
    [SerializeField]
    private List<GameObject> ceilingLights;

    // PUZZLES
    [SerializeField]
    private PuzzleManager puzzleManager;

    private float doorMalfunctionFloorCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        floorNumber = 35;
        subFloorTracker = 35.0f;
        doorMalfunctionFloorCounter = 0;
        floorIndicatorText.text = floorNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        DoorMalfunctionCheck();
    }

    // Check status of elevator to see if it can move
    public ELEVATOR_CONDITION CheckCondition()
    {

        List<Puzzle> puzzles = puzzleManager.GetActivePuzzles();
        ELEVATOR_CONDITION condition = ELEVATOR_CONDITION.Good;
        foreach(var puzzle in puzzles)
        {
            if(puzzle.state == Puzzle.PuzzleState.Hidden)
                continue;

            if(puzzle.state == Puzzle.PuzzleState.Fail)
            {
                return ELEVATOR_CONDITION.Failed;
            }
            if(puzzle.state == Puzzle.PuzzleState.Bad)
            {
                condition = ELEVATOR_CONDITION.Errors;
            }
        }

        // check other stuff like doors etc
        if(condition == ELEVATOR_CONDITION.Good && DoorManager.GetDoorStatus())
            condition = ELEVATOR_CONDITION.Errors;

        return condition;
    }

    // Called when the elevator can continue moving
    public void DoMove()
    {
        int lastFloor = floorNumber;
        subFloorTracker -= elevatorMoveSpeed * Time.deltaTime;
        floorNumber = Mathf.CeilToInt(subFloorTracker);
        if(lastFloor != floorNumber)
        {
            SFXController.PlaySound(SFX.FLOOR_DING);
            doorMalfunctionFloorCounter++;
        }
        floorIndicatorText.text = floorNumber.ToString();
    }

    void DoorMalfunctionCheck()
    {
        if(DoorManager.GetDoorStatus() == true)
            return;

        // Open door
        if(doorMalfunctionFloorCounter > 3)
        {
            DoorManager.OpenDoor();
        }

    }


}
