using System.Collections;
using UnityEngine;

public class EvasiveManuvers : MonoBehaviour
{
    public float dodge;
    public float tilt;
    public float smoothing;
    public Vector2 startWait;
    public Vector2 manuverTime;
    public Vector2 manuverWait;
    public Boundary boundry;

    private float currentSpeed;
    private float targetManuver;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = rb.velocity.z;
        StartCoroutine(Evade());
    }

    IEnumerator  Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            targetManuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(manuverTime.x, manuverTime.y));
            targetManuver = 0;
            yield return new WaitForSeconds(Random.Range(manuverWait.x, manuverWait.y));
        }
    }

    void FixedUpdate()
    {
        float newManuver = Mathf.MoveTowards(rb.velocity.x, targetManuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManuver, 0.0f, -currentSpeed);
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundry.xMin, boundry.xMax), 0.0f, Mathf.Clamp(rb.position.z, boundry.zMin, boundry.zMax));
        rb.rotation = Quaternion.Euler(0.0f, 180.0f, rb.velocity.x * -tilt);
    }
}
