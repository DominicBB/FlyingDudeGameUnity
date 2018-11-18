
using UnityEngine;

public class TerrarinGenerator : MonoBehaviour {

    public int width;
    public int height;

    public int depth;

    public int scale;

    private int offsetx;
    private int offsety;
	// Use this for initialization
	void Start () {

        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        offsetx = Random.Range(100, 1000);
        offsety = Random.Range(100, 1000);
        
    }

    private TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for(int x = 0; x<width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x,y);
            }
        }

        return heights;
    }

    private float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetx;
        float yCoord = (float)y / height * scale + offsety;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    
    

   
}
