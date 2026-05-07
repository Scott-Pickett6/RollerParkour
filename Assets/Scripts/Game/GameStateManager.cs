using UnityEngine;

namespace Game
{
    public class GameStateManager : MonoBehaviour
    {
        public GameState CurrentGameState { get; private set; } = GameState.Starting;
        public static GameStateManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        void OnEnable()
        {
            GameManager.Instance.OnGameStateChanged += SetGameState;
        }
    
        void OnDisable()
        {
            GameManager.Instance.OnGameStateChanged -= SetGameState;
        }
    
        public void SetGameState(GameState newGameState)
        {
            CurrentGameState = newGameState;
        }
    }
}
