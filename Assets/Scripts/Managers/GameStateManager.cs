using Assets.Scripts.Enums;
using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{

    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager Instance { get; private set; }
        public GameState CurrentGameState { get; private set; } = GameState.Starting;

        public event Action OnGameStarted;
        public event Action OnGameOver;
        public event Action OnGameRestarted;
        public event Action OnMainMenuLoaded;

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
        public void StartGame()
        {
            CurrentGameState = GameState.Playing;
            OnGameStarted?.Invoke();
        }

        public void GameOver()
        {
            if (CurrentGameState != GameState.Playing) return;
            CurrentGameState = GameState.GameOver;
            OnGameOver?.Invoke();
        }

        public void RestartGame()
        {
            CurrentGameState = GameState.Starting;
            OnGameRestarted?.Invoke();
        }

        public void LoadMainMenu()
        {
            CurrentGameState = GameState.Menu;
            OnMainMenuLoaded?.Invoke();
        }
    }
}