using System;
using Entities;
using Player;
using PowerUp;
using UnityEngine;

namespace Entities
{
    public class PowerUpPickup : Entity
    {
        [SerializeField]
        PowerUpEffect powerUpEffect;
        
        void OnTriggerEnter(Collider other)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player == null) return;
            ActivatePowerUp(player);
        }

        void ActivatePowerUp(PlayerController player)
        {
            player.ApplyPowerUpEffect(powerUpEffect);
            Destroy(gameObject);
            Destroy(transform.parent.gameObject);
        }
    }
}