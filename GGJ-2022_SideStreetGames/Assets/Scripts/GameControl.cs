using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public GameObject PauseUI;
    public GameObject GameOverUI;

    private bool gamePaused;
    public static bool isAlive;

    private void Start()
    {
        isAlive = true;
        Time.timeScale = 1f;
    }

    void Update()
    {

        if (isAlive)
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
        else
            GameOver();
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

    public void GameOver()
    {
        GameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    

    public void ReturntoTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("TestAI");
    }

}

