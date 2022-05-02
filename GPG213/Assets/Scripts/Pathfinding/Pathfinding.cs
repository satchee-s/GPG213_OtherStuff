using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Grid grid;
    [SerializeField] Transform enemy;
    [SerializeField] Transform target;

    public List<Node> neighbourNodes = new List<Node>();
    List<Node> open = new List<Node>();
    public List<Node> final = new List<Node>();

    Vector3Int startingNodePos;
    Vector3Int targetNodePos;

    public Node startingNode;
    public Node targetNode;   
    Node currentNode;

    private void Start()
    {
        grid = FindObjectOfType<Grid>();
        startingNodePos = grid.NodeFromWorldPosition(enemy.position);
        startingNode = grid.GetNode(startingNodePos);
        startingNode.gridPosition = startingNodePos;

        targetNodePos = grid.NodeFromWorldPosition(target.position);
        targetNode = grid.GetNode(targetNodePos);
        targetNode.gridPosition = targetNodePos;
        
        open.Add(startingNode);
        FindPath();
    }

    void FindPath()
    {
        bool containsInOpen;
        while (true)
        {
            open.Sort();
            currentNode = open[0];
            GetNeighbours(currentNode.gridPosition);
            open.Remove(currentNode);
            currentNode.isVisited = true;
            if (currentNode == targetNode) 
            {
                FinalPath(targetNode);
                break;
            }
            for (int i = 0; i < neighbourNodes.Count; i++)
            {
                containsInOpen = open.Contains(neighbourNodes[i]);
                if (!neighbourNodes[i].Walkable || neighbourNodes[i].isVisited == true)
                    continue;
                neighbourNodes[i].gCost = CalculateCost(neighbourNodes[i].gridPosition, startingNodePos);
                neighbourNodes[i].hCost = CalculateCost(neighbourNodes[i].gridPosition, targetNodePos);
                if (neighbourNodes[i].hCost < currentNode.hCost || !containsInOpen)
                {
                    neighbourNodes[i].parent = currentNode;
                    if (!containsInOpen)
                        open.Add(neighbourNodes[i]);
                }
            }
            neighbourNodes.Clear();
        }
    }
    void GetNeighbours(Vector3Int nodePos)
    {
        if (nodePos.x - 1 >= 0) //left neighbour
        {
            Vector3Int leftNeighbour = new Vector3Int(nodePos.x - 1, nodePos.y, nodePos.z);
            neighbourNodes.Add(grid.GetNode(leftNeighbour));
            neighbourNodes[neighbourNodes.Count - 1].gridPosition = leftNeighbour;
        }
        if (nodePos.x + 1 < grid.totalTilesX)
        {
            Vector3Int rightNeighbour = new Vector3Int(nodePos.x + 1, nodePos.y, nodePos.z);
            neighbourNodes.Add(grid.GetNode(rightNeighbour));
            neighbourNodes[neighbourNodes.Count - 1].gridPosition = rightNeighbour;
        }
        if (nodePos.z + 1 < grid.totalTilesZ)
        {
            Vector3Int upNeighbour = new Vector3Int(nodePos.x, nodePos.y, nodePos.z + 1);
            neighbourNodes.Add(grid.GetNode(upNeighbour));
            neighbourNodes[neighbourNodes.Count - 1].gridPosition = upNeighbour;
        }
        if (nodePos.z - 1 >= 0)
        {
            Vector3Int downNeighbour = new Vector3Int(nodePos.x, nodePos.y, nodePos.z - 1);
            neighbourNodes.Add(grid.GetNode(downNeighbour));
            neighbourNodes[neighbourNodes.Count - 1].gridPosition = downNeighbour;
        }
        if (nodePos.x - 1 >= 0 && nodePos.z + 1 < grid.totalTilesZ)
        {
            Vector3Int upLeft = new Vector3Int(nodePos.x - 1, nodePos.y, nodePos.z + 1);
            neighbourNodes.Add(grid.GetNode(upLeft));
            neighbourNodes[neighbourNodes.Count - 1].gridPosition = upLeft;
        }
        if (nodePos.x + 1 < grid.totalTilesX && nodePos.z + 1 < grid.totalTilesZ)
        {
            Vector3Int upRight = new Vector3Int(nodePos.x + 1, nodePos.y, nodePos.z + 1);
            neighbourNodes.Add(grid.GetNode(upRight));
            neighbourNodes[neighbourNodes.Count - 1].gridPosition = upRight;
        }
        if (nodePos.x - 1 >= 0 && nodePos.z - 1 >= 0)
        {
            Vector3Int downLeft = new Vector3Int(nodePos.x - 1, nodePos.y, nodePos.z - 1);
            neighbourNodes.Add(grid.GetNode(downLeft));
            neighbourNodes[neighbourNodes.Count - 1].gridPosition = downLeft;
        }
        if (nodePos.x + 1 < grid.totalTilesX && nodePos.z - 1 >= 0)
        {
            Vector3Int downRight = new Vector3Int(nodePos.x + 1, nodePos.y, nodePos.z - 1);
            neighbourNodes.Add(grid.GetNode(downRight));
            neighbourNodes[neighbourNodes.Count - 1].gridPosition = downRight;
        }
    }

    int CalculateCost(Vector3Int nodeA, Vector3Int nodeB)
    {
        return Mathf.Abs(nodeA.x - nodeB.x) + Mathf.Abs(nodeA.z - nodeB.z);
    }

    void FinalPath(Node node)
    {
        final.Add(node);
        while (true)
        {
            final.Add(node.parent);
            node = final[final.Count - 1];
            if (final[final.Count - 1] == startingNode)
            {
                enemy.gameObject.GetComponent<AIMovement>().OnPathFound(final);
                break;
            }
        }
    }
}