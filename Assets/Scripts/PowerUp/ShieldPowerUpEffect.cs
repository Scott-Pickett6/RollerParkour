using Player;
using UnityEngine;

namespace PowerUp
{
    [CreateAssetMenu(fileName = "ShieldPowerUpEffect", menuName = "PowerUp Effects/TimedPowerUpEffect/ShieldPowerUpEffect")]
    public class ShieldPowerUpEffect : TimedPowerUpEffect
    {
        public override void ApplyPowerUp(GameObject target)
        {
            target.GetComponent<PlayerPowerUpManager>().HasSheild = true;
        }

        public override void RemovePowerUp(GameObject target)
        {
            target.GetComponent<PlayerPowerUpManager>().HasSheild = false;
        }
    }
}