using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField]
    private List<Puzzle> PuzzlePrefabs; // puzzle prefabs

    [SerializeField]
    private GameObject PuzzlePointsPrefab; // areas where puzzles can be placed
    private List<Transform> m_PuzzlePoints;

    private List<Puzzle> m_ActivePuzzles;
    public List<Puzzle> GetActivePuzzles() { return m_ActivePuzzles; }
        
    private List<Puzzle> m_CurrentPuzzles;


    [SerializeField]
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {

        m_ActivePuzzles = new List<Puzzle>();
        m_CurrentPuzzles = new List<Puzzle>();

        // Spawn puzzles
        m_PuzzlePoints = new List<Transform>();
        foreach(Transform child in PuzzlePointsPrefab.transform)
        {
            m_PuzzlePoints.Add(child);
        }
   
        SpawnPuzzles();

        // TODO -- remove debugging
        StartCoroutine(StartTestPuzzle());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPuzzles() {
        foreach(var pt in m_PuzzlePoints)
        {
            int rnd = UnityEngine.Random.Range(0,PuzzlePrefabs.Count);
            var puzzle = Instantiate(PuzzlePrefabs[rnd]);
            puzzle.transform.position = pt.position;
            puzzle.transform.rotation = pt.rotation;

            m_CurrentPuzzles.Add(puzzle);
        }
    }

    IEnumerator StartTestPuzzle()
    {
        yield return new WaitForSeconds(1);
        
        foreach(var puzzle in m_CurrentPuzzles)
        {
            if(puzzle.state == Puzzle.PuzzleState.Hidden)
            {
                puzzle.ActivatePuzzle();
                m_ActivePuzzles.Add(puzzle);
                break;
            }
        }

        StartCoroutine(StartTestPuzzle());
    }

}
