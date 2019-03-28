using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject Hazards;
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
        StartCoroutine(spawnWaves());
        Score = 0;
        UpdateScore();
    }

    private void Update()
    {
            if (Input.GetKey(KeyCode.R) && restart)
            SceneManager.LoadScene("Space Shooter Tutorial Followed Game");

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Leaving so soon?");
        }
    }
    IEnumerator spawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 SpawnPosition = new Vector3(Random.Range(-SpawnValues.x, SpawnValues.x), SpawnValues.y, SpawnValues.z);
                Quaternion SpawnRotation = Quaternion.identity;
                Instantiate(Hazards, SpawnPosition, SpawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            if (gameOver)
                RestartText.text = "Hello There! Seems you've died. That's fine, just press [R] to restart! :D";
            restart = true;
            break;
        }
    }

    void UpdateScore()
    {
        ScoreText.text = "Score "+ Score;
    }

    public void AddScore(int newScoreValue)
    {
        Score += newScoreValue;
        UpdateScore();
    }

    public void GameOver()
    {
        GameOverText.text = "!!Game Over!!";
        gameOver = true;
    }
}
