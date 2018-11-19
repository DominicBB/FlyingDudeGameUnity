using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator  {

    public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve meshHeightCurve, int LoD)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

    
        int simplificationIncrement = (LoD == 0)?1:LoD * 2;
        int vertPerLine = (width - 1) / simplificationIncrement + 1;

        MeshData meshData = new MeshData(vertPerLine, vertPerLine);
        int vertexIndex = 0;

        for (int y = 0; y < height; y+= simplificationIncrement)
        {
            for (int x = 0; x < width; x += simplificationIncrement)
            {
                if (x < width - 1 && y < height - 1)
                {
                    meshData.AddTriangle(vertexIndex, vertexIndex + vertPerLine + 1, vertexIndex + vertPerLine);
                    meshData.AddTriangle(vertexIndex + vertPerLine + 1, vertexIndex, vertexIndex + 1);
                }
                meshData.vertices[vertexIndex] = new Vector3(x + topLeftX, meshHeightCurve.Evaluate(heightMap[x, y]) * heightMultiplier, topLeftZ - y);
                meshData.uvs[vertexIndex++] = new Vector2(x/(float)width, y/(float)height);
            }
            
        }
        return meshData;
    }
}

//should use this formnat with my thingy
public class MeshData
{
    public Vector2[] uvs;
    public Vector3[] vertices;
    public int[] triangles;

    private int triangleIndex = 0;

    public MeshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshHeight * meshWidth];
        uvs = new Vector2[meshHeight * meshWidth];
        triangles = new int[((meshHeight-1) * (meshWidth-1)) *6];

    }

    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex +1] = b;
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
