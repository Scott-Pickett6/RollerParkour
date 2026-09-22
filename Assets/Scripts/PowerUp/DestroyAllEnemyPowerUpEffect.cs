using Events;
using UnityEngine;

namespace PowerUp
{
    [CreateAssetMenu(fileName = "DestroyAllEnemyPowerUpEffect", menuName = "PowerUp Effects/SingleTimePowerUpEffects/DestroyAllEnemyPowerUpEffect")]
    public class DestroyAllEnemyPowerUpEffect : SingleTimePowerUpEffect
    {
        [SerializeField]
        DestroyAllEnemiesEventChannel destroyAllEnemiesEventChannel;
        public override void ApplyPowerUp(GameObject target)
        {
            destroyAllEnemiesEventChannel.RaiseEvent();
        }
    }
}