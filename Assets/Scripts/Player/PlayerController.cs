using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private PlayerInput input;
        [SerializeField] private PlayerMovement movement;

        private PlayerInputInfo currentInputInfo;
        private GameState currentGameState;

        private void Update()
        {
            currentGameState = GameStateManager.Instance.CurrentGameState;
            currentInputInfo = input.GetInput();

            if (gameState == GameState.Starting)
            {
                if (currentInputInfo.HasInput())
                {
                    GameStateManager.Instance.StartGame();
                }
            }
        }

        private void FixedUpdate()
        {
            GameState gameState = GameStateManager.Instance.CurrentGameState;

            if (currentGameState != GameState.Playing)
                return;

            movement.Move(currentInputInfo);
        }
    }
}