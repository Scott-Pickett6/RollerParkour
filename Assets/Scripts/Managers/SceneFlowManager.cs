using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Managers
{
    public class SceneFlowManager : MonoBehaviour
    {
        void OnEnable()
        {
            if (GameStateManager.Instance != null)
            {
                GameStateManager.Instance.OnMainMenuLoaded += LoadMainMenu;
                GameStateManager.Instance.OnGameRestarted += LoadGameScene;
            }
        }

        void OnDisable()
        {
            if (GameStateManager.Instance != null)
            {
                GameStateManager.Instance.OnMainMenuLoaded -= LoadMainMenu;
                GameStateManager.Instance.OnGameRestarted -= LoadGameScene;
            }
        }

        private void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void LoadGameScene()
        {
            SceneManager.LoadScene("RollerParkour");
        }
    }
}