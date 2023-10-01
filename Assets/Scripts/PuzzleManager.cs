using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField]
    private List<Puzzle> PuzzlePrefabs; // puzzle prefabs
    [SerializeField]
    private GameObject CoverPanel;

    [SerializeField]
    private GameObject PuzzlePointsPrefab; // areas where puzzles can be placed
    private List<Transform> m_PuzzlePoints;

    private List<Puzzle> m_ActivePuzzles;
    private List<Puzzle> m_CurrentPuzzles;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn puzzles
        m_PuzzlePoints = new List<Transform>();
        foreach(Transform child in PuzzlePointsPrefab.transform)
        {
            m_PuzzlePoints.Add(child);
        }


        SpawnPanels();        
        SpawnPuzzles();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Create the puzzle covers
    void SpawnPanels() {
        foreach(var pt in m_PuzzlePoints)
        {
            var panel = Instantiate(CoverPanel);
            panel.transform.position = pt.position;
            panel.transform.rotation = pt.rotation;
            // Rotate to match

        }
    }

    void SpawnPuzzles() {
        foreach(var pt in m_PuzzlePoints)
        {
            var panel = Instantiate(PuzzlePrefabs[0]);
            panel.transform.position = pt.position;
            panel.transform.rotation = pt.rotation;
            // Rotate to match

        }
    }
}
