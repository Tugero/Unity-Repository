using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWalls : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public int speed=1;
    void Update()
    {
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time/3f, 1));
    }

}

