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
    
    
    public GameObject[] leftSprites;
    public GameObject[] rightSprites;
    [SerializeField] private Text[] liveScores;
    [SerializeField] private Text finalScore;
    
    public static float[] scores = new float[10];
    
    Texture2D currentStrokeTexture;
    int currentIndex = 0;
    public void GotToScene(int scene){
        SceneManager.UnloadSceneAsync(this.gameObject.scene);
        SceneManager.LoadSceneAsync(scene);
    }
    
    void Start(){
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
        
        Debug.Log("START!");
        currentIndex = 0;
        startTime = Time.time;
    }
    
    void Awake(){
        Debug.LogFormat("AWAKEN! With lists {0}, {1}", leftSprites.Length, rightSprites.Length);
        currentIndex = 0;
        startTime = Time.time;
        if(countDown){
            SymbolRenderer.dataRendered += FinalizeSymbol_step2;
        }
    }
    
    void Update(){
        
        if(liveScores != null && liveScores.Length > 0) {
            foreach (var score in liveScores)
            {
                score.text = string.Format("{0:00}", scores.Sum());
            }
        }
        
        if(finalScore != null) {
            finalScore.text = string.Format("{0:00}", scores.Sum());
        }
        
        if (!countDown) {
            return;
        }
        
        var remainingTime = 10 + startTime - Time.time;
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
        if(currentIndex < 10){
            currentStrokeTexture = symbolDisplayer.currentTexture;
            symbolRenderer.GrabTexture(); // will fire async dataRendered event
        }
    }
        
    void FinalizeSymbol_step2(Color[] canvas){
        var result = Random.Range(0,100);
        // var result = Grid2DUtil.CompareGrid(
        //     currentStrokeTexture.GetPixels(0, 0, currentStrokeTexture.width, currentStrokeTexture.height),
        //     canvas
        // );
        // Debug.LogFormat("Got a result of {0}%", Mathf.FloorToInt((float) result * 100));
        scores[currentIndex] = result;
        
        var img = Sprite.Create(currentStrokeTexture, new Rect(0, 0, 1024, 1024), new Vector2(0.5f, 0.5f), 100, 0, SpriteMeshType.FullRect);
        
        Debug.LogFormat("Trying to get index {0} from list of length {1}", currentIndex, leftSprites.Count());
        
        leftSprites[currentIndex].GetComponent<Image>().sprite = img;
        rightSprites[currentIndex].GetComponent<Image>().sprite = img;
        
        var remainingTime = Time.time - startTime;
        startTime += 10;
    
        symbolDisplayer.Next();
        
        currentIndex++;
        
        if(currentIndex >= 10) {
            Debug.Log("Ending game!");
            Time.timeScale = 0;
            StartCoroutine(WaitThenFinish(1));
        }
    }
    
    public void MainMenu(){
        GotToScene(0);
    }
    public void TogglePause(){
        // .
    }

    IEnumerator WaitThenFinish(float seconds)
    {
        Debug.Log("... starting timer!");

        yield return new WaitForSecondsRealtime(seconds);
        Debug.Log("... timer done!!");
        Time.timeScale = 1;
        currentIndex = 0;
        GotToScene(4);
    }
    
    void OnDestroy(){
        SymbolRenderer.dataRendered -= FinalizeSymbol_step2;
    }

}
