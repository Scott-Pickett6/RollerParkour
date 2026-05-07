using System;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
    
        public event Action<int> OnDistanceChanged;
        public event Action<string> OnTimerUpdated;
        public event Action<long> OnGameOver;

        public event Action<int> OnPowerUpTimerUpdated;
        public event Action<bool> OnPowerUpStatusChanged;
    
        public event Action<GameState> OnGameStateChanged;

        private float elapsedTime;
        private int distanceTraveled;
        private bool isTimerRunning;
        private bool hasPowerUp;
        private float powerUpDuration;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        void OnEnable()
        {
            PlayerInput.OnFirstMove += StartTimer;
        }
        void OnDisable()
        {
            PlayerInput.OnFirstMove -= StartTimer;
        }

        void Update()
        {
            if (isTimerRunning)
            {
                elapsedTime += Time.deltaTime;
                int seconds = Mathf.RoundToInt(elapsedTime);
                OnTimerUpdated?.Invoke(FormatTime(seconds));
            }
            if (hasPowerUp)
            {
                powerUpDuration -= Time.deltaTime;
                if (powerUpDuration <= 0f)
                {
                    hasPowerUp = false;
                    powerUpDuration = 0f;
                    OnPowerUpStatusChanged?.Invoke(false);
                    OnPowerUpTimerUpdated?.Invoke(0);
                }
                else
                {
                    OnPowerUpTimerUpdated?.Invoke(Mathf.RoundToInt(powerUpDuration));
                }
            }
        }

        private void StartTimer()
        {
            OnGameStateChanged?.Invoke(GameState.Playing);
            isTimerRunning = true;
            elapsedTime = 0f;
        }

        public void ReportPlayerDistace(int zDistance)
        {
            distanceTraveled = zDistance;
            OnDistanceChanged?.Invoke(zDistance);
        }

        public void GameOver()
        {
            if (GameStateManager.Instance.CurrentGameState == GameState.GameOver) return;

            isTimerRunning = false;
            int score = GameScoreManager.Instance.CalculateScore(distanceTraveled, elapsedTime);
            hasPowerUp = false;
            GameScoreManager.Instance.SaveBestScore(score);
            OnGameOver?.Invoke(score);
            OnGameStateChanged?.Invoke(GameState.GameOver);
        }

        public void RocketHitPlayer()
        {
            if (hasPowerUp)
            {
                hasPowerUp = false;
                powerUpDuration = 0f;
                OnPowerUpTimerUpdated?.Invoke(0);
                OnPowerUpStatusChanged?.Invoke(false);
            }
            else
            {
                GameOver();
            }
        }

        public void RestartGame()
        {
            OnGameStateChanged?.Invoke(GameState.Starting);
            isTimerRunning = false;
            elapsedTime = 0f;
            distanceTraveled = 0;
            powerUpDuration = 0f;
            hasPowerUp = false;
            Time.timeScale = 1f;

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoadMainMenu()
        {
            OnGameStateChanged?.Invoke(GameState.Starting);
            isTimerRunning = false;
            elapsedTime = 0f;
            hasPowerUp= false;
            powerUpDuration = 0f;
            distanceTraveled = 0;
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }

        public void HitPowerUp()
        {
            hasPowerUp = true;
            powerUpDuration += 20f;
            OnPowerUpStatusChanged?.Invoke(true);
        }

        private string FormatTime(int seconds)
        {
            int mins = seconds / 60;
            int secs = seconds % 60;
            return mins.ToString("00") + ":" + secs.ToString("00");
        }
    }
}
