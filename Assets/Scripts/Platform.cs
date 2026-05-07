using System;
using Game;
using UnityEngine;
using Random = UnityEngine.Random;

public class Platform : Entity
{
    public event Action<Vector3> PlayerLanded;

    GameObject player;
    GameObject powerUp;
    bool hasBeenHit;

    void Start()
    {
        hasBeenHit = false;
        player = GameObject.FindGameObjectWithTag("Player");

        Transform powerUpTransform = transform.Find("Power_Up");
        if (powerUpTransform != null)
        {
            powerUp = powerUpTransform.gameObject;

            float powerUpChance = Random.value;
            if (powerUpChance < 0.8f)
            {
                powerUp.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (player != null && player.transform.position.z - transform.position.z > 7f)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        PlayerLanded = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (hasBeenHit || GameStateManager.Instance.CurrentGameState == GameState.GameOver)
        {
            return;
        }

        hasBeenHit = true;
        PlayerLanded?.Invoke(transform.position);
    }
}
