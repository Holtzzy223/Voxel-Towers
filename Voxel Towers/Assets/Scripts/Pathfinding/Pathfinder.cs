using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public Vector2Int startCoords;
    public Vector2Int StartCoords { get { return startCoords; } }
   

    public Vector2Int destinationCoords;
    public Vector2Int DestinationCoords { get { return destinationCoords; } }
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

        return GetNewPath(startCoords);
    }

    public List<Node> GetNewPath(Vector2Int coords)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coords);
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
    
    void BreadthFirstSearch(Vector2Int coords)
    {
        startNode.isTraversable = true;
        destinationNode.isTraversable = true;
        map.Clear();
        reached.Clear();
        bool isRunning = true;

        map.Enqueue(grid[coords]);
        reached.Add(coords,grid[coords]);
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

        while (currentNode != null)
        {
            currentNode = currentNode.connectedNode;
            if (currentNode != null)
            {
                currentNode.isPath = true;
                path.Add(currentNode);
                
            }
        }
        path.Reverse();
        
        return path;

    }

    public bool WillBlockPath(Vector2Int coords)
    {
        if (grid.ContainsKey(coords))
        {
            bool prevState = grid[coords].isTraversable;
            grid[coords].isTraversable = false;
            List<Node> newPath = GetNewPath();
            grid[coords].isTraversable = prevState;
            if (coords == startCoords || coords == destinationCoords)
            {
                return true;
            }
            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }
        return false;
    }
    public bool IsInPath(Vector2Int coords)
    {
            bool isInPath = grid[coords].isPath;
            return isInPath;
    
    }
    public void NotifyRecievers() 
    {
        BroadcastMessage("FindPath",false,SendMessageOptions.DontRequireReceiver);
    }
    public void InitPath()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = grid[startCoords];
            destinationNode = grid[destinationCoords];
            startNode.isTraversable = true;
            destinationNode.isTraversable = true;
        }
    }
}
