using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class ScoreSystemManager : MonoBehaviour
    {
        public static ScoreSystemManager Instance { get; private set; }

        private float elapsedTime = 0f;
        private int distanceTraveled = 0;
        private bool isTimerRunning = false;

        public int DistanceTraveled => distanceTraveled;
        public int ElapsedSeconds => Mathf.FloorToInt(elapsedTime);

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        private void OnEnable()
        {
            if (GameStateManager.Instance != null)
            {
                GameStateManager.Instance.OnGameStarted += StartTimer;
                GameStateManager.Instance.OnGameOver += StopTimer;
            }
        }

        private void OnDisable()
        {
            if (GameStateManager.Instance != null)
            {
                GameStateManager.Instance.OnGameStarted -= StartTimer;
                GameStateManager.Instance.OnGameOver -= StopTimer;
            }
        }

        private void Update()
        {
            UpdateElapsedTime();
        }

        private void UpdateElapsedTime()
        {
            if (isTimerRunning)
            {
                elapsedTime += Time.deltaTime;
            }
        }

        private void StartTimer()
        {
            elapsedTime = 0f;
            isTimerRunning = true;
        }

        private void StopTimer()
        {
            isTimerRunning = false;
        }

        public void UpdateDistance(int distance)
        {
            distanceTraveled = distance;
        }

        public int LoadBestScore()
        {
            if (PlayerPrefs.HasKey("BestScore"))
            {
                return PlayerPrefs.GetInt("BestScore");
            }

            return 0;
        }

        public int CalculateScore()
        {
            double distanceScore = System.Math.Pow(distanceTraveled, 2.5);
            double speedBonus = distanceTraveled / (elapsedTime + 1f);

            double totalScore = (distanceScore + speedBonus) * 0.01;

            return Mathf.Max(0, Mathf.RoundToInt((float)totalScore));
        }

        public void SaveBestScore(int score)
        {
            int bestScore = 0;
            if (PlayerPrefs.HasKey("BestScore"))
            {
                bestScore = PlayerPrefs.GetInt("BestScore");
            }

            if (score > bestScore)
            {
                PlayerPrefs.SetInt("BestScore", score);
            }
        }
    }
}
