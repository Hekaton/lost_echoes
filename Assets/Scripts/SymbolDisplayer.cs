using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SymbolDisplayer : MonoBehaviour
{
    public Texture2D currentTexture;
    [SerializeField] Texture2D[] availableSymbols;
    
    
    List<int> availableIndeces;
    
    // Start is called before the first frame update
    void Awake()
    {
        Reset();
        Next();
    }
    
    public void Reset(){
        availableIndeces = availableSymbols.Select((t, i) => i).ToList();
    }
    
    public void Next(){
        int newSymbol = availableIndeces[Random.Range(0, availableIndeces.Count())];
        availableIndeces.Remove(newSymbol);        
        currentTexture = availableSymbols[newSymbol];
        var renderer = GetComponent<Renderer>();
        if(renderer) {
            renderer.material.SetTexture($"_MainTex", currentTexture);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
