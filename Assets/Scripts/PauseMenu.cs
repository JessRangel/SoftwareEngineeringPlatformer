using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void ResumeButton()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>().ResumeGame();
    }

    public void QuitToMenuButton()
    {
        SceneManager.LoadScene(0); // 0 refers to the Title Screen
    }
}
