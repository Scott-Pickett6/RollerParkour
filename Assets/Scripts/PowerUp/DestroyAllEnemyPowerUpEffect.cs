using System;
using UnityEngine;

namespace PowerUp
{
    [CreateAssetMenu(fileName = "DestroyAllEnemyPowerUpEffect", menuName = "PowerUp Effects/SingleTimePowerUpEffects/DestroyAllEnemyPowerUpEffect")]
    public class DestroyAllEnemyPowerUpEffect : SingleTimePowerUpEffect
    {
        public static event Action OnDestroyAllEnemies;
        public override void ApplyPowerUp(GameObject target)
        {
            OnDestroyAllEnemies?.Invoke();
        }
    }
}