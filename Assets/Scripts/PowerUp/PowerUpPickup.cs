using System;
using Entities;
using Player;
using UnityEngine;

namespace PowerUp
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
        }
    }
}