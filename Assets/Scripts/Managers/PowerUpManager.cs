using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PowerUpManager : MonoBehaviour
    {
        public static PowerUpManager Instance { get; private set; }

        public event Action OnPowerUpStarted;
        public event Action OnPowerUpEnded;

        public bool hasPowerUp { get; private set; } = false;
        private float powerUpDuration = 0f;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        private void Update()
        {
            UpdatePowerUp();
        }

        private void UpdatePowerUp()
        {
            if (hasPowerUp)
            {
                powerUpDuration -= Time.deltaTime;
                if (powerUpDuration <= 0f)
                {
                    hasPowerUp = false;
                    powerUpDuration = 0f;
                    OnLostPowerUp?.Invoke();
                }
            }
        }
        public void HitPowerUp()
        {
            hasPowerUp = true;
            powerUpDuration += 20f;
            OnPowerUpStarted?.Invoke();
        }

        public void EndPowerUp()
        {
            hasPowerUp = false;
            powerUpDuration = 0f;
            OnPowerUpEnded?.Invoke();
        }
    }
}