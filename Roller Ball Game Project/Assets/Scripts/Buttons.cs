using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Application Terminated!");
       Application.Quit();
    }

    public void ResetGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
