using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MouseTracker : MonoBehaviour
{
    [SerializeField] private Camera drawCamera;
    [SerializeField] private Camera renderCamera;
    [SerializeField] private GameObject inkSource;
    
    
    private GameObject currentInk;
    private bool isDrawing = false;
    private List<GameObject> lines = new List<GameObject>();
    

    // Update is called once per frame
    void Update()
    {
        var mousePos = UnityEngine.Input.mousePosition;
        
        isDrawing = Input.GetMouseButton(0);
        if(Input.GetMouseButtonDown(0)) {
            // create new ink
            currentInk = GameObject.Instantiate(inkSource, mousePos, Quaternion.identity);
            lines.Add(currentInk);
            
        } else if(Input.GetMouseButtonUp(0)) {
            // unbind ink
            currentInk = null;
        }
        
        if(isDrawing && currentInk){
            currentInk.transform.position = drawCamera.ScreenPointToRay(mousePos).GetPoint(19);
        }
        
        if(Input.GetKeyDown(KeyCode.Space)){
            var comp = renderCamera.GetComponent<SymbolRenderer>();
            if(comp){
                Debug.Log("Starting render");
                comp.GrabTexture();
            } else {
                Debug.LogWarning("Could not get a SymbolRenderer Component");
            }
        }
    }
    
    public void Reset(){
        foreach (var line in lines)
        {
            GameObject.Destroy(line);
        }
    }
}
