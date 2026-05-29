using Game;
using PowerUp;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(IInput))]
    [RequireComponent(typeof(IMovement))]
    [RequireComponent(typeof(IOffMapDetector))]
    public class PlayerController : Entity
    {
        IInput playerInput;
        IMovement playerMovement;
        IOffMapDetector playerFallDetector;

        bool jumpRequested;
        public bool HasSheild { get; set; } = false;
        
        void Awake()
        {
            playerInput = GetComponent<IInput>();
            playerMovement = GetComponent<IMovement>();
            playerFallDetector = GetComponent<IOffMapDetector>();
        }

        void OnEnable()
        {
            playerFallDetector.OnOffMap += HandlePlayerFell;
        }

        private void OnDisable()
        {
            playerFallDetector.OnOffMap -= HandlePlayerFell;
        }

        void Update()
        {
            if (GameStateManager.Instance.CurrentGameState != GameState.Playing) return;

            if (playerInput.JumpInput)
            {
                jumpRequested = true;
            }

            GameManager.Instance.ReportPlayerDistace(Mathf.RoundToInt(transform.position.z));
        }

        void FixedUpdate()
        {
            if (GameStateManager.Instance.CurrentGameState != GameState.Playing) return;

            playerMovement.Move(playerInput.MovementInput);

            if (jumpRequested)
            {
                playerMovement.TryJump();
                jumpRequested = false;
            }
        }
        
        void HandlePlayerFell()
        {
            GameManager.Instance.GameOver();
            playerMovement.DisablePhysics();
        }
        
        public void ApplyPowerUpEffect(PowerUpEffect powerUpEffect)
        {
            powerUpEffect.ApplyPowerUp(gameObject);

            if (powerUpEffect is TimedPowerUpEffect timedPowerUpEffect)
            {
                StartCoroutine(timedPowerUpEffect.StartTimedPowerUp(gameObject));
            }
        }
    }
}
