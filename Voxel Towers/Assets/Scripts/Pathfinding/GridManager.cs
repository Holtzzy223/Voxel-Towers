using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector2Int gridSize;
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
