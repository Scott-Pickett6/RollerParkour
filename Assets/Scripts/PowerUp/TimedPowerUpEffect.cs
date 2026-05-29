using System;
using System.Collections;
using UnityEngine;

namespace PowerUp
{
    public abstract class TimedPowerUpEffect : PowerUpEffect
    {
        [SerializeField] 
        float duration = 20;
        
        public abstract void RemovePowerUp(GameObject target);

        public IEnumerator StartTimedPowerUp(GameObject target)
        {
            yield return new WaitForSeconds(duration);
            RemovePowerUp(target);
        }
    }
}