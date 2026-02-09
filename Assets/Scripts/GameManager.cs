using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Starting,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static GameState CurrentGameState { get; private set; } = GameState.Starting;

    public event System.Action<int> OnDistanceChanged;
    public event System.Action<string> OnTimerUpdated;
    public event System.Action<long> OnGameOver;

    private float elapsedTime = 0f;
    private int distanceTraveled = 0;
    private bool isTimerRunning = false;

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

    void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            int seconds = Mathf.RoundToInt(elapsedTime);
            OnTimerUpdated?.Invoke(FormatTime(seconds));
        }
    }

    public void StartTimer()
    {
        CurrentGameState = GameState.Playing;
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
        if (CurrentGameState == GameState.GameOver) return;

        isTimerRunning = false;
        long score = CalculateScore(distanceTraveled, elapsedTime);
        SaveBestScore(score);
        OnGameOver?.Invoke(score);
    }

    public void RestartGame()
    {
        CurrentGameState = GameState.Starting;
        isTimerRunning = false;
        elapsedTime = 0f;
        distanceTraveled = 0;
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        CurrentGameState = GameState.Starting;
        isTimerRunning = false;
        elapsedTime = 0f;
        distanceTraveled = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private string FormatTime(int seconds)
    {
        int mins = seconds / 60;
        int secs = seconds % 60;
        return mins.ToString("00") + ":" + secs.ToString("00");
    }

    private long CalculateScore(int distance, float timeInSeconds)
    {
        double distanceScore = System.Math.Pow(distance, 2.5);
        double speedBonus = distance / (timeInSeconds + 1f);

        double totalScore = (distanceScore + speedBonus) * 0.01;

        return (long)totalScore;
    }

    private void SaveBestScore(long score)
    {
        string path = Path.Combine(Application.persistentDataPath, "BestScore.json");
        long bestScore = 0;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            BestScoreData data = JsonUtility.FromJson<BestScoreData>(json);
            bestScore = data.bestScore;
        }
        if (score > bestScore)
        {
            BestScoreData newData = new BestScoreData { bestScore = score };
            string json = JsonUtility.ToJson(newData);
            File.WriteAllText(path, json);
        }
    }
    public long LoadBestScore()
    {
        string path = Path.Combine(Application.persistentDataPath, "BestScore.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            BestScoreData data = JsonUtility.FromJson<BestScoreData>(json);
            return data != null ? data.bestScore : 0;
        }
        return 0;
    }

}

public class BestScoreData
{
    public long bestScore;
}
