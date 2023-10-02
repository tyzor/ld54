using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlidePuzzle : MonoBehaviour
{

    [SerializeField]
    private GameObject tilePrefab;
    private Transform[] gridPoints;

    [System.Serializable]
    struct TileInfo {
        public Transform tileObject;
        public int number;
    }
    TileInfo[] tileInfoArray;
    
    private float tileWidth;
    private float tileHeight;
    private Vector3 boardCenterPoint;
    private Vector2Int emptyGridPos;

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
    
            Vector3 boardWorldPt = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, Vector3.Distance(Camera.main.transform.position,boardCenterPoint)) );
            Vector3 localPt = boardWorldPt - boardCenterPoint;
            localPt = Quaternion.Inverse(_puzzle.transform.rotation) * localPt;

            float gridX = localPt.z / tileWidth;
            float gridY = localPt.y / tileHeight;
            
            Vector2Int gridPos = new Vector2Int(Mathf.RoundToInt(gridX),Mathf.RoundToInt(gridY));
            //Debug.Log($"Empty {emptyGridPos} Clicked {gridPos}");
            OnTileClicked(gridPos);

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
        bool result = true;
        int counter = 0;
        for(int i=0; i<9;i++)
        {
            if(tileInfoArray[i].number > 8)
                continue;

            if(tileInfoArray[i].number != counter + 1)
            {
                result = false;
                break;
            }
            counter++;
        }    
        //Debug.Log($"Puzzle done is {result}");
        return result;
    }

    void OnTileClicked(Vector2Int gridPos)
    {
        if(isAdjacentToEmpty(gridPos))
        {
            SwapTile(gridPos);
            UpdatePuzzleStatus();
        }
    }

    bool isAdjacentToEmpty(Vector2Int gridPos)
    {
        return Vector2Int.Distance(gridPos,emptyGridPos) == 1f;
    }

    // Always swap with the empty space
    void SwapTile(Vector2Int gridPos)
    {
        //Debug.Log($"SwapTile on {gridPos} {emptyGridPos}");
        int index1 = IndexFromGridPos(gridPos);
        int index2 = IndexFromGridPos(emptyGridPos);
        //Debug.Log($"Indexes {index1} {index2}");
        TileInfo tileInfo1 = tileInfoArray[index1];
        TileInfo tileInfo2 = tileInfoArray[index2];

        tileInfoArray[index1] = tileInfo2;
        tileInfoArray[index2] = tileInfo1;

        // Update transform
        tileInfo1.tileObject.position = gridPoints[index2].position;

        emptyGridPos = gridPos;

    }

    Vector2Int GetGridPositionFromIndex(int i)
    {
        int x = i % 3;
        int y = (int)(i/3);
        // Center the grid with 0,0 at middle (need to invert y)
        return new Vector2Int(x-1,-(y-1));
    }

    int IndexFromGridPos(Vector2Int gridPos)
    {
        // Grid is 0,0 in center so we need to convert to a space that makes sense
        Vector2Int gridCoord = new Vector2Int(gridPos.x+1,-(gridPos.y-1));
        int i = gridCoord.y * 3 + gridCoord.x;
        return i;
    }


    void InitializePuzzle()
    {
        // TODO -- maybe not the best way to do this?
        // Get grid points
        List<Transform> points = new List<Transform>();
        var container = transform.Find("PuzzleContainer");
        foreach(Transform spot in container.transform)
        {
            points.Add(spot);
        }
        gridPoints = points.ToArray();

        // Calculate grid info
        Bounds bounds = tilePrefab.GetComponent<Renderer>().bounds;
        
        tileWidth = Vector3.Distance(points[0].position, points[1].position);  //bounds.extents.z;
        tileHeight = Vector3.Distance(points[0].position, points[3].position); //bounds.extents.y;
        boardCenterPoint = gridPoints[4].position;

        // Shuffle an array of numbers
        List<int> numbers = new List<int>();
        for(int i=1; i<=9; i++)
            numbers.Add(i);
        numbers.Shuffle();    

        // Spawn the prefabs
        List<TileInfo> gridInfo = new List<TileInfo>();
        for(int i=1; i<=9; i++)
        {
            // Skip the empty space
            if(numbers[i-1] == 9)
            {
                emptyGridPos = GetGridPositionFromIndex(i-1);
                TileInfo tInf = new TileInfo();
                tInf.tileObject = null;
                tInf.number = 9;
                gridInfo.Add(tInf);
                continue;
            }
            var tile = Instantiate(tilePrefab,gridPoints[i-1].position, gridPoints[i-1].rotation, transform);
            tile.GetComponentInChildren<TextMeshProUGUI>().SetText(numbers[i-1].ToString());
            TileInfo tileInfo = new TileInfo();
            tileInfo.number = numbers[i-1];
            tileInfo.tileObject = tile.transform;
            gridInfo.Add(tileInfo);
        }
        tileInfoArray = gridInfo.ToArray();


    }

}
