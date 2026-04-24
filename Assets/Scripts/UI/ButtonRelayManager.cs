using Assets.Scripts.Managers;
using UnityEngine;

public class ButtonRelayManger : MonoBehaviour
{
    public void ClickRestart()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.RestartGame();
        }
    }
    public void ClickMainMenu()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.LoadMainMenu();
        }
    }
}
