using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width, height;
    public float heightMultiplier;
    public float scale;
    public int octaves;
    public float persistence;
    public float lacunarity;
    
    public int seed;
    public bool autoUpdate;
    public bool useDiversiveTexture;
    public AnimationCurve meshHeightCurve;
    public Presets[] presets;
    public Region[] regions;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(width, height, scale, octaves, persistence, lacunarity, seed);
      
    }
    public void DrawMap(float[,] noiseMap)
    {
        Color[] colorMap;
        MapRenderer display = FindObjectOfType<MapRenderer>();
        if (useDiversiveTexture) colorMap = Noise.GenerateDiversiveColorMap(noiseMap, regions);
        else colorMap = Noise.GenerateColorMap(noiseMap, regions);
        MeshData meshData = MeshGenerator.GenerateTerrain(noiseMap, heightMultiplier, meshHeightCurve);
        display.DrawMesh(meshData, TextureGenerator.TextureFromColorMap(colorMap, width, height));
    }
}
[System.Serializable]
public struct Presets
{
    public Region[] regions;
    public AnimationCurve curve;
}
[System.Serializable]
public struct Region
{
    public string name;
    public float heightCondition;
    public Color colorThreshold;
    public Color colorBound;
}