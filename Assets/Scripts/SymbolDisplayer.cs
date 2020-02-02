using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SymbolDisplayer : MonoBehaviour
{
    
    [SerializeField] Texture2D[] availableSymbols;
    
    
    List<int> availableIndeces;
    
    // Start is called before the first frame update
    void Start()
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
        var renderer = GetComponent<Renderer>();
        if(renderer) {
            renderer.material.SetTexture($"_MainTex", availableSymbols[newSymbol]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
