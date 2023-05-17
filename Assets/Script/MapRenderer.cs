using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapRenderer: MonoBehaviour
{
    public Renderer textureRenderer;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void DrawTexture(Texture2D _texture, int width, int height)
    {
        Texture2D texture = _texture;
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();

        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(width, 1, height);
    }
    public void DrawMesh(MeshData data, Texture2D texture)
    {
        meshFilter.sharedMesh = data.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
    
}
