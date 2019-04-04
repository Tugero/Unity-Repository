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
    public bool inAir;
    private bool hasJetPack;
    private bool hasParashute;
    private bool hasScicle;
    private bool isFlipped;
    public float ParashuteStrength;
    public float JetPackStrength;
    public int JetPackFuel;
    public int jetPackEfficeincy = 5;
    Rigidbody rb3D;
    public GameObject Camera;
    public Text FuelGage;
    public Vector3 Velocity;
    public float HP;

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Animator>();
        rb3D = gameObject.GetComponent<Rigidbody>();
        Screen.lockCursor = true;
        hasJetPack = false;
        hasParashute = false;
        hasScicle = false;
        FuelGage.text = "Fuel: " + JetPackFuel;
        isFlipped = false;
    }

    // Update is called once per frame
    void Update()
    {
        JetPackFuel = Mathf.Clamp(JetPackFuel, 0, 100);
        HP = GetComponent<DamageCalculations>().CHP;
    }

    void FixedUpdate()
    {
        if (HP > 0)
        {
            Velocity = rb3D.velocity;
            playerMover();
            //CameraRotater();
            JetPack();
            Parashute();
            PlayerFlipper();
            CameraRotater();
            SickleAttack();
        }
        if (Velocity.y > 3){
            Debug.Log("Slowing player down!");
            rb3D.AddForce(new Vector3(0, -200, 0));
         }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (isFlipped ==false)
                rb3D.AddForce(new Vector3(0, JumpForce , 0 ), ForceMode.Impulse);
                if (isFlipped == true)
                    rb3D.AddForce(new Vector3(0, -JumpForce, 0), ForceMode.Impulse);
            }
            FuelGage.text = "Fuel: " + JetPackFuel;
            JetPackFuel += jetPackEfficeincy;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            inAir = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            //Jump controll
            if (Input.GetKey(KeyCode.Space))
            {
                inAir = true;
                Player.SetInteger("Move", 5);
            }
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
        if (other.gameObject.CompareTag("WeaponUpOne"))
        {
            other.gameObject.SetActive(false);
            hasScicle = true;
        }
    }

    public void Parashute()
    {
        if (isFlipped == false)
        {
            if (hasParashute)
            {
                if (Velocity.y < 0)
                    rb3D.drag = ParashuteStrength;
            }
            if (Velocity.y >= 0)
            {
                rb3D.drag = 0;
            }
        }
        if (isFlipped == true)
        {
            if (hasParashute)
            {
                if (Velocity.y > 0)
                    rb3D.drag = ParashuteStrength;
            }
            if (Velocity.y <= 0)
            {
                rb3D.drag = 0;
            }
        }
    }

    public void JetPack()
    {
        if (hasJetPack)
        {
            if (JetPackFuel >0)
            if (Input.GetKey(KeyCode.Space))
            {
                    FuelGage.text = "Fuel: " + JetPackFuel;
                    if (isFlipped==false)
                    rb3D.AddForce(new Vector3(0, JetPackStrength, 0));
                    if (isFlipped == true)
                        rb3D.AddForce(new Vector3(0, -JetPackStrength, 0));
                    Debug.Log("Using Jetpack");
                    JetPackFuel -= jetPackEfficeincy;
            }
        }
    }
    public void playerMover()
    {
        transform.position = transform.position + new Vector3(Camera.transform.forward.x,0,Camera.transform.forward.z) * NS * Time.deltaTime;
        transform.position = transform.position + Camera.transform.right * EW * Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
        {
            NS = PlayerSpeed;
            if (!inAir)
            Player.SetInteger("Move", 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            NS = -PlayerSpeed;
            if (!inAir)
                Player.SetInteger("Move", 4);
        }
        if (Input.GetKey(KeyCode.D))
        {
            EW = PlayerSpeed;
            if (!inAir)
                Player.SetInteger("Move", 2);
        }
        if (Input.GetKey(KeyCode.A))
        {
            EW = -PlayerSpeed;
            if (!inAir)
                Player.SetInteger("Move", 3);
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            NS = 0;
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            EW = 0;
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)&& !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            if (!inAir)
                Player.SetInteger("Move", 0);
        }
    }

    public void CameraRotater()
    {
        CameraX = Input.GetAxis("Mouse X");
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

    private void PlayerFlipper()
    {
        if (Physics.gravity == new Vector3(0, 9.81f, 0))
        {
            if (isFlipped == false)
            {
                Debug.Log("Flipping Player");
                transform.rotation = Quaternion.Euler(180, 0,0);
                isFlipped = true;
            }
        }
        if (Physics.gravity == new Vector3(0, -9.81f, 0))
        {
            if (isFlipped == true)
            {
                Debug.Log("Flipping Player");
                transform.rotation = Quaternion.Euler(0, 0, 0);
                isFlipped = false;
            }
        }
    }

    private void SickleAttack()
    {
        if (hasScicle == true)
        {
            if (Input.GetKey(KeyCode.Q))
                Player.Play("SCIAttack");
        }
    }
}
