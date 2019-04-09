using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loadlevel : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenCotnrols()
    {
        SceneManager.LoadScene(2);
    }
    public void Win()
    {
        SceneManager.LoadScene(3);
    }
    public void Lose()
    {
        SceneManager.LoadScene(4);
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exiting Game");
    }
    public void ResumeGame()
    {
        gameObject.SetActive(false);
        Screen.lockCursor=true;
    }
}
