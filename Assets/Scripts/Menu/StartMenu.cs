using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartButton()
    {
        Debug.Log("Start button pressed");
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Debug.Log("Quit button pressed");
        Application.Quit();
    }
}
