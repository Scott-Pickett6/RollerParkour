using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI highScoreText;

    private void OnEnable()
    {
        UpdateBestScore();
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("RollerParkour");
    }
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    private void UpdateBestScore()
    {
        if (GameManager.Instance == null)
        {
            highScoreText.text = "Best Score: 0";
            return;
        }

        long bestScore = GameManager.Instance.LoadBestScore();
        highScoreText.text = $"Best: {bestScore}";
    }
}
