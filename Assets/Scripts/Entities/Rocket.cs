using System;
using Events;
using Game;
using Player;
using PowerUp;
using UnityEngine;

namespace Entities
{
    public class Rocket : Entity
    {
        [SerializeField]
        DestroyAllEnemiesEventChannel destroyAllEnemiesEventChannel;
        
        [SerializeField]
        float speed;
        [SerializeField]
        GameObject explosionEffectPrefab;
        [SerializeField]
        float destroyDistance;
    
        Rigidbody rb;
        Vector3 startingPos;

        void Start()
        {
            startingPos = transform.position;
        }

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void OnEnable()
        {
            destroyAllEnemiesEventChannel.OnEventRaised += HandleDestroyAllEnemiesEvent;
        }
        
         void OnDisable()
        {
            destroyAllEnemiesEventChannel.OnEventRaised -= HandleDestroyAllEnemiesEvent;
        }

        void Update()
        {
            if (DetectOffMap())
            {
                Destroy(gameObject);
            }
            if (GameStateManager.Instance.CurrentGameState == GameState.GameOver)
            {
                Destroy(gameObject);
            }
        }
    
        void FixedUpdate()
        {
            rb.velocity = -transform.right * speed;
        }

        void OnCollisionEnter(Collision collision)
        {
            PlayerPowerUpManager powerUpManager = collision.gameObject.GetComponent<PlayerPowerUpManager>();
            if (powerUpManager != null && !powerUpManager.ConsumedSheild())
            {
                GameManager.Instance.GameOver();
            }
            TriggerExplosion();
            Destroy(gameObject);
        }

        void HandleDestroyAllEnemiesEvent()
        {
            TriggerExplosion();
            Destroy();
        }

        void TriggerExplosion()
        {
            if (explosionEffectPrefab != null)
            {
                GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

                Destroy(explosion, 5f);
            }
        }

        bool DetectOffMap()
        {
            if (Mathf.Abs(transform.position.x - startingPos.x) > destroyDistance)
            {
                return true;
            }
            return false;
        }

        void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
