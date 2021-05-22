using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int coords;
    public bool isTraversable;
    public bool isExplored;
    public bool isPath;
    public Node connectedNode;

    public Node(Vector2Int coords, bool isTraversable) 
    {
        this.coords = coords;
        this.isTraversable = isTraversable;
    }
}
