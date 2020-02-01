using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameController : MonoBehaviour
{
    [SerializeField] private SymbolRenderer symbolRenderer;
    Texture2D currentStrokeTexture;
    public void GotToScene(int scene){
        SceneManager.LoadScene(scene);
    }
    
    void Start(){
        SymbolRenderer.dataRendered += FinalizeSymbol_step2;
        currentStrokeTexture = new Texture2D(32, 32, TextureFormat.ARGB32, 0, true);
    }
    
    public void FinalizeSymbol(){
        symbolRenderer.GrabTexture(); // will fire async dataRendered event
    }
    
    void FinalizeSymbol_step2(Color[] canvas){
         
        var result = Grid2DUtil.CompareGrid(
            currentStrokeTexture.GetPixels(0, 0, currentStrokeTexture.width, currentStrokeTexture.height),
            canvas
        );
        
        Debug.LogFormat("Got a result of {0}%", Mathf.FloorToInt((float) result * 100));
    }
}
