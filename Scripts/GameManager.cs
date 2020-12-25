using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    int currentScore, highScore = 0;

    [SerializeField]
    TextMeshProUGUI currentScoreText, currentScoreGameOver, highScoreText;

    [SerializeField]
    GameObject menuPanel, gameOverPanel, gamePanel, sceneSwitchingPanel;

    [SerializeField]
    float timeAvailable = 5;

    float timeRemaining;

    [SerializeField]
    Slider timeSlider;

    [SerializeField]
    float timeIncreaseOnTap = 0.2f;

    [SerializeField]
    TimeSliderHighlighter sliderHighlight;

    bool gameOver = false, gameStarted = false, gamePaused = false;


    [SerializeField]
    Image pauseButton;
    [SerializeField]
    Sprite notPaused, paused;

    [SerializeField]
    GameObject tapLeftButton, tapRightButton;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = this;

        ResetVariables();
    }

    // Update is called once per frame
    void Update()
    {
        TimePassing();
    }

    void TimePassing()
    {
        if (gameStarted && !gamePaused)
        {
            timeRemaining -= Time.deltaTime;
            timeSlider.value = timeRemaining;
            if (timeRemaining <= 0)
            {
                OnGameOver();
            }
        }
    }

    public void TimeIncreaseAfterTap()
    {
        if (timeRemaining + timeIncreaseOnTap < timeAvailable)
        {
            timeRemaining += timeIncreaseOnTap;

        }
        else
        {
            timeRemaining = timeAvailable;
        }

    }

    public void OnScoreIncrease()
    {
        currentScore++;
        UpdateCurrentScore();
        TimeIncreaseAfterTap();
        sliderHighlight.OnNewScore();

    }

    public void ResetVariables()
    {
        gameOver = false;
        gameStarted = false;
        gamePaused = false;
        pauseButton.sprite = notPaused;
        currentScore = 0;
        timeRemaining = timeAvailable;
        timeSlider.maxValue = timeAvailable;
        timeSlider.value = timeAvailable;
        UpdateCurrentScore();
    }

    void UpdateCurrentScore()
    {
        currentScoreText.text = currentScore.ToString();
        currentScoreGameOver.text = currentScore.ToString();
    }

    public void OnPlayAgainBtn()
    {
        ResetVariables();
        gameOverPanel.SetActive(false);
        menuPanel.SetActive(false);

        sceneSwitchingPanel.SetActive(true);
        Invoke("ActivateGamePanel", 0.3f);
        Invoke("CreateNewList", 0.3f);
    }

    void CreateNewList()
    {
        TrunkSpawner.trunkSpawner.SpawnNewList();
        PlayerController.playerController.ResetPlayer();
    }

    public void OnGoHomeBtn()
    {
        ResetVariables();
        sceneSwitchingPanel.SetActive(true);
        Invoke("DisableSceneSwitching", 0.5f);
        menuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        PlayerController.playerController.ResetPlayer();

    }

    void DisableSceneSwitching()
    {
        sceneSwitchingPanel.SetActive(false);

    }

    public void OnGameOver()
    {
        gameOver = true;
        gameStarted = false;
        if (highScore < currentScore)
        {
            highScore = currentScore;
            highScoreText.text = highScore.ToString();
        }
        sceneSwitchingPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        gamePanel.SetActive(false);
        ActivateTapButtons();
    }

    void ActivateGamePanel()
    {
        gamePanel.SetActive(true);
    }

    void ActivateTapButtons()
    {
        tapLeftButton.SetActive(true);
        tapRightButton.SetActive(true);
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void GameHasStarted()
    {
        gameStarted = true;
    }

    public bool IsGameStarted()
    {
        return gameStarted;
    }

    public bool IsGamePaused()
    {
        return gamePaused;
    }
    public void OnPauseGame()
    {
        if (!gameOver)
        {
            gamePaused = !gamePaused;
            if (gamePaused)
            {
                pauseButton.sprite = paused;
            }
            else
            {
                pauseButton.sprite = notPaused;
            }
        }

    }
}
