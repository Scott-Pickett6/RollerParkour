using Assets.Scripts.Managers;
using UnityEngine;

public class PlayerPositionManger : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.OnGameOver += HandleGameOver;
        }
    }

    private void OnDisable()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.OnGameOver -= HandleGameOver;
        }
    }

    private void Update()
    {
        CheckPlayerPosition();
    }

    private void CheckPlayerPosition()
    {
        if (ScoreSystemManager.Instance != null)
        {
            ScoreSystemManager.Instance.UpdateDistance(Mathf.RoundToInt(transform.position.z));
        }

        CheckIfFallen();
    }

    private void CheckIfFallen()
    {
        if (transform.position.y < -10f)
        {
            GameStateManager.Instance.GameOver();
        }
    }

    private void HandleGameOver()
    {
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }
}
