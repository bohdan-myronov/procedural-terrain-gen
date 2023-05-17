using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int width, int height, float scale, int octaves, float persistance, float lacunarity, int seed)
    {
        if (octaves <= 0) octaves = 1;
        
        float[,] noiseMap = new float[width, height];

        float maxNoiseValue = float.MinValue;
        float minNoiseValue = float.MaxValue;

        Vector2[] offsets = SeedNoiseMap(seed, octaves);

        float halfwidth = width / 2;
        float halfheight = height / 2;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float value = 0;
                for (int i = 0; i < octaves; i++)
                {
                    float x_ = (x-halfwidth) / scale * frequency + offsets[i].x;
                    float y_ = (y-halfheight) / scale * frequency + offsets[i].y;
                    float temp = Mathf.PerlinNoise(x_, y_) * 2 - 1;
                    noiseMap[x, y] = temp;
                    value += temp * amplitude;
                    amplitude *= persistance;
                    frequency *= lacunarity;
                    
                }
                if (value > maxNoiseValue)
                {
                    maxNoiseValue = value;
                }
                else if (value < minNoiseValue)
                {
                    minNoiseValue = value;
                }
                noiseMap[x, y] = value;
            }
        }
        for (int y = 0; y < height; y++)
        {

            for (int x = 0; x < width; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseValue, maxNoiseValue, noiseMap[x, y]);
            }
        }
        return noiseMap;
    }

    private static Vector2[] SeedNoiseMap(int seed,int octaves)
    {
        Vector2[] offsets = new Vector2[octaves];

        System.Random random = new System.Random(seed);
        for (int i = 0; i < offsets.Length; i++)
        {
            Vector2 temp = new Vector2(random.Next(10000), random.Next(10000));
            offsets[i] = temp;
        }
        return offsets;
    }
    public static Color[] GenerateColorMap(float[,] noiseMap, TerrainType[] regions)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);
        Color[] colormap = new Color[width * height];
        if (regions == null) throw new System.Exception("No region preset included");
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight < regions[i].heightCondition)
                    {
                        colormap[y * width + x] = regions[i].color;
                        break;
                    }
                }
            }
        }
        return colormap;
    }
}
