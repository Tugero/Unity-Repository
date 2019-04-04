using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] Hazards;
    public Vector3 SpawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text ScoreText;
    public Text RestartText;
    public Text GameOverText;
    private bool gameOver;
    private bool restart;
    private int Score;

    private void Start()
    {
        GameOverText.text = "";
        gameOver = false;
        RestartText.text = "";
        restart = false;
        StartCoroutine(SpawnWaves());
        Score = 0;
        UpdateScore();
    }

    private void Update()
    {
            if (Input.GetKey(KeyCode.X) && restart)
            SceneManager.LoadScene("Space Shooter Tutorial Followed Game");

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Leaving so soon?");
        }
    }
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject Hazard = Hazards[Random.Range(0,Hazards.Length)];
                Vector3 SpawnPosition = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
                Quaternion SpawnRotation = Quaternion.identity;
                Instantiate(Hazard, SpawnPosition, SpawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
            {
               //RestartText.text = "Hello There! Seems you've died. That's fine, just press [R] to restart! :D";
                restart = true;
                break;
            }
        }
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: "+ Score;
    }

    public void AddScore(int newScoreValue)
    {
        Score += newScoreValue;
        UpdateScore();
        if (Score == 100)
        {
            GameOverText.text = "Game Created By Daniel Cortell";
            RestartText.text = "Looks Like you've won! Congratulations! Press [X] if you want to go again!";
            gameOver = true;
        }
    }

    public void GameOver()
    {
        GameOverText.text = "!!Game Over!!";
        RestartText.text = "Hello There! Seems you've died. That's fine, just press [X] to restart!";
        gameOver = true;
    }
}
