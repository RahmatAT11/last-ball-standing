using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// This class is for updating the ui
// and managing win or lose for the player

public class GameManager : MonoBehaviour
{
    private SpawnManager spawnManager;
    private GameObject player;

    public TextMeshProUGUI totalBallsRemain;
    public TextMeshProUGUI waveNumText;
    public TextMeshProUGUI gameOverText;

    public Button restartButton;

    public GameObject gameOverUI;
    public GameObject pauseMenu;

    public bool isGameOver;

    public static bool isGamePaused;

    private void Awake()
    {
        spawnManager = GameObject.FindObjectOfType<SpawnManager>().GetComponent<SpawnManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        PauseGameInput();
    }

    public void updateBallsRemain(int ballsRemain)
    {
        totalBallsRemain.text = "Total Balls Remain: " + ballsRemain;
    }

    public void updateWaveNum(int waveNum)
    {
        waveNumText.text = "Wave: " + waveNum;
    }

    public void GameOver(int gameOverType)
    {
        gameOverUI.SetActive(true);

        if (gameOverType == 1)
        {
            gameOverText.text = "You Win!";
            gameOverText.color = new Color(0, 255, 0);
        }
        else
        {
            gameOverText.text = "You Lose!";
            gameOverText.color = new Color(255, 0, 0);
        }

        Time.timeScale = 0.0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MyGame");
    }

    public void StartMenu()
    {
        SceneManager.LoadScene("StartGame");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void PauseGameInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isGamePaused = !isGamePaused;
            PauseGame();
        }
    }

    void PauseGame()
    {
        if (isGamePaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        isGamePaused = !isGamePaused;
        PauseGame();
    }
}
