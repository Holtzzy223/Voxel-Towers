using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector2Int gridSize;
    public int sceneGridSize = 10;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get{ return grid; } }
    private void Awake()
    {
        CreateGrid();
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
    public Vector2Int GetCoordsFromPos(Vector3 pos)
    {
        Vector2Int coords = new Vector2Int();
        coords.x = Mathf.RoundToInt(pos.x / sceneGridSize);
        coords.x = Mathf.RoundToInt(pos.z / sceneGridSize);
        return coords;
    }
    
    public Vector3 GetPosFromCoords(Vector2Int coords)
    {
        Vector3 pos = new Vector3();
        pos.x = coords.x * sceneGridSize;
        pos.z = coords.y * sceneGridSize;
        return pos;
    }

    private void CreateGrid()
    {
        for (int x = 0; x < gridSize.x;x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coords = new Vector2Int(x,y);
                grid.Add(coords, new Node(coords, true));
                Debug.Log(grid[coords].coords + "=" + grid[coords].isTraversable);
            }
        }
    }
}
