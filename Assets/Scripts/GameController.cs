using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Camera viewCamera;
    [SerializeField] private Camera drawCamera;
    [SerializeField] private Transform mouseFollower;
    
    // Start is called before the first frame update
    void Start()
    {
        Display.displays[0].Activate();
        Display.displays[1].Activate();
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = UnityEngine.Input.mousePosition;
        mouseFollower.position = Camera.main.ScreenPointToRay(mousePos).GetPoint(5);
    }
}
