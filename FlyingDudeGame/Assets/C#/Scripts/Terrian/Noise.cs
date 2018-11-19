using UnityEngine;
using UnityEditor;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight,int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
    {
        System.Random rng = new System.Random(seed);
        Vector2[] octavesOffset = new Vector2[octaves];
        for(int i = 0; i<octaves; i++)
        {
            float offsetX = rng.Next(-100000, 10000) + offset.x;
            float offsetY = rng.Next(-100000, 10000) + offset.y;
            octavesOffset[i] = new Vector2(offsetX, offsetY);
        }


        float[,] noiseMap = new float[mapWidth, mapHeight];
        if (scale <= 0)
        {
            scale = 0.001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;
        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {

                float amp = 1;
                float freq = 1;
                float noiseHeight = 0;
                //
                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x-halfWidth) / scale * freq + octavesOffset[i].x;
                    float sampleY = (y - halfHeight) / scale * freq + octavesOffset[i].y;

                    //some 0-1
                    float perlinVal = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinVal * amp;

                    //decrease amp
                    amp *= persistance;
                    //increase freq
                    freq *= lacunarity;
                }

                //keep track for normalisiation
                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                noiseMap[x, y] = noiseHeight;
            }
        }


        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]); //normalise noisemap
            }
        }
        return noiseMap;
    }
}