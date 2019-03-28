using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Add Controller Support!!!
public class PlayerMover : MonoBehaviour
{
    Animator Player;
    public float PlayerSpeed;
    public float JumpForce;
    private float NS;
    private float EW;
    private float CameraX;
    private float CameraY;
    public float CameraYAngle;
    public float YAngleMin = -.22f;
    public float YAngleMax= .25f;
    private bool inAir;
    private bool hasJetPack;
    private bool hasParashute;
    public float ParashuteStrength;
    public float JetPackStrength;
    public float JetPackFuel =100f;
    public float jetPackEfficeincy = 5;
    Rigidbody rb3D;
    public GameObject Camera;
    public Text FuelGage;
    public Vector3 Velocity;

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Animator>();
        rb3D = gameObject.GetComponent<Rigidbody>();
        Screen.lockCursor = true;
        hasJetPack = false;
        hasParashute = false;
        FuelGage.text = "Fuel: " + JetPackFuel;
    }

    // Update is called once per frame
    void Update()
    {
        JetPackFuel = Mathf.Clamp(JetPackFuel, 0.0f, 100.00f);
    }

    void FixedUpdate()
    {
        Velocity = rb3D.velocity;
        playerMover();
        CameraRotater();
        JetPack();
        Parashute();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rb3D.AddForce(new Vector3(0, JumpForce,0), ForceMode.Impulse);
            }
            JetPackFuel += jetPackEfficeincy;
            FuelGage.text = "Fuel: " + JetPackFuel;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("JetPack"))
        {
            other.gameObject.SetActive(false);
            hasJetPack = true;
            Debug.Log("Obtained Jetpack");
        }
        if (other.gameObject.CompareTag("Parashute"))
        {
            other.gameObject.SetActive(false);
            hasParashute = true;
        }
    }

    public void Parashute()
    {
        if (hasParashute)
        {
            if (Velocity.y < 0)
            rb3D.drag = ParashuteStrength;
        }
        if (Velocity.y >=0)
        {
            rb3D.drag = 0;
        }
    }

    public void JetPack()
    {
        if (hasJetPack)
        {
            if (JetPackFuel >0)
            if (Input.GetKey(KeyCode.Space))
            {
                rb3D.AddForce(new Vector3(0, JetPackStrength, 0));
                Debug.Log("Using Jetpack");
                    JetPackFuel -= jetPackEfficeincy;
                    FuelGage.text = "Fuel: "+JetPackFuel;
            }
        }
    }
    public void playerMover()
    {
        transform.position = transform.position + new Vector3(Camera.transform.forward.x,0,Camera.transform.forward.z) * NS * Time.deltaTime;
        transform.position = transform.position + Camera.transform.right * EW * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.W))
        {
            NS = PlayerSpeed;
            Player.SetInteger("Move", 1);
        }
        if (Input.GetKeyDown(KeyCode.S))
            NS = -PlayerSpeed;
        if (Input.GetKeyDown(KeyCode.D))
            EW = PlayerSpeed;
        if (Input.GetKeyDown(KeyCode.A))
            EW = -PlayerSpeed;
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            NS = 0;
            Player.SetInteger("Move", 0);
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            EW = 0;
    }

    public void CameraRotater()
    {
        CameraX = Input.GetAxis("Mouse X");
       //CameraX = Input.GetAxis("4");
        CameraY = -Input.GetAxis("Mouse Y");
        transform.Rotate(0, CameraX, 0);
        CameraYAngle = Camera.transform.rotation.x; 
        Camera.gameObject.transform.Rotate(CameraY, 0, 0);
        if (Camera.transform.localRotation.x >= YAngleMax)
        {
            Camera.gameObject.transform.Rotate(-CameraY, 0, 0);
        }
        if (Camera.transform.localRotation.x <= YAngleMin)
        {
            Camera.gameObject.transform.Rotate(-CameraY, 0, 0);
        }
    }
}
