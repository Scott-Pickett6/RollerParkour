using Player;
using UnityEngine;

namespace PowerUp
{
    [CreateAssetMenu(fileName = "ShieldPowerUpEffect", menuName = "PowerUp Effects/TimedPowerUpEffect/ShieldPowerUpEffect")]
    public class ShieldPowerUpEffect : TimedPowerUpEffect
    {
        public override void ApplyPowerUp(GameObject target)
        {
            PlayerController playerController = target.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.HasSheild = true;
            }
        }

        public override void RemovePowerUp(GameObject target)
        {
            PlayerController playerController = target.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.HasSheild = false;
            }
        }
    }
}