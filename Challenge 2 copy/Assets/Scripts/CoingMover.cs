using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movecoins : MonoBehaviour
{

    public Vector2 a;
    public Vector2 b;
    private float t;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 lerp = Vector2.Lerp(a, b, t);
    }
    
}
