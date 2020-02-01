using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Camera))]
public class SymbolRenderer : MonoBehaviour
{
    [SerializeField, Range(2, 256)] private int textureSize = 32;
    Texture2D tex;
    RenderTexture rt;
    private bool isRendering = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        tex = new Texture2D(textureSize, textureSize, TextureFormat.RGB24, false);
    }
    public void GrabTexture(){
        isRendering = true;
    }
    
    public void OnPostRender(){
        Debug.Log("Postrender...");
        if(isRendering){
            isRendering = false;
            Debug.LogFormat("Got greyscales: {0}", string.Join(" ", colorToGrayscale(GetPixels())));
        }
    }
    
    public Color[] GetPixels(){
        tex.ReadPixels(new Rect(0, 0, textureSize, textureSize), 0, 0);
        return tex.GetPixels(0, 0, textureSize, textureSize);
    }
    
    private float[] colorToGrayscale(Color[] colors){
        return colors.Select(c => c.grayscale < 0.5f ? 1f : 0f).ToArray();
    }
}
