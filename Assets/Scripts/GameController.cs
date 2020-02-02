using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameController : MonoBehaviour
{
    [SerializeField] private SymbolRenderer symbolRenderer;
    [SerializeField] private SymbolDisplayer symbolDisplayer;
    
    
    [SerializeField] private bool countDown = false;
    [SerializeField] private Text[] timerText;
    private float startTime = 0;
    
    
    [SerializeField] private GameObject[] leftSprites;
    [SerializeField] private GameObject[] rightSprites;
    
    Texture2D currentStrokeTexture;
    int currentIndex = 0;
    public void GotToScene(int scene){
        SceneManager.LoadScene(scene);
    }
    
    void Start(){
        startTime = Time.time;
    }
    
    void Awake(){
        startTime = Time.time;
        SymbolRenderer.dataRendered += FinalizeSymbol_step2;
    }
    
    void Update(){
        if (!countDown) {
            return;
        }
        
        var remainingTime = 10 + startTime - Time.time;
        Debug.LogFormat("Remaining time: {0}", remainingTime);
        if(remainingTime < 0) {
            GotToScene(3);
            gameObject.SetActive(false);
        }
        
        var remainingTimeText = string.Format("{0:00:0}", Mathf.Floor((remainingTime) * 10f));
        foreach (var text in timerText)
        {
            text.text = $"{remainingTimeText}";
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
        var remainingTime = Time.time - startTime;
        startTime += 10;
        
        
        Debug.LogFormat("Got a result of {0}%", Mathf.FloorToInt((float) result * 100));
        
        symbolDisplayer.Next();
    }
    
    public void MainMenu(){
        GotToScene(0);
    }
    public void TogglePause(){
        // .
    }
}
