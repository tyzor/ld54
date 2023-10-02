using UnityEngine;

public class TileSliderManager : MonoBehaviour
{
    [System.Serializable]
    public struct TileInfo
    {
        public GameObject tileObject;
        public int number;
    }

    public TileInfo[] tileInfoArray;
    public int gridSize = 3;
    private Vector2Int emptyTilePosition;

    void Start()
    {
        // Initialize the puzzle
        InitializePuzzle();
    }

    public void PuzzleActivation()
    {
        // This method can be called by other scripts to activate the puzzle
        // ... Add any activation logic here ...
    }

    void InitializePuzzle()
    {
        // Assuming tileInfoArray is already populated in the Inspector
        // Randomize the tileInfoArray
        // ... Add randomization logic here ...

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                int index = y * gridSize + x;
                TileInfo tileInfo = tileInfoArray[index];
                tileInfo.tileObject.transform.localPosition = new Vector3(x, y, 0);

                // If it's block_9, hide it and store its position as the empty tile position
                if (tileInfo.number == 9)
                {
                    tileInfo.tileObject.SetActive(false);
                    emptyTilePosition = new Vector2Int(x, y);
                }
            }
        }
    }

    public void OnTileClicked(GameObject clickedTile)
    {
        Vector2Int clickedTilePosition = GetTilePosition(clickedTile);
        if (IsAdjacentToEmpty(clickedTilePosition))
        {
            // Swap the clicked tile with the empty space
            SwapTiles(clickedTilePosition, emptyTilePosition);
        }
    }

    bool IsAdjacentToEmpty(Vector2Int position)
    {
        return (position - emptyTilePosition).sqrMagnitude == 1;
    }

    void SwapTiles(Vector2Int position1, Vector2Int position2)
    {
        int index1 = position1.y * gridSize + position1.x;
        int index2 = position2.y * gridSize + position2.x;
        TileInfo tileInfo1 = tileInfoArray[index1];
        TileInfo tileInfo2 = tileInfoArray[index2];

        tileInfoArray[index1] = tileInfo2;
        tileInfoArray[index2] = tileInfo1;

        Vector3 tempPosition = tileInfo1.tileObject.transform.localPosition;
        tileInfo1.tileObject.transform.localPosition = tileInfo2.tileObject.transform.localPosition;
        tileInfo2.tileObject.transform.localPosition = tempPosition;

        emptyTilePosition = position2;
    }

    Vector2Int GetTilePosition(GameObject tile)
    {
        for (int i = 0; i < tileInfoArray.Length; i++)
        {
            if (tileInfoArray[i].tileObject == tile)
            {
                int x = i % gridSize;
                int y = i / gridSize;
                return new Vector2Int(x, y);
            }
        }

        return new Vector2Int(-1, -1);  // Return an invalid position if the tile is not found
    }
}
