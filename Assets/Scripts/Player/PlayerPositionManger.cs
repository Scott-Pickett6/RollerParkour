using Assets.Scripts.GameManagers;
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
        GameStateManager.Instance.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        GameStateManager.Instance.OnGameOver -= HandleGameOver;
    }

    private void Update()
    {
        CheckPlayerPosition();
    }

    private void CheckPlayerPosition()
    {
        ScoreSystemManager.Instance.UpdateDistance(Mathf.RoundToInt(transform.position.z));
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
        rb.isKinematic = true;
    }
}