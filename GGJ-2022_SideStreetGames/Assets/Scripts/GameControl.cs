using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject PauseUI;
    public GameObject GameOverUI;

    private bool gamePaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                ResumeGame();
            }
            else
                PauseGame();
        }
    }

    //Methods

    public void PauseGame()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void ResumeGame()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

}

