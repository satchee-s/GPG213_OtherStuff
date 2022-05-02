using UnityEngine;
using API;

public class Triangle : MonoBehaviour
{
    MeshFilter filter;
    MeshRenderer render;
    Material material;
    int[] indices;
    Vector3[] verticeCoordinates;
    Vector3[] verticeNormals;
    [SerializeField] int totalSquaresX;
    [SerializeField] int totalSquaresZ;
    [SerializeField] Texture2D heightMap;
    [SerializeField] int heightMultiplier;
    [SerializeField] Color col;
    int totalVerticesX;
    int totalVerticesZ;
    
    void Start()
    {
        GenerateTerrain terrain = new GenerateTerrain();
        Coordinates coordinates = new Coordinates();

        totalVerticesX = totalSquaresX + 1;
        totalVerticesZ = totalSquaresZ + 1;

        filter = gameObject.AddComponent<MeshFilter>();
        render = gameObject.AddComponent<MeshRenderer>();
        material = render.material;
        material.color = col;

        coordinates.SetCoordinates(out verticeCoordinates, heightMap, totalVerticesX, totalVerticesZ, heightMultiplier);
        filter.mesh.vertices = verticeCoordinates;

        terrain.CalculateIndices(out indices, totalSquaresX, totalSquaresZ, out verticeNormals, verticeCoordinates);
        filter.mesh.triangles = indices;
        filter.mesh.normals = verticeNormals;
    }
}
