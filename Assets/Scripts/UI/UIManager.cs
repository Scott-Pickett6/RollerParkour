using Game;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
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
    
        [SerializeField]
        private PlayerPowerUpManager playerPowerUpManager;
        private int lastDisplayedPowerUpSec = -1;

        private void Start()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnDistanceChanged += UpdateDistance;
                GameManager.Instance.OnTimerUpdated += UpdateTimer;
                GameManager.Instance.OnGameOver += HandleGameOver;
            }
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnDistanceChanged -= UpdateDistance;
                GameManager.Instance.OnTimerUpdated -= UpdateTimer;
                GameManager.Instance.OnGameOver -= HandleGameOver;
            }
        }

        private void Update()
        {
            UpdatePowerUpDisplay();
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

        private void UpdatePowerUpDisplay()
        {
            if (playerPowerUpManager.ActiveTimedPowerUp == null || playerPowerUpManager.ActiveTimedPowerUp.IsExpired)
            {
                lastDisplayedPowerUpSec = -1;
                powerUpTimer.text = "";
                return;
            }
            int secondsLeft = Mathf.CeilToInt(playerPowerUpManager.ActiveTimedPowerUp.RemainingTime);
            if (secondsLeft == lastDisplayedPowerUpSec) return;
            lastDisplayedPowerUpSec = secondsLeft;
            powerUpTimer.text = "Power Up: " + secondsLeft;
        }
    }
}
