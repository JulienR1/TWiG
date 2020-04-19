using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRender;

    public void DrawTexture(Texture2D texture)
    {
        textureRender.enabled = true;
        textureRender.sharedMaterial.mainTexture = texture;
    }

    public void DrawNone()
    {
        textureRender.enabled = false;
    }

}