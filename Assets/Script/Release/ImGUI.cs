using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImGUI : MonoBehaviour
{
    MapGenerator mapGen;

    int width;
    int height;
    void Start()
    {
        mapGen = GetComponent<MapGenerator>();
    }

    void Update()
    {
        width = Screen.width/16;
        height = Screen.height/9;
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical("box");
        GUILayout.Label($"width = {mapGen.width}");
        mapGen.width = (int)GUILayout.HorizontalSlider(mapGen.width, 1, 241);
        GUILayout.Label($"height = {mapGen.height}");
        mapGen.height = (int)GUILayout.HorizontalSlider(mapGen.height, 1, 241);
        GUILayout.Label($"scale = {mapGen.scale}");
        mapGen.scale = GUILayout.HorizontalSlider(mapGen.scale, 1, 500);
        GUILayout.Label($"heightMultiplier = {mapGen.heightMultiplier}");
        mapGen.heightMultiplier = GUILayout.HorizontalSlider(mapGen.heightMultiplier, 1, 100);
        GUILayout.Label($"octaves = {mapGen.octaves}");
        mapGen.octaves = (int)GUILayout.HorizontalSlider(mapGen.octaves, 1, 10);
        GUILayout.Label($"persistence = {mapGen.persistence}");
        mapGen.persistence =GUILayout.HorizontalSlider(mapGen.persistence, 0, 1);
        GUILayout.Label($"lacunarity = {mapGen.lacunarity}");
        mapGen.lacunarity =GUILayout.HorizontalSlider(mapGen.lacunarity, 0, 5);
        GUILayout.Label($"seed = {mapGen.seed}");
        mapGen.seed = (int)GUILayout.HorizontalSlider(mapGen.height, int.MinValue, int.MaxValue);
        if (GUILayout.Button("Generate"))
        {
            mapGen.GenerateMap();
        }
        
        GUILayout.EndVertical();
    }


}
