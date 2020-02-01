using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineTest : MonoBehaviour
{
    public Spline2D spline1 = new Spline2D();

    public Spline2D spline2 = new Spline2D();
    
    IEnumerable<Vector2> _spline1Points = new List<Vector2>();
    IEnumerable<Vector2> _spline2Points = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        spline1.AddPoints(_spline1Points);
        spline2.AddPoints(_spline2Points);
        
        Debug.Log("Initialize both splines");

        Debug.Log("Calculating Difference");
        float difference = Spline2DUtil.CalculateDifference(spline1, spline2);
        Debug.LogFormat("Difference is: {0}", difference);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
