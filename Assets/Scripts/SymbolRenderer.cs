using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Camera))]
public class SymbolRenderer : MonoBehaviour
{
    [SerializeField] private int textureSize = 1024;
    Texture2D tex;
    RenderTexture rt;
    private bool isRendering = false;
    
    public delegate void OnDataRendered(Color[] canvas);
    public static event OnDataRendered dataRendered;
    
    
    // Start is called before the first frame update
    void Start()
    {
        tex = new Texture2D(textureSize, textureSize, TextureFormat.RGB24, false);
    }
    public void GrabTexture(){
        isRendering = true;
    }
    
    public IEnumerator OnPostRender(){
        yield return new WaitForEndOfFrame();
        if(isRendering){
            isRendering = false;
            var pixels = GetPixels();
            dataRendered(pixels);
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
