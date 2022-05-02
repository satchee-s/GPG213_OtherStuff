using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int tileWidth; //x
    public int tileHeight; //z
    public int totalTilesX;
    public int totalTilesZ;
    public Node[] nodes;
    LayerMask layerMask;

    private void Start()
    {
        layerMask = LayerMask.GetMask("Obstacles");
        nodes = new Node[totalTilesZ * totalTilesX];
        for (int z = 0; z < totalTilesZ; z++)
        {
            for (int x = 0; x < totalTilesX; x++)
            {
                int i = x + z * totalTilesX;
                Vector3 position = transform.position + new Vector3(((float)tileWidth / 2.0f) + x * tileWidth, 0,
                                   ((float)tileHeight / 2.0f) + z * tileHeight);
                bool isWalkable = !Physics.CheckBox(position, new Vector3(tileWidth / 2.0f, 0f, tileHeight / 2.0f),
                                                    Quaternion.identity, layerMask);
                nodes[i] = new Node(position, isWalkable);
            }
        }
    }
    public Node GetNode(Vector3Int nodePos)
    {
        int i = nodePos.x + (nodePos.z * (int)totalTilesX);
        return nodes[i];
    }

    public Vector3Int NodeFromWorldPosition(Vector3 pos)
    {
        float x = pos.x / tileWidth;
        float z = pos.z / tileHeight;
        Vector3Int gridCoordinates = new Vector3Int((int)x, 0, (int)z);
        return gridCoordinates;
    }
}
