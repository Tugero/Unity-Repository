using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private int score;
    private int lives;
    private bool isonlevel2 = false;
    private Vector3 level2 = new Vector3(0, (float)-99.5, 0);
    public float speed;
    public Text countText;
    public Text scoreText;
    public Text livesText;
    public Text winText;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;
    public GameObject Reset;
    public GameObject Quit;
    public Text loseText;
    public GameObject Boom;
    public GameObject BadBoom;
   

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        score = 0;
        lives = 3;
        setCountText();
        winText.text = "";
        loseText.text = "";
    }
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 Movement = new Vector3(moveHorizontal,0.0f,moveVertical);
        rb.AddForce(Movement * speed);
    }
    private void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();
        setLives();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count+1;
            score = score+1;
            setCountText();
        }

        //Damage Code!
        if (other.gameObject.CompareTag("Enemy"))
        {
            lives = lives - 1;
            score = score - 3;
            Debug.Log("Lives deducted!");
            setLives();
           

        }
        if (count >= 1)
        {
            Enemy1.gameObject.SetActive(true);
        }
        if (count >= 4)
        {
            Enemy2.gameObject.SetActive(true);
        }
        if (count >= 7)
        {
            Enemy3.gameObject.SetActive(true);
        }
        if (count >= 10)
        {
            Enemy4.gameObject.SetActive(true);
        }

    }

    void setCountText()
    {
        countText.text = "COLLECTED: " + count.ToString();
        scoreText.text = "SCORE: " + score.ToString();
        if (count >= 12)
        {
            Enemy1.gameObject.SetActive(false);
            Enemy2.gameObject.SetActive(false);
            Enemy3.gameObject.SetActive(false);
            Enemy4.gameObject.SetActive(false);
            while (isonlevel2 == false) { 
            gameObject.transform.position = level2;
                isonlevel2 = true;
         }
            if (count >= 22)
            {
                if (score < 22)
                {
                    Reset.gameObject.SetActive(true);
                    Quit.gameObject.SetActive(true);
                    winText.text = "!!YOU SURVIVED!!";
                    Instantiate(Boom, gameObject.transform.position, gameObject.transform.rotation);
                    gameObject.SetActive(false);
                }
                else
                {
                    Reset.gameObject.SetActive(true);
                    Quit.gameObject.SetActive(true);
                    winText.text = "!!YOU WIN!!";
                    Instantiate(Boom, gameObject.transform.position, gameObject.transform.rotation);
                    gameObject.SetActive(false);
                }

            }

        }
    }

    void setLives()
    {
        if (lives == 2)
        {
            livesText.text = "LIVES: X X";
        }
        else if (lives == 1)
        {
            livesText.text = "LIVES: X";
        }
        else if (lives < 1)
        {
            livesText.text = "LIVES:";
            gameObject.SetActive(false);
            Reset.gameObject.SetActive(true);
            Quit.gameObject.SetActive(true);
            loseText.text = "!!GAME OVER!!";
            Instantiate(BadBoom, gameObject.transform.position, gameObject.transform.rotation);
        }
    }

}
