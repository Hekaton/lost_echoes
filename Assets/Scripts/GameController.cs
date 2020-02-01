using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Display.displays[0].Activate();
        Display.displays[1].Activate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
