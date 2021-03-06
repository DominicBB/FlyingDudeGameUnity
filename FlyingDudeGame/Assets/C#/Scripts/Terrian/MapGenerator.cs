﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public enum DrawMode { NoiseMap, ColorMap, Mesh}
    public DrawMode drawMode;

    const int mapChunkSize = 241; //divisable by 2,4,6,8,10,12 good for mipmapping 
    [Range(0,6)]
    public int LevelOfDetial;
    public float scale;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;
    public bool autoUpdate;

    public TerrainType[] regions;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, scale, octaves, persistance, lacunarity, offset);

        Color[] colors = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = noiseMap[x, y];
                for(int i = 0; i<regions.Length; i++)
                {
                    if(currentHeight <= regions[i].height)
                    {
                        colors[y * mapChunkSize + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if(drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }else if(drawMode == DrawMode.ColorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colors, mapChunkSize,mapChunkSize));

        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, LevelOfDetial), TextureGenerator.TextureFromColorMap(colors, mapChunkSize, mapChunkSize));

        }




    }

    private void OnValidate()
    {
        

        if (lacunarity < 1)
        {
            lacunarity = 1;
        }

        if (octaves< 0)
        {
            octaves = 0;
        }
        if (octaves > 10)
        {
            octaves = 10;
        }

    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}
