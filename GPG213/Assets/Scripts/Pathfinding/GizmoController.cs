using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoController : MonoBehaviour
{
    Pathfinding pathfinding;
    Grid grid;
    [SerializeField] float sphereRadius;

    private void Start()
    {
        pathfinding = FindObjectOfType<Pathfinding>();
        grid = FindObjectOfType<Grid>();
    }

    private void OnDrawGizmos()
    {
        for (int z = 0; z <= grid.totalTilesZ; z++)
        {
            Gizmos.DrawLine(transform.position + new Vector3(0, 0, z * grid.tileHeight),
                            transform.position + new Vector3(grid.tileWidth * grid.totalTilesX, 0, z * grid.tileHeight));
        }

        for (int x = 0; x <= grid.totalTilesX; x++)
        {
            Gizmos.DrawLine(transform.position + new Vector3(x * grid.tileWidth, 0, 0),
                            transform.position + new Vector3(x * grid.tileWidth, 0, grid.tileHeight * grid.totalTilesZ));
        }

        Gizmos.color = Color.magenta;
        for (int i = 0; i < grid.nodes.Length; i++)
        {
            Gizmos.DrawSphere(grid.nodes[i].Position, sphereRadius);
        }

        Gizmos.color = Color.white;
        for (int i = 0; i < pathfinding.final.Count; i++)
        {
            Gizmos.DrawSphere(pathfinding.final[i].Position, sphereRadius);
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(pathfinding.startingNode.Position, sphereRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(pathfinding.targetNode.Position, sphereRadius);
    }
}