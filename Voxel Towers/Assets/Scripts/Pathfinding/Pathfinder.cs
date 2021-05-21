using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoords;
    [SerializeField] Vector2Int destinationCoords;
    Node startNode;
    Node destinationNode;
    Node currentSearchNode;

    Queue<Node> map = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary < Vector2Int, Node>();
    
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        startNode = gridManager.Grid[startCoords];
        destinationNode = gridManager.Grid[destinationCoords];
        GetNewPath();
    }

    public List<Node> GetNewPath() 
    {
        gridManager.ResetNodes();
        BreadthFirstSearch();
        return BuildPath();
    }
    private void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoords = currentSearchNode.coords + direction;
            if (grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
            }    
        }

        foreach (Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coords) && neighbor.isTraversable)
            {
                neighbor.connectedNode = currentSearchNode;
                reached.Add(neighbor.coords, neighbor);
                map.Enqueue(neighbor);
            }
        }
    }
    
    void BreadthFirstSearch()
    {
        map.Clear();
        reached.Clear();
        bool isRunning = true;

        map.Enqueue(startNode);
        reached.Add(startCoords,startNode);
        while (map.Count > 0 && isRunning)
        {
            currentSearchNode = map.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if (currentSearchNode.coords == destinationCoords)
            {
                isRunning = false;
            }
        }
    }
    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedNode != null)
        {
            currentNode = currentNode.connectedNode;
            path.Add(currentNode);
            currentNode.isPath = true;
        }
        path.Reverse();
        
        return path;

    }

    public bool WillBlockPath(Vector2Int coords)
    {
        if (grid.ContainsKey(coords))
        {
            bool prevState = grid[coords].isTraversable;
            List<Node> newPath = GetNewPath();
            grid[coords].isTraversable = prevState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }
        return false;
    }
}
