using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector2Int gridSize;
    public int sceneGridSize;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }
    public Tile tile;
    private void Awake()
    {
       StartCoroutine(CreateGrid(0,0,false));
    }
    public Node GetNode(Vector2Int coords)
    {
        if (grid.ContainsKey(coords))
        {
            return grid[coords];
        }
        return null;
    }

    public void BlockNode(Vector2Int coords)
    {
        if (grid.ContainsKey(coords))
        {
            grid[coords].isTraversable = false;
        }
    }
    public void ClearNode(Vector2Int coords)
    {
        if (grid.ContainsKey(coords))
        {
            grid[coords].isTraversable = true;
        }
    }

    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedNode = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public Vector2Int GetCoordsFromPos(Vector3 pos)
    {
        Vector2Int coords = new Vector2Int();
        coords.x = Mathf.RoundToInt(pos.x / sceneGridSize);
        coords.y = Mathf.RoundToInt(pos.z / sceneGridSize);
        return coords;
    }

    public Vector3 GetPosFromCoords(Vector2Int coords)
    {
        Vector3 pos = new Vector3();
        pos.x = coords.x * sceneGridSize;
        pos.y = pos.y + 1;
        pos.z = coords.y * sceneGridSize;
        return pos;
    }

    public IEnumerator CreateGrid(int xStart, int yStart, bool runTimer)
    {
        if (grid != null)
        {
            for (int x = xStart; x < gridSize.x; x++)
            {
                for (int y = yStart; y < gridSize.y; y++)
                {
                    Vector2Int coords = new Vector2Int(x, y);
                    
                    grid.Add(coords, new Node(coords, true));
                    Debug.Log(grid[coords].coords + "=" + grid[coords].isTraversable);
                    Instantiate(tile, GetPosFromCoords(grid[coords].coords), Quaternion.identity);
                    yield return new WaitForSeconds(0.00f);

                }
            }
            Debug.LogWarning("Built Grid");
            FindObjectOfType<Pathfinder>().InitPath();
            FindObjectOfType<Pathfinder>().GetNewPath();
            
            if (runTimer)
            {
                FindObjectOfType<Timer>().timerIsRunning = true;
            }
        }
    }
}

