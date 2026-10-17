using UnityEngine;

namespace PowerUp
{
    public abstract class TimedPowerUpEffect : PowerUpEffect
    {
        [SerializeField] 
        float duration = 20;
        
        public float Duration => duration;
        
        public abstract void RemovePowerUp(GameObject target);
    }
}