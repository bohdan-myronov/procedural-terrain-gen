using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public DrawMode drawMode;
    public int width, height;
    public float heightMultiplier;
    public float scale;
    public int octaves;
    public float persistence;
    public float lacunarity;
    
    public int seed;
    public bool autoUpdate;
    public AnimationCurve meshHeightCurve;
    public TerrainType[] regions;

    Texture2D texture;
    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(width, height, scale, octaves, persistence, lacunarity, seed);
        Color[] colorMap;
        MapRenderer display = FindObjectOfType<MapRenderer>();
        if (drawMode == DrawMode.HEIGHT)
        {
            texture = TextureGenerator.TextureFromHeightMap(noiseMap);
            display.DrawTexture(texture, width, height);
        }
        else if (drawMode == DrawMode.COLOR)
        {
            colorMap = Noise.GenerateColorMap(noiseMap, regions);
            texture = TextureGenerator.TextureFromColorMap(colorMap, width, height);
            display.DrawTexture(texture, width, height);
        }
        else if (drawMode == DrawMode.MESH) {
            colorMap = Noise.GenerateColorMap(noiseMap, regions);
            MeshData meshData = MeshGenerator.GenerateTerrain(noiseMap,heightMultiplier,meshHeightCurve);
            display.DrawMesh(meshData, TextureGenerator.TextureFromColorMap(colorMap, width, height));
        }
    }
}
[System.Serializable]
public struct TerrainType
{
    public string name;
    public float heightCondition;
    public Color color;
}
public enum DrawMode
{
    HEIGHT,
    COLOR,
    MESH   
}
