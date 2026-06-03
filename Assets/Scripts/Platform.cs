using System;
using Game;
using SpawnSystem;
using UnityEngine;
using Random = UnityEngine.Random;

public class Platform : Entity
{
    public event Action<Vector3> PlayerLanded;

    GameObject powerUp;
    bool hasBeenHit;
    
    PlatformData data;
    private Transform player;

    public void Init(PlatformData data, Transform player)
    {
        this.data = data;
    }

    void Start()
    {
        hasBeenHit = false;

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
        if(player == null) return;
        if (player.position.z - transform.position.z > data.DespawnDistance)
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
        if (!collision.gameObject.CompareTag("Player")) return;

        if (hasBeenHit || GameStateManager.Instance.CurrentGameState == GameState.GameOver) return;

        hasBeenHit = true;
        PlayerLanded?.Invoke(transform.position);
    }
}
