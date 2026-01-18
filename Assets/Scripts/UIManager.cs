using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private TextMeshProUGUI distanceText;

    private void OnEnable()
    {
        GameManager.Instance.OnDistanceChanged += UpdateDistance;
        GameManager.Instance.OnTimerUpdated += UpdateTimer;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnDistanceChanged -= UpdateDistance;
        GameManager.Instance.OnTimerUpdated -= UpdateTimer;
    }

    public void UpdateTimer(string time)
    {
        timerText.text = "Time: " + time;
    }
    public void UpdateDistance(int zDistance)
    {
        distanceText.text = "Distance: " + zDistance + " m";
    }
}
