using Game;
using TMPro;
using UnityEngine;

namespace UI
{
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

            long bestScore = GameScoreManager.Instance.LoadBestScore();
            highScoreText.text = $"Best: {bestScore}";
        }
    }
}
