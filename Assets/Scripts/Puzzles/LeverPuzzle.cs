using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{

    [SerializeField]
    private Lever leverPrefab;
    private Transform[] leverPoints;

    private List<Lever> _levers;

    private int targetVolts;
    private void SetTargetVolts(int volts)
    {
        targetVolts = volts;
        targetVoltLabel.SetText(volts.ToString() + " V");
    }
    private int currentVolts;
    private void SetCurrentVolts(int volts)
    {
        currentVolts = volts;
        currentVoltLabel.SetText(volts.ToString() + " V");
    }

    [SerializeField]
    TextMeshProUGUI targetVoltLabel;
    [SerializeField]
    TextMeshProUGUI currentVoltLabel;

    private Puzzle _puzzle;

    // Start is called before the first frame update
    void Start()
    {
        _puzzle = GetComponent<Puzzle>();
        // Initialize puzzle grid
        InitializePuzzle();
        UpdatePuzzleStatus();
    }

    // Update is called once per frame
    void Update()
    {
        if(_puzzle.IsEngaged == false)
            return;

        if(Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if we clicked a lever
            // Toggle lever status
            RaycastHit[] hits = Physics.RaycastAll(ray,100f);
            foreach(var obj in hits)
            {
                Lever lever;
                if(obj.transform.TryGetComponent<Lever>(out lever))
                {
                    lever.ToggleLever();
                    SFXController.PlaySound(SFX.LEVER);
                    if(lever.isOn)
                    {
                        this.SetCurrentVolts(currentVolts + lever.volts);
                    } else {
                        this.SetCurrentVolts(currentVolts - lever.volts);
                    }
                    UpdatePuzzleStatus();
                }
            }


            /*
            Vector3 boardWorldPt = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, Vector3.Distance(Camera.main.transform.position,boardCenterPoint)) );
            Vector3 localPt = boardWorldPt - boardCenterPoint;
            localPt = Quaternion.Inverse(_puzzle.transform.rotation) * localPt;

            float gridX = localPt.z / tileWidth;
            float gridY = localPt.y / tileHeight;
            
            Vector2Int gridPos = new Vector2Int(Mathf.RoundToInt(gridX),Mathf.RoundToInt(gridY));
            //Debug.Log($"Empty {emptyGridPos} Clicked {gridPos}");
            OnTileClicked(gridPos);
            */
        }
    }

    void UpdatePuzzleStatus()
    {
        if(CheckPuzzleDone())
        {
            this.enabled = false;
            _puzzle.SetPuzzleState(Puzzle.PuzzleState.Good);
        }
    }

    bool CheckPuzzleDone()
    {
        return this.currentVolts == this.targetVolts;
    }

    void InitializePuzzle()
    {
        // TODO -- maybe not the best way to do this?
        // Get grid points
        List<Transform> points = new List<Transform>();
        var container = transform.Find("LeverSlots");
        foreach(Transform spot in container.transform)
        {
            points.Add(spot);
        }
        leverPoints = points.ToArray();

        
        // Spawn prefabs
        _levers = new List<Lever>();
        for(int i=0;i<leverPoints.Length;i++)
        {
            Lever lever = Instantiate<Lever>(leverPrefab, leverPoints[i].position, leverPoints[i].rotation, transform);
            _levers.Add(lever);
        }

        this.SetTargetVolts(220);
        this.SetCurrentVolts(0);

        // Calculate voltages
        List<int> ints = new List<int>();
        // First get a series of ints that add to targetVolts
        int counter = targetVolts;
        while(counter > 0)
        {
            int r = UnityEngine.Random.Range(1,10) * 5;
            if(counter - r < 0)
                r = counter;
            ints.Add(r);
            counter -= r;
        }
        int correctSwitches = UnityEngine.Random.Range(3,leverPoints.Length + 1);
        
        // Reduce the list until we have the number we want
        while(ints.Count > correctSwitches)
        {
            int n = ints[0];
            ints.RemoveAt(0);
            ints[UnityEngine.Random.Range(0,ints.Count)] += n;
        }

        // Fill the rest of the numbers with randomness
        while(ints.Count < _levers.Count)
        {
            ints.Add(UnityEngine.Random.Range(1,10) * 5);
        }

        // Shuffle the list
        ints.Shuffle();

        // Assign volts to levers
        for(int i=0;i<_levers.Count;i++)
        {
            Lever lever = _levers[i];
            lever.InitLever(ints[i]);
        }

    }

}
