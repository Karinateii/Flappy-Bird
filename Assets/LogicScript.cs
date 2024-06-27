using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    // Variables for UI elements and game state
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;
    public GameObject startGameButton;
    public GameObject quitGameButton;
    public GameObject pauseMenu;
    public GameObject resumeButton;

    private PipeMoveScreen[] pipeMovements;
    private bool gameStarted = false;
    private bool isPaused = false;

    // Method to increase the player's score
    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore = playerScore + scoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    // Method to restart the game
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Method to handle game over state
    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        StopPipeMovement();
        Time.timeScale = 0;
    }

    // Method to start the game
    public void startGame()
    {
        gameStarted = true;
        Time.timeScale = 1;
        isPaused = false;
        startGameButton.SetActive(false);
        quitGameButton.SetActive(false);
        pauseMenu.SetActive(false);
    }

    // Method to quit the game
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Method to pause the game
    public void pauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    // Method to resume the game
    public void resumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    // Method to stop pipe movement
    void StopPipeMovement()
    {
        foreach (PipeMoveScreen pipeMovement in pipeMovements)
        {
            pipeMovement.enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Pause the game at the start
        Time.timeScale = 0;
        startGameButton.SetActive(true);
        quitGameButton.SetActive(true);
        pauseMenu.SetActive(false); // Ensure pause menu is hidden at the start

        // Find all PipeMoveScreen scripts
        pipeMovements = FindObjectsOfType<PipeMoveScreen>();
    }

    // Update is called once per frame
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
