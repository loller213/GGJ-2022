using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [SerializeField] private Animator PauseAnimator;

    public GameObject PauseUI;
    public GameObject GameOverUI;
    public float timeCount = 5;

    private float count;
    private bool gamePaused;
    private bool isCounting;
    public static bool isAlive;


    private void Start()
    {
        count = timeCount;
        isAlive = true;
        Time.timeScale = 1f;
        isCounting = false;
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

            if (isCounting)
            {
                count -= 1f;
                count = timeCount;
            }
            else
                return;
        }
        else
            GameOver();


    }

    //Methods

    public void PauseGame()
    {
        PauseUI.SetActive(true);
        gamePaused = true;
        Time.timeScale = 0f;
        if (PauseUI)
        {
            PauseAnimator.Play("SlideIn");
        }
    }

    public void ResumeGame()
    {
        isCounting = true;
        PauseAnimator.Play("SlideOut");
        Time.timeScale = 1f;
        gamePaused = false;
        
        if (count <= 0)
        {
            PauseUI.SetActive(false);
            isCounting = false;
        }

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

