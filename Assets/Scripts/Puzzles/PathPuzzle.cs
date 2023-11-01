using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PathPuzzle : MonoBehaviour
{

    [SerializeField]
    private PathButton buttonPrefab;
    private Transform[] gridPoints;

    private List<PathButton> _buttons;


    private float tileWidth;
    private float tileHeight;
    private Vector3 boardCenterPoint;

    private Vector2Int startPos;

    private Vector2Int endPos;
    
    private int[] pathSolution;

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
        bool isValid = true;
        for(int i=0;i<pathSolution.Length-1;i++)
        {
            int ci = pathSolution[i];
            int ni = pathSolution[i+1];

            int edgeIndex = edgeFromIndexes(ci, ni);
            int reverseEdgeIndex = ((edgeIndex + 2)%4);
            
            PathButton currBtn = _buttons[ci];
            PathButton nextBtn = _buttons[ni];

            // Can we navigate over this edge
            if(!(currBtn.GetEdge(edgeIndex) && nextBtn.GetEdge(reverseEdgeIndex)) )
            {
                isValid = false;
                break;
            }

        }
        return isValid;

    }



    void OnTileClicked(Vector2Int gridPos)
    {
        PathButton btn = _buttons[IndexFromGridPos(gridPos)];
        if(btn.nodeType != PathButton.PathNodeType.Node)
            return;

        SFXController.PlaySound(SFX.BUTTON_CLICK);
        btn.Rotate();

        // Do check etc
        UpdatePuzzleStatus();
        
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
        Bounds bounds = buttonPrefab.GetComponent<Renderer>().bounds;
        
        tileWidth = Vector3.Distance(points[0].position, points[1].position);  //bounds.extents.z;
        tileHeight = Vector3.Distance(points[0].position, points[3].position); //bounds.extents.y;
        boardCenterPoint = gridPoints[4].position;

        // Spawn the prefabs
        _buttons = new List<PathButton>();
        for(int i=0; i<gridPoints.Length; i++)
        {
            PathButton button = Instantiate<PathButton>(buttonPrefab,gridPoints[i].position, gridPoints[i].rotation, transform);
            button.SetEdges(3);
            // Apply random rotation
            bool dir = UnityEngine.Random.Range(0f,1f) > 0.5f;
            button.Rotate(dir);
            _buttons.Add(button);
        }
        
        // Generate path
        GeneratePath();
        // Rotate buttons


    }

    private void GeneratePath()
    {
        // For now we fix the start and end positions and pick from a list
        // of pre-selected paths
        // With more time I wanted to pick random spots and search for random paths of a minimum length
        // (better strategy for a bigger grid)

        // Get start and end positions
        //startPos = new Vector2Int(0, UnityEngine.Random.Range(0,3));
        //endPos = new Vector2Int(2, UnityEngine.Random.Range(0,3));
        startPos = new Vector2Int(-1,0);
        endPos = new Vector2Int(1,0);

        PathButton startButton = _buttons[IndexFromGridPos(startPos)];
        startButton.SetType(PathButton.PathNodeType.Start);
        PathButton endButton = _buttons[IndexFromGridPos(endPos)];
        endButton.SetType(PathButton.PathNodeType.End);

        List<int[]> possiblePaths = new List<int[]>();
        possiblePaths.Add(new[] {3,0,1,2,5});
        possiblePaths.Add(new[] {3,6,7,8,5});
        possiblePaths.Add(new[] {3,0,1,4,7,8,5});
        possiblePaths.Add(new[] {3,6,7,4,1,2,5});
        possiblePaths.Add(new[] {3,4,1,2,5});
        possiblePaths.Add(new[] {3,4,7,8,5});

        int[] path = possiblePaths[UnityEngine.Random.Range(0,possiblePaths.Count)];

        // Zero out all nodes on the path
        for(int i=0;i<path.Length;i++)
        {
            PathButton btn = _buttons[path[i]];
            btn.SetEdges(0);
        }

        for(int i=0;i<path.Length-1;i++)
        {
            int currentIndex = path[i];
            int nextIndex = path[i+1];
            
            PathButton btn = _buttons[currentIndex];
            int edges = btn.GetEdges();
            int currentEdge = 0;

            // up
            currentEdge = edgeFromIndexes(currentIndex,nextIndex);
            btn.SetEdges(edges + ((int)Math.Pow(2, currentEdge)));

            // Set edges of next node
            PathButton nextBtn = _buttons[nextIndex];
            int nextEdges = nextBtn.GetEdges();
            nextEdges += (int)Math.Pow(2,((currentEdge+2)%4));
            nextBtn.SetEdges(nextEdges);
        }

        // Scramble the middle parts of the path
        for(int i=1;i<path.Length-1;i++)
        {
            PathButton btn = _buttons[path[i]];
            int r = UnityEngine.Random.Range(1,3);
            for(int c=0;c<r;c++)
                btn.Rotate();
        }

        pathSolution = path;
    
    }

    int edgeFromIndexes(int currentIndex, int nextIndex)
    {
        int currentEdge = 0;
        if(nextIndex - currentIndex == -3)
        {
            currentEdge = 0;
        // down
        } else if(nextIndex - currentIndex == 3)
        {
            currentEdge = 2;
        // left
        }else if(nextIndex - currentIndex == -1)
        {
            currentEdge = 3;
        // right
        } else if(nextIndex - currentIndex == 1)
        {
            currentEdge = 1;
        }
        return currentEdge;
    }

}

