using Assets.Scripts.Interfaces;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerCollisionHandler : MonoBehaviour, IRocketHittable
    {
        public void OnRocketHit()
        {
            if (PowerUpManager.Instance != null && PowerUpManager.Instance.hasPowerUp)
            {
                PowerUpManager.Instance.EndPowerUp();
            }
            else if (GameStateManager.Instance != null)
            {
                GameStateManager.Instance.GameOver();
            }
        }
    }
}
