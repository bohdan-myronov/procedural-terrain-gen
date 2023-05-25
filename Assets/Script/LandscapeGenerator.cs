using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeGenerator : MonoBehaviour
{
    public const float maxViewDist = 300;
    public Transform viewer;
    public static Vector2 viewerPos;
    int chunkSize;
    int chunksVisibleInViewDist;

    Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
    List<TerrainChunk> terrainChunksLastUpdated = new List<TerrainChunk>();
    private void Start()
    {
        chunkSize = 240;
        chunksVisibleInViewDist = Mathf.RoundToInt(maxViewDist / chunkSize);
    }
    private void Update()
    {
        viewerPos = new Vector2(viewer.position.x, viewer.position.z);
        UpdateVisibleCHunks();
    }
    void UpdateVisibleCHunks()
    {
        for (int i = 0; i < terrainChunksLastUpdated.Count; i++)
        {
            terrainChunksLastUpdated[i].SetVisible(false);
        }
        terrainChunksLastUpdated.Clear();
        int currentChunkCoordX = Mathf.RoundToInt(viewerPos.x / chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPos.y / chunkSize);
        for (int yOffset = 0; yOffset <= chunksVisibleInViewDist; yOffset++)
        {
            for (int xOffset = 0; xOffset <= chunksVisibleInViewDist; xOffset++)
            {
                Vector2 viewesChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);
                if (terrainChunkDictionary.ContainsKey(viewesChunkCoord))
                {
                    terrainChunkDictionary[viewesChunkCoord].Update();
                    if (terrainChunkDictionary[viewesChunkCoord].isVisible())
                    {
                        terrainChunksLastUpdated.Add(terrainChunkDictionary[viewesChunkCoord]);
                    }
                }
                else
                {
                    terrainChunkDictionary.Add(viewesChunkCoord, new TerrainChunk(viewesChunkCoord,chunkSize,transform));
                }
            }
        }
    }
    public class TerrainChunk
    {
        GameObject meshObject;
        Vector2 position;
        Bounds bounds;
        public TerrainChunk(Vector2 coord, int size, Transform parent)
        {
            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 pos3 = new Vector3(position.x, 0, position.y);
            meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
            meshObject.transform.position = pos3;
            meshObject.transform.localScale = Vector3.one * size / 10f;
            meshObject.transform.parent = parent;
            SetVisible(false);
        }
        public void Update()
        {
            float viewerDistFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPos));
            bool visible = viewerDistFromNearestEdge <= maxViewDist;
            SetVisible(visible);
        }
        public void SetVisible(bool visible)
        {
            meshObject.SetActive(visible);

        }
        public bool isVisible()
        {
            return meshObject.activeSelf;
        }
    }
}

