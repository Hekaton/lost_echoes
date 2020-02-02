﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameController : MonoBehaviour
{
    [SerializeField] private SymbolRenderer symbolRenderer;
    [SerializeField] private SymbolDisplayer symbolDisplayer;
    
    
    [SerializeField] private Text[] timerText;
    [SerializeField] private float startTime;
    
    
    [SerializeField] private GameObject[] leftSprites;
    [SerializeField] private GameObject[] rightSprites;
    
    Texture2D currentStrokeTexture;
    int currentIndex = 0;
    public void GotToScene(int scene){
        SceneManager.LoadScene(scene);
    }
    
    void Awake(){
        SymbolRenderer.dataRendered += FinalizeSymbol_step2;
    }
    
    void Update(){
        // timerText.Select(v => v.text = $"{Time.time - startTime}");
        var remainingTime = string.Format("{0:00:0}", 100f - Mathf.Floor((Time.time - startTime) * 10f));
        foreach (var text in timerText)
        {
            text.text = $"{remainingTime}";
        }
    }
    
    public void FinalizeSymbol(){
        currentStrokeTexture = symbolDisplayer.currentTexture;
        symbolRenderer.GrabTexture(); // will fire async dataRendered event
    }
        
    void FinalizeSymbol_step2(Color[] canvas){
         
        var result = Grid2DUtil.CompareGrid(
            currentStrokeTexture.GetPixels(0, 0, currentStrokeTexture.width, currentStrokeTexture.height),
            canvas
        );
        
        var img = Sprite.Create(currentStrokeTexture, new Rect(0, 0, 1024, 1024), new Vector2(0.5f, 0.5f), 100, 0, SpriteMeshType.FullRect);
        
        leftSprites[currentIndex].GetComponent<Image>().sprite = img;
        rightSprites[currentIndex].GetComponent<Image>().sprite = img;
        
        currentIndex++;
        startTime = Time.time;
        
        
        Debug.LogFormat("Got a result of {0}%", Mathf.FloorToInt((float) result * 100));
        
        symbolDisplayer.Next();
    }
    
    public void MainMenu(){
        // .
    }
    public void TogglePause(){
        // .
    }
}
