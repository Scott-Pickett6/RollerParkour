using System;
using Game;
using PowerUp;
using SpawnSystem;
using UnityEngine;

namespace Entities
{
    public class Platform : Entity
    {
        public event Action<Vector3> PlayerLanded;
        bool hasBeenHit;
    
        PlatformData data;
        Transform player;
    
        public PowerUpSpawnPoint[] Points { get; private set; }

        public void Init(PlatformData data, Transform player)
        {
            this.data = data;
            this.player = player;
        }

        void Awake()
        {
            Points = GetComponentsInChildren<PowerUpSpawnPoint>();
        }

        void Start()
        {
            hasBeenHit = false;
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
}
