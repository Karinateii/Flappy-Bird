using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;
    public GameObject startGameButton;
    public GameObject quitGameButton;
    public GameObject pauseMenu;
    public GameObject resumeButton;
    //public GameObject pipes;

    private PipeMoveScreen[] pipeMovements;
    private bool gameStarted = false;
    private bool isPaused = false;

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore = playerScore + scoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        StopPipeMovement();
        Time.timeScale = 0;
    }

    public void startGame()
    {
        gameStarted = true;
        Time.timeScale = 1;
        isPaused = false;
        startGameButton.SetActive(false);
        quitGameButton.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void pauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void resumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    void StopPipeMovement()
    {
        foreach (PipeMoveScreen pipeMovement in pipeMovements)
        {
            pipeMovement.enabled = false;
        }
    }

    void Start()
    {
        // Pause the game at the start
        Time.timeScale = 0;
        startGameButton.SetActive(true);
        quitGameButton.SetActive(true);
        pauseMenu.SetActive(false); // Ensure pause menu is hidden at the start

        // Find all PipeMoveSreen scripts
        pipeMovements = FindObjectsOfType<PipeMoveScreen>();
    }

    void Update()
    {
        if (!gameStarted)
        {
            // Ensure the game is paused if it hasn't started
            Time.timeScale = 0;
        }

        if (gameStarted && Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }

    }
}
