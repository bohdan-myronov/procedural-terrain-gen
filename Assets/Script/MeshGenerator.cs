using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static MeshData GenerateTerrain(float[,] noiseMap, float heightMultiplier,AnimationCurve curve)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);
        float halfWidth = (width - 1) / 2;
        float halfHeight = (height - 1) / 2;
        MeshData meshData = new MeshData(width, height);
        int vertexIndex = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
               meshData.vertices[vertexIndex] = new Vector3(x-halfWidth , curve.Evaluate(noiseMap[x, y])* heightMultiplier, y-halfHeight);
                //meshData.vertices[vertexIndex] = new Vector3(x, noiseMap[x, y], y);
                if (x < width  -1 && y < height - 1)
                {
                    meshData.AddTriangle(vertexIndex, vertexIndex + width, vertexIndex + width+1);
                    meshData.AddTriangle(vertexIndex, vertexIndex+width +1, vertexIndex + 1);
                    meshData.uvs[vertexIndex] = new Vector2(1- x / (float)width, 1- y / (float)height);
                }
                vertexIndex++;
            }
        }
        return meshData;
    }
}
public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;
    int triangleIndex;

    public MeshData(int width, int height)
    {
        vertices = new Vector3[width * height];
        uvs = new Vector2[width * height];
        triangles = new int[(width-1) * (height)*6];
    }
    public void AddTriangle (int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex+1] = b;
        triangles[triangleIndex+2] = c;
        triangleIndex += 3;
    }
    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}

