using System;
using UnityEngine;

public class Node : IComparable
{
    public int gCost;
    public int hCost;
    public int fCost { get { return gCost + hCost; } }

    public Vector3Int gridPosition;

    public Node parent;
    public bool isVisited;
    public Vector3 Position { get; }
    public bool Walkable { get; }
    public Node (Vector3 position, bool walkable)
    {
        Position = position;
        Walkable = walkable;
    }

    public int CompareTo(object obj)
    {
        Node node = obj as Node;
        if(node.fCost > fCost)
            return -1;
        else if (node.fCost < fCost)
            return 1;
        return 0;
    }
}
