using SpawnSystem;
using UnityEngine;

namespace PowerUp
{
    public class ActiveTimedPowerUp : ITickable
    {
        public TimedPowerUpEffect TimedPowerUpEffect  { get; }

        public float RemainingTime { get; private set; }
        
        public bool IsExpired => RemainingTime <= 0;

        public ActiveTimedPowerUp(TimedPowerUpEffect timedPowerUpEffect)
        {
            TimedPowerUpEffect = timedPowerUpEffect;
            RemainingTime = timedPowerUpEffect.Duration;
        }

        public void Tick(float deltaTime)
        {
            RemainingTime -= deltaTime;
        }
    }
}