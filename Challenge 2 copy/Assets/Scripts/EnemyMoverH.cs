using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoverH : MonoBehaviour
{
    private Vector2 start;
    private Vector2 raise;
    private Vector2 mover;
    public float flip;
    private bool flipper;
    // Start is called before the first frame update

    void Start()
    {
        start = gameObject.transform.position;
        raise = start + new Vector2(0, 5);
        flip = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //flip = Mathf.PingPong(Time.fixedDeltaTime, 1f);
        transform.position = Vector2.Lerp(start, raise, Mathf.PingPong(Time.fixedTime/3, 1f));
    }
}
