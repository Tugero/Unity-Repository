using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyS2S : MonoBehaviour
{
    private Vector2 start;
    private Vector2 raise;
    private Vector2 mover;
    public float flip;
    private bool facingRight;

    // Start is called before the first frame update

    void Start()
    {
        start = gameObject.transform.position;
        raise = start + new Vector2(20, 0);
        facingRight = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        flip = Mathf.PingPong(Time.fixedTime / 6, 1f);
        //flip = Mathf.PingPong(Time.fixedDeltaTime, 1f);
        transform.position = Vector2.Lerp(start, raise, flip);
        Flip();
    }
    void Flip()
    {
        //flips sprite
        if (flip == 0f && !facingRight|| flip == 1f && facingRight)
        {
           // Debug.Log("flipping");
            facingRight = !facingRight;
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        }
    }
}
