using UnityEngine;

namespace PowerUp
{
    public abstract class PowerUpEffect : ScriptableObject
    {
        public abstract void ApplyPowerUp(GameObject target);
    }
}