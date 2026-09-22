using System;
using PowerUp;
using UnityEngine;

namespace Player
{
    public class PlayerPowerUpManager : MonoBehaviour
    {
        public bool HasSheild { get; set;}
        public ActiveTimedPowerUp ActiveTimedPowerUp { get; private set;}
        
        public void ApplyPowerUp(PowerUpEffect powerUpEffect)
        {
            if (powerUpEffect is TimedPowerUpEffect timedPowerUpEffect)
            {
                ApplyTimedPowerUp(timedPowerUpEffect);
            }
            powerUpEffect.ApplyPowerUp(gameObject);
        }

        public bool ConsumedSheild()
        {
            if (!HasSheild) return false;
            
            HasSheild = false;
            if (ActiveTimedPowerUp != null && ActiveTimedPowerUp.TimedPowerUpEffect is ShieldPowerUpEffect shieldPowerUpEffect)
            {
                ActiveTimedPowerUp = null;
                return true;
            }
            return false;
        }

        void ApplyTimedPowerUp(TimedPowerUpEffect timedPowerUpEffect)
        {
            ActiveTimedPowerUp = new ActiveTimedPowerUp(timedPowerUpEffect);
        }
        
        void Update()
        {
            if (ActiveTimedPowerUp == null) return;
            
            ActiveTimedPowerUp.Tick(Time.deltaTime);

            if (!ActiveTimedPowerUp.IsExpired) return;
            
            ActiveTimedPowerUp.TimedPowerUpEffect.RemovePowerUp(gameObject);
            ActiveTimedPowerUp = null;
        }
    }
}