using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event System.Action<int> OnDistanceChanged;
    public event System.Action<string> OnTimerUpdated;

    private float elapsedTime = 0f;
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

    void Start()
    {

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
        isTimerRunning = true;
        elapsedTime = 0f;
    }

    public void ReportPlayerDistace(int zDistance)
    {
        OnDistanceChanged?.Invoke(zDistance);
    }

    private string FormatTime(int seconds)
    {
        int mins = seconds / 60;
        int secs = seconds % 60;
        return mins.ToString("00") + ":" + secs.ToString("00");
    }
}
