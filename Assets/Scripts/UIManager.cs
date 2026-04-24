using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnDistanceChanged += UpdateDistance;
            GameManager.Instance.OnTimerUpdated += UpdateTimer;
            GameManager.Instance.OnGameOver += HandleGameOver;
            GameManager.Instance.OnPowerUpTimerUpdated += UpdatePowerUpTimer;
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnDistanceChanged -= UpdateDistance;
            GameManager.Instance.OnTimerUpdated -= UpdateTimer;
            GameManager.Instance.OnGameOver -= HandleGameOver;
            GameManager.Instance.OnPowerUpTimerUpdated -= UpdatePowerUpTimer;
        }
    }

    private void UpdateTimer(string time)
    {
        timerText.text = "Time: " + time;
    }

    private void UpdateDistance(int zDistance)
    {
        distanceText.text = "Distance: " + zDistance + " m";
    }

    private void HandleGameOver(long score)
    {
        scoreText.gameObject.SetActive(true);
        scoreText.text = "Final Score: " + score;
        restartButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
    }

    private void UpdatePowerUpTimer(int secondsLeft)
    {
       powerUpTimer.text = "Power Up Time: " + secondsLeft;
    }
}
