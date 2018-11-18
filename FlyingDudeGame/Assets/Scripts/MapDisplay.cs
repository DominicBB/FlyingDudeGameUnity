using UnityEngine;
using System.Collections;

public class MapDisplay:MonoBehaviour
{

    public Renderer textureRenderer;
    public MeshFilter MeshFilter;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;

    public void DrawTexture(Texture2D texture)
    {
       
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);

        
    }

    public void DrawMesh(MeshData meshData, Texture2D texture2D)
    {
        Mesh mesh = meshData.CreateMesh();
        MeshFilter.sharedMesh = mesh;
        meshRenderer.sharedMaterial.mainTexture = texture2D;
        meshCollider.sharedMesh = mesh;

    }
}
