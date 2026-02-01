using UnityEngine;

public class RestartRelay : MonoBehaviour
{
    public void ClickRestart()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame();
        }
    }
}