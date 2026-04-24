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
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);
        highScoreText.text = $"Best: {bestScore}";
    }
}
