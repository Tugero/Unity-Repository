using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator cat;
    private Rigidbody2D rb2d;
    public GameObject H1;
    public GameObject H2;
    public GameObject H3;
    public GameObject Camera;
    public GameObject BaseMusic;
    public GameObject WinMusic;
    public float speed;
    public float Movementspeed;
    public float jumpForce;
    public Text ScoreText;
    public Text WinText;
    public Text LoseText;
    public int score;
    private int Hp=3;
    private bool isAlive;
    private bool level2;
    public bool inAir;
    public bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        ScoreText.text= "x0";
        WinText.text = "";
        LoseText.text = "";
        isAlive = true;
        level2 = false;
        cat = GetComponent<Animator>();
        facingRight = true;
        WinMusic.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive == true)
        {
            Flip();
            movePlayer(speed);
            //move right
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
               speed= Movementspeed;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                speed = 0;
            }
            //move left
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                speed =-Movementspeed;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                speed = 0;
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Application Quit");
        }
        if (score >= 4)
        {
            //WinText.text = "!!YOU WIN!!";
            while (level2 == false)
            {
                transform.position = new Vector2(0.9f, -102.49f);
                Camera.transform.position = new Vector3(0f, -100f, -10);
                H1.SetActive(true);
                H2.SetActive(true);
                H3.SetActive(true);
                Hp = 3;
                level2 = true;
            }
        }
        if (score >= 8)
        {
            WinText.text = "!!YOU WIN!!";
            Hp = 90000;
            H1.SetActive(true);
            H2.SetActive(true);
            H3.SetActive(true);
            BaseMusic.SetActive(false);

            WinMusic.SetActive(true);
        }
    }
    void movePlayer(float Playerspeed)
    {
        if (inAir == false && Playerspeed < 0 || Playerspeed > 0 && inAir == false)
        {
            cat.SetInteger("State", 1);
        }
        if (inAir == false && Playerspeed ==0)
        {
            cat.SetInteger("State", 0);
        }
        rb2d.velocity = new Vector3(speed, rb2d.velocity.y, 0);
    }
    void Flip()
    {
        //flips sprite
        if (speed > 0 && !facingRight || speed < 0 && facingRight)
        {
            //Debug.Log("flipping");
            facingRight = !facingRight;
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        }
    }


        private void OnCollisionStay2D(Collision2D collision)
    {
        if (isAlive == true)
        {
            if (collision.collider.tag == "Ground")
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAlive == true)
        {
            if (collision.collider.tag == "Ground")
            {
                cat.SetInteger("State", 0);
                inAir = false;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Jump controll
        if (Input.GetKey(KeyCode.UpArrow))
        {
            cat.SetInteger("State", 2);
            inAir = true;
        }
}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coins"))
        {
            other.gameObject.SetActive(false);
            score = score + 1;
            ScoreText.text = "x" + score;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            Hp = Hp - 1;
            if (Hp < 3)
            {
                H1.SetActive(false);
            }
            if (Hp < 2)
            {
                H2.SetActive(false);
            }
            if (Hp < 1)
            {
                H3.SetActive(false);
                PlayerDeath();
            }
        }
    }

    void PlayerDeath()
    {
        LoseText.text = "!!GAME OVER!!";
        isAlive = false;
        cat.SetInteger("State", 3);
    }
}
