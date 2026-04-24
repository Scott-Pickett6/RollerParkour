using Assets.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private TextMeshProUGUI distanceText;

    [SerializeField]
    private TextMeshProUGUI powerUpTimer;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private Button restartButton;

    [SerializeField]
    private Button mainMenuButton;

    private ScoreSystemManager scoreSystemManager;
    private PowerUpManager powerUpManager;
    private GameStateManager gameStateManager;

    private void Start()
    {
        scoreSystemManager = ScoreSystemManager.Instance;
        powerUpManager = PowerUpManager.Instance;
        gameStateManager = GameStateManager.Instance;

        if (gameStateManager != null)
        {
            gameStateManager.OnGameOver += HandleGameOver;
        }

        HandleGameStarted();
    }

    private void Update()
    {
        if (scoreSystemManager == null)
        {
            scoreSystemManager = ScoreSystemManager.Instance;
        }

        if (powerUpManager == null)
        {
            powerUpManager = PowerUpManager.Instance;
        }

        if (gameStateManager == null && GameStateManager.Instance != null)
        {
            gameStateManager = GameStateManager.Instance;
            gameStateManager.OnGameOver += HandleGameOver;
        }

        if (scoreSystemManager != null)
        {
            UpdateDistance(scoreSystemManager.DistanceTraveled);
            UpdateTimer(FormatTime(scoreSystemManager.ElapsedSeconds));
        }

        if (powerUpManager != null && powerUpManager.hasPowerUp)
        {
            UpdatePowerUpTimer(powerUpManager.RemainingSeconds);
        }
        else if (powerUpTimer != null)
        {
            powerUpTimer.text = string.Empty;
        }
    }

    private void OnDestroy()
    {
        if (gameStateManager != null)
        {
            gameStateManager.OnGameOver -= HandleGameOver;
        }
    }

    private void UpdateTimer(string time)
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + time;
        }
    }

    private void UpdateDistance(int zDistance)
    {
        if (distanceText != null)
        {
            distanceText.text = "Distance: " + zDistance + " m";
        }
    }

    private void HandleGameOver()
    {
        int score = 0;
        if (scoreSystemManager != null)
        {
            score = scoreSystemManager.CalculateScore();
            scoreSystemManager.SaveBestScore(score);
        }

        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(true);
            scoreText.text = "Final Score: " + score;
        }

        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.gameObject.SetActive(true);
        }
    }

    private void UpdatePowerUpTimer(int secondsLeft)
    {
        if (powerUpTimer != null)
        {
            powerUpTimer.text = "Power Up Time: " + secondsLeft;
        }
    }

    private void HandleGameStarted()
    {
        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(false);
        }

        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(false);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.gameObject.SetActive(false);
        }
    }

    private string FormatTime(int seconds)
    {
        int mins = seconds / 60;
        int secs = seconds % 60;
        return mins.ToString("00") + ":" + secs.ToString("00");
    }
}
