using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothChanger : MonoBehaviour
{
    public SkinnedMeshRenderer mesh;

    public Texture2D defaultTexture;
    public Texture2D texture;
    public string shaderIdName = "_EmissionMap";

    private void Awake()
    {
        ResetTexture();
    }

    public void ChangePlayerTexture()
    {
        mesh.sharedMaterials[0].SetTexture(shaderIdName, texture);
    }

    public void ChangePlayerTexture(ClothSetup setup)
    {
        mesh.sharedMaterials[0].SetTexture(shaderIdName, setup.texture);
    }

    [NaughtyAttributes.Button]
    public void ChangeTexture()
    {
        mesh.sharedMaterials[0].SetTexture(shaderIdName, texture);
    }

    public void ResetTexture()
    {
        mesh.sharedMaterials[0].SetTexture(shaderIdName, defaultTexture);
    }
}
